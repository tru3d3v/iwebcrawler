using System;
using System.Collections.Generic;
using System.Text;

namespace CrawlerNameSpace.Utilities
{
    /**
     * This class represents a data structure which contains unique requests, with each request
     * it has a rank and can be modified anytime, this data structure gets the elements ordered 
     * by thier rank, can insert new requests and update the exist ones
     */
    public class RankingTrie
    {
        static public int RANK_LEVELS = 100;

        private Trie<PointingNode> _urlTrie;
        private RankTableEntry[] _rankLevels;
        private int _count;
        private int _maxRank, _minRank;

        /**
         * creates a new empty RankingTrie
         */
        public RankingTrie()
        {
            _urlTrie = new Trie<PointingNode>();
            _rankLevels = new RankTableEntry[RANK_LEVELS];

            for (int i = 0; i < RANK_LEVELS; i++)
            {
                _rankLevels[i] = new RankTableEntry();
                _rankLevels[i].setRankLevel(i);
            }
            _count = 0;
            _maxRank = 0;
            _minRank = 0;
        }

        /**
         * returns how many elements exists right now
         */
        public int count()
        {
            return _count;
        }

        /**
         * updates the request ranking
         */
        private void updateRequest(PointingNode pNode, Url newRequest)
        {
            if (pNode == null || newRequest == null) return;

            LinkedListNode<RankNode> pLinkNode = pNode.getRankNode();
            // the request has been token before
            if (pLinkNode == null) return;

            pLinkNode.Value.addReference(newRequest.getRank());
            int newRank = Convert.ToInt32(pLinkNode.Value.calcRank());
            if (newRank != pNode.getRankTableEntry().getRankLevel())
            {
                RankNode rankNode = pLinkNode.Value;
                pNode.getRankTableEntry().removeNode(pLinkNode);
                pNode.setRankTableEntry(_rankLevels[newRank]);
                pNode.getRankTableEntry().addFirstNode(rankNode);

                if (newRank < _minRank) _minRank = newRank;
                if (newRank > _maxRank) _maxRank = newRank;
            }
        }

        /**
         * adds request to the data structure:
         *  if the request is new it will be added normally
         *  if the request exists from before it will updated (rank)
         *  if the request exists but has been used (poped) it will do nothing
         */
        public void add(Url request)
        {
            if (request == null) return;

            bool match = _urlTrie.Matcher.NewMatch(request.getUrl().Trim());
            if (match == false)
            {
                PointingNode pNode = new PointingNode();
                LinkedListNode<RankNode> pLinkNode = _rankLevels[request.getRank()].addFirstNode(new RankNode(request));
                pNode.setRankNode(pLinkNode);
                pNode.setRankTableEntry(_rankLevels[request.getRank()]);
                _urlTrie.Put(request.getUrl().Trim(), pNode);

                if (request.getRank() < _minRank) _minRank = request.getRank();
                if (request.getRank() > _maxRank) _maxRank = request.getRank();
                _count++;
            }
            else
            {
                PointingNode pNode = _urlTrie.Matcher.GetExactMatch();
                updateRequest(pNode, request);
            }
        }

        /**
         * updates the min, max values
         */
        public void update()
        {
            _minRank = 0;
            for (int i = 0; i < RANK_LEVELS; i++)
            {
                if (_rankLevels[i].nodesCount() != 0)
                {
                    _minRank = i;
                    break;
                }
            }

            _maxRank = 0;
            for (int i = RANK_LEVELS -1; i >= 0; i--)
            {
                if (_rankLevels[i].nodesCount() != 0)
                {
                    _maxRank = i;
                    break;
                }
            }
        }

        /**
         * returns the max ranked element (pop)
         */
        public Url pop()
        {
            if (_count <= 0) return null;

            RankNode rankNode = _rankLevels[_maxRank].readFirstNode();
            _rankLevels[_maxRank].removeFirstNode();
            bool match = _urlTrie.Matcher.NewMatch(rankNode.getRequest().getUrl().Trim());
            if (match == true)
            {
                PointingNode pNode = _urlTrie.Matcher.GetExactMatch();
                pNode.setRankTableEntry(null);
                pNode.setRankNode(null);
            }
            _count--;
            update();

            Url request = rankNode.getRequest();
            int rank = Convert.ToInt32(rankNode.calcRank());
            return new Url(request.getUrl(), request.getUrlHashCode(), rank, request.getDomain(), request.getDomainHashCode());
        }

        /**
         * returns the max rank
         */
        public int getMaxRank()
        {
            return _maxRank;
        }

        /**
         * returns the min rank
         */
        public int getMinRank()
        {
            return _minRank;
        }
    }
}

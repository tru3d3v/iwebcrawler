using System;
using System.Collections.Generic;
using System.Text;

namespace CrawlerNameSpace.Utilities
{
    /**
     * This class represents a pointing node which points to the rankNode itself
     * and the rankTableEntry which contains it
     */
    public class PointingNode
    {
        private RankTableEntry _rankTableEntry;
        private LinkedListNode<RankNode> _rankNode;

        public PointingNode()
        {   
        }

        /**
         * sets the rankTableEntry
         */
        public void setRankTableEntry(RankTableEntry rankTableEntry)
        {
            _rankTableEntry = rankTableEntry;
        }

        /**
         * sets the RankNode
         */
        public void setRankNode(LinkedListNode<RankNode> rankNode)
        {
            _rankNode = rankNode;
        }

        /**
         * returns the rankTableEntry
         */
        public RankTableEntry getRankTableEntry()
        {
            return _rankTableEntry;
        }

        /**
         * returns the rankNode
         */
        public LinkedListNode<RankNode> getRankNode()
        {
            return _rankNode;
        }
    }
}

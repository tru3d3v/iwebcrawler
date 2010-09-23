using System;
using System.Collections.Generic;
using System.Text;

namespace CrawlerNameSpace.Utilities
{
    /**
     * This class reperesents rank table entry which contains a rank and it's list of Rank nodes
     */
    public class RankTableEntry
    {
        int _rankLevel;
        LinkedList<RankNode> _nodeList;

        /**
         * creates a new TankTableEntry which contains a rank level and ranknodes with 
         * the specified level
         */
        public RankTableEntry()
        {
            _nodeList = new LinkedList<RankNode>();
        }

        /**
         * sets the specified rank level
         */
        public void setRankLevel(int rank)
        {
            _rankLevel = rank;
        }

        /**
         * returns the rank level
         */
        public int getRankLevel()
        {
            return _rankLevel;
        }

        /**
         * adds the specfied node to the members of this level, and returns reference
         * to the warpper node
         */
        public LinkedListNode<RankNode> addFirstNode(RankNode rankNode)
        {
            return _nodeList.AddFirst(rankNode);
        }

        /**
         * returns the nodes count in the group
         */
        public int nodesCount()
        {
            return _nodeList.Count;
        }

        /**
         * returns the first node content in the group
         */
        public RankNode readFirstNode()
        {
            return _nodeList.First.Value;
        }

        /**
         * removes the first node element from the group
         */
        public void removeFirstNode()
        {
            _nodeList.RemoveFirst();
        }

        /**
         * removes the specified node
         */
        public void removeNode(LinkedListNode<RankNode> rankNode)
        {
            _nodeList.Remove(rankNode);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace CrawlerNameSpace.Utilities
{
    /**
     * This class represents rank node which contains url request and it's current ranking
     */
    public class RankNode
    {
        // some constants for the total rank calculation
        static double MAX_PERCENT = 0.2;
        static double MAX_REF_COUNT = 20;

        private Url _urlRequest;
        private double _max, _avg;
        int _count;
        
        /**
         * creates a new instance of RankNode
         */
        public RankNode(Url url)
        {
            _urlRequest = url;
            _avg = _max = url.getRank();
            _count = 1;
        }

        /**
         * adds a new reference/link to the specified node
         */
        public void addReference(int rank)
        {
            if (_max < rank) _max = rank;
            _avg = (_avg * _count + rank) / (_count + 1);
            _count++;
        }

        /**
         * returns the current rank of the node
         */
        public double calcRank()
        {
            double totalRank = MAX_PERCENT * _max + (1 - MAX_PERCENT) * _avg;
            if (_count > MAX_REF_COUNT) totalRank += MAX_REF_COUNT;
            else totalRank += _count;
            return Math.Min(100, totalRank);
        }

        /**
         * returns the url request
         */
        public Url getRequest()
        {
            int rank = Convert.ToInt32(calcRank());
            return new Url(_urlRequest.getUrl(), _urlRequest.getUrlHashCode(), rank, _urlRequest.getDomain(), _urlRequest.getDomainHashCode());
        }
    }
}

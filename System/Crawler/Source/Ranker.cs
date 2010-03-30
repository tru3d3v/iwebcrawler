using System;
using System.Collections.Generic;
using System.Text;

namespace CrawlerNameSpace
{
    /**
     * This Class is responsible for giving a rank to  the url
     * that has been fetched.
     */
    class Ranker
    {
        private Categorizer categorizer;
        private int defualtRank = 0;

        public Ranker(Categorizer categorizer)
        {
            this.categorizer = categorizer;
        }

        /**
         * This method calculates the rank of a given resource and returns it.
         */
        public int rankUrl(int parentRank, String parentContent, ResourceContent resource)
        {
            return defualtRank;
        }
    }
}

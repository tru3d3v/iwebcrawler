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
         * This method calculates the rank of a given url and returns it.
         */
        public int rankUrl(int parentRank, String parentContent, String url)
        {
            return defualtRank;
        }
    }
}

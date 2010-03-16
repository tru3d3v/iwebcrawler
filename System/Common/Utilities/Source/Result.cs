using System;
using System.Collections.Generic;
using System.Text;

/**
 * This NameSpace offers some utitlity classes which will be used on all over the crawler
 * project and the other projects, This namespace contains classes which represent:
 * Task, Category, Constraints, Result, Record;
 */
namespace CrawlerNameSpace.Utilities
{

    // This enumuration represents the orders we can ask for the results from the data base
    public enum Order { NormalOrder, AscendungRank, DescendingRank, AscendingTrust, DescendingTrust};

    /**
     * This class is immutable class which represents the result of crawled url, 
     * so it contains matching data between the url and it's suitable category and rank.
     */ 
    class Result
    {
        // private members which contains the url and it's suitable category, it also
        // conatins two metreics the rank which stands for the page rank (used actually for
        // getting some data on the pages on the net), and the trust meter which is represent
        // confidence level of the result
        private string resultID, url, categoryID;
        private int rank, trustMeter;

        /**
         * constucts a new Result class with the specified details
         */
        public Result(string resultID, string url, string categoryID, int rank, int trustMeter)
        {
            this.resultID = resultID;
            this.url = url;
            this.categoryID = categoryID;
            this.rank = rank;
            this.trustMeter = trustMeter;
        }

        /**
         * returns the resultID of this
         */
        public string getResultID()
        {
            return resultID;
        }

        /**
         * returns the url of this result
         */
        public string getUrl()
        {
            return url;
        }

        /**
         * returns the categoryID of this
         */
        public string getCategoryID()
        {
            return categoryID;
        }

        /**
         * returns the rank of the url page
         */
        public int getRank()
        {
            return rank;
        }

        /**
         * returns the trust meterics of the result
         */
        public int getTrustMeter()
        {
            return trustMeter;
        }
    }
}

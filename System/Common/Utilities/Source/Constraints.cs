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
    /**
     * This class is responsible for maintaining and validiting all the user constriants
     * of the crawled netword, thus this class can determine if the given link is allowed
     * (e.g. should be crawled with the given constrains or not)
     */ 
    public class Constraints
    {
        // This sign is the separator when representing network group
        private const char SEPRATOR = ' ';

        // defines the allowed link depth (e.g. max limit)
        uint linkDepth;
        // saves whether url parameters allowed or not
        bool allowUrlParameters;

        // these lists saves the restricted networks (the crawler shouldn't crawl)
        // and the crawl networks (we may crawl)
        List<string> restrictedNetworks, crawlNetworks;

        /**
         * constucts a new constrains object with the specified configuration
         */ 
        public Constraints(uint depth, bool allowParam, string restrict, string crawl)
        {
            linkDepth = depth;
            allowUrlParameters = allowParam;

            restrictedNetworks = new List<string>();
            crawlNetworks = new List<string>();

            string[] restrictedArray = restrict.Split(new char[] { SEPRATOR }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string network in restrictedArray)
            {
                restrictedNetworks.Add(network.ToLower());
            }

            string[] allowedArray = crawl.Split(new char[] { SEPRATOR }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string network in allowedArray)
            {
                crawlNetworks.Add(network.ToLower());
            }
        }

        /**
         * returns the depth max. limit (0 means no limitation at all)
         */ 
        public uint getAllowedDepth()
        {
            return linkDepth;
        }

        /**
         * returns a copy of the restricted networks list
         */ 
        public List<string> getRestrictionList()
        {
            List<string> cloned = new List<string>(restrictedNetworks);
            return cloned;
        }

        /**
         * returns a copy of the crawl networks list
         */
        public List<string> getCrawlList()
        {
            List<string> cloned = new List<string>(crawlNetworks);
            return cloned;
        }

        /**
         * returns true if parameters allowed in urls, otherwise false will be returned
         */ 
        public bool isParametrizationAllowed()
        {
            return allowUrlParameters;
        }

        /**
         * returns true if the url meets all the requirements specified by the constraints
         * otherwise false will be returned
         */ 
        public bool isUrlValid(string url)
        {
            try
            {
                if (linkDepth != 0 && getUrlDepth(url) > linkDepth) return false;
                if (allowUrlParameters != true && containsParameter(url) == true) return false;

                string network = getUrlNetwork(url).ToLower();
                foreach (string restrictedNetwork in restrictedNetworks)
                    if (network.EndsWith(restrictedNetwork) == true) return false;

                if (crawlNetworks.Count != 0)
                {
                    foreach (string crawlNetwork in crawlNetworks)
                        if (network.EndsWith(crawlNetwork) == true) return true;
                    return false;
                }
                return true;
            }
            catch (System.ArgumentException e)
            {
                return false;
            }
        }

        /**
         * This method returns the depth of the given url, it assumes that the given url is in 
         * legal form : protocol://address
         */ 
        private uint getUrlDepth(string url)
        {
            string[] words = url.Split(new string[] { "://" }, StringSplitOptions.RemoveEmptyEntries);
            if(words.GetLength(0) != 2) throw new System.ArgumentException("Illegal Url address");

            string[] buffer = words[1].Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
            return (uint)buffer.GetLength(0);
        }

        /**
         * This method returns true if the address contains parameter, it assumes that the given
         * url is in legal form : protocol://address
         */
        private bool containsParameter(string url)
        {
            string[] words = url.Split(new string[] { "://" }, StringSplitOptions.RemoveEmptyEntries);
            if (words.GetLength(0) != 2) throw new System.ArgumentException("Illegal Url address");

            return words[1].Contains("?");
        }

        /**
         * This method returns the network of the given url, it assumes that the given url is in
         * legal form : protocol://address
         */ 
        private string getUrlNetwork(string url)
        {
            string[] words = url.Split(new string[] { "://" }, StringSplitOptions.RemoveEmptyEntries);
            if (words.GetLength(0) != 2) throw new System.ArgumentException("Illegal Url address");

            string[] buffer = words[1].Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
            if (buffer.GetLength(0) == 0) throw new System.ArgumentException("Illegal Url address");
            return buffer[0];
        }
    }
}

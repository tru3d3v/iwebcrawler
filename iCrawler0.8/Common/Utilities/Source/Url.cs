using System;
using System.Collections.Generic;
using System.Text;

namespace CrawlerNameSpace.Utilities
{
    /**
     * This class represents the url, it contains it's rank and it's hashcode.
     */
    public class Url
    {
        private String url, domain;
        
        //hashcode represents the hashcode of the url.
        private int hashcode, domainHash;

        //rank contains the rank given to the url.
        private int rank;

        public Url(String _url, int _hashcode, int _rank, String _domain, int _domainHash)
        {
            url        = _url;
            hashcode   = _hashcode;
            rank       = _rank;
            domain     = _domain;
            domainHash = _domainHash;
        }

        /**
         * This method returns the url.
         */
        public String getUrl()
        {
            return url;
        }

        /**
         * This method returns the url.
         */
        public String getDomain()
        {
            return domain;
        }

        /**
         * This method returns the hashcode of the url.
         */
        public int getUrlHashCode()
        {
            return hashcode;
        }

        /**
         * This method returns the hashcode of the url.
         */
        public int getDomainHashCode()
        {
            return domainHash;
        }


        /**
         * This method returns the rank of the url.
         */
        public int getRank()
        {
            return rank;
        }

        /**
         * This method overrides the ToString method.
         */
        public override string ToString()
        {
            String urlString;
            urlString = "Url is : " + url + "[-]HashCode of the url is : " +
                       hashcode + "[-]Rank of url is : " + rank + "[-]Domain Url : " + 
                       domain + "[-]HashCode of the domain is : " + domainHash + "[-]";
            return urlString;
        }

    }
}

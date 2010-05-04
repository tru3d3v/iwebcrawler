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
        private String url;
        
        //hashcode represents the hashcode of the url.
        private int hashcode;

        //rank contains the rank given to the url.
        private int rank;

        public Url(String _url, int _hashcode, int _rank)
        {
            url = _url;
            hashcode = _hashcode;
            rank = _rank;
        }

        /**
         * This method returns the url.
         */
        public String getUrl()
        {
            return url;
        }

        /**
         * This method returns the hashcode of the url.
         */
        public int getHashCode()
        {
            return hashcode;
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
            urlString = "Url is : " + url + "|HashCode of the url is : " +
                       hashcode + "|Rank of url is : " + rank;
            return urlString;
        }

    }
}

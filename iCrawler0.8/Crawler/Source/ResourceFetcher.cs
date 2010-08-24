using System;
using System.Collections.Generic;
using System.Text;

namespace CrawlerNameSpace
{
    /**
     * This interface defines the methods that every resource fethcer unit should supply
     * that is the canfetch method and the fetch method
     */
    
    interface ResourceFetcher
    {
        /**
         * This method returns boolean value that indicates if the given url argument can 
         * be fetched or not.
         */
         bool canFetch(String url);

        /**
         * This method tries to fetch the given url from the web and returns the resourceContent 
         * of the url.
         */
         ResourceContent fetch(String url, int timeOut, int rankOfUrl);

    }
}

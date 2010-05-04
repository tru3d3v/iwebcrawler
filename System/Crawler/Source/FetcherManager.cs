using System;
using System.Collections.Generic;
using System.Text;

/**
 * This class is a container for all the fechers (i.e. downloaders) which is responsible
 * for downloading a resource from the internet, this class searchs for the first protocol 
 * handler which can download the first url
 */
namespace CrawlerNameSpace
{
    class FetcherManager
    {
        // This list will contain the protocols which can fetch the required url
        private Dictionary<String, ResourceFetcher> resourceFetchers;
        // This is the time out for the request in miliseconds
        int timeOut;

        /**
         * Constracts a new fetcher manger which will contain resource fetchers
         */ 
        public FetcherManager()
        {
            resourceFetchers = new Dictionary<string, ResourceFetcher>();
            timeOut = 1000;
        }

        /**
         * This method sets the time out limit for the request to the desired on in ms
         */ 
        public void setTimeOut(int desiredTimeOut)
        {
            timeOut = desiredTimeOut;
        }

        /**
         * This method adds a new fetcher portcol to the manager 
         */ 
        public void addProtocol(String protocolId, ResourceFetcher protocol)
        {
            if (protocolId == null || protocol == null) throw new NullReferenceException();
            if (resourceFetchers.ContainsKey(protocolId) == true) throw new InvalidOperationException();
            resourceFetchers.Add(protocolId, protocol);
        }

        /**
         * Removes the protocol from the manager container, if the desired protocol
         * not found nothing will be done.
         */ 
        public void removeProtocol(String protocolId)
        {
            if (resourceFetchers.ContainsKey(procId) == true)
                resourceFetchers.Remove(procId);
        }

        /**
         * This method is used to fetch the required resource from the network by using one
         * of the supplied fetchers, null will be returned if no fetcher can fetch the required
         * content
         */ 
        public ResourceContent fetchResource(String url)
        {
            if (url == null) throw new NullReferenceException();
            foreach (String protcolId in resourceFetchers.Keys)
            {
                if (resourceFetchers[protcolId].canFetch(url) == true)
                {
                    return resourceFetchers[protocolId].fetch(url);
                }
            }
            return null;
        }
    }
}

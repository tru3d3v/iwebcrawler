using System;
using System.Collections.Generic;
using System.Text;

namespace CrawlerNameSpace.Utilities
{
    /**
     * This class maintain runtime information about the crawler, the crawled pages memory usage
     * and etc; note this class will be static global class for all the components thus it thread safe
     */
    public class RuntimeStatistics
    {
        // These variables contains the status of the crawler at each point of time
        private static Long crawledUrls = 0, totalErrors = 0, extractedUrls = 0, feedUrls = 0;
        private static Long PagesCrawled = 0, fetchedUrls = 0, frontierUrls = 0;

        /**
         * resets the counters
         */
        public static void resetStatistics()
        {
            crawledUrls = 0;
            totalErrors = 0;
            extractedUrls = 0;
            feedUrls = 0;
            PagesCrawled = 0;
            fetchedUrls = 0;
            frontierUrls = 0;
        }

        /**
         * sets the specified amount to the frontier urls counter
         */
        public static void setFrontierUrls(Long amount)
        {
            lock (frontierUrls)
            {
                frontierUrls = amount;
            }
        }

        /**
         * returns the value of the specified counter
         */
        public static long getFrontierUrls()
        {
            lock (frontierUrls)
            {
                return frontierUrls;
            }
        }

        /**
         * adds the specified amount to the fetched urls counter
         */
        public static void addToFetchedUrls(Long amount)
        {
            lock (fetchedUrls)
            {
                fetchedUrls = fetchedUrls + amount;
            }
            lock (PagesCrawled)
            {
                PagesCrawled = PagesCrawled + amount;
            }
        }

        /**
         * returns the value of the specified counter
         */
        public static long getFetchedUrls()
        {
            lock (fetchedUrls)
            {
                return fetchedUrls;
            }
        }

        /**
         * adds the specified amount to the crawled urls counter
         */
        public static void addToCrawledUrls(Long amount)
        {
            lock (crawledUrls)
            {
                crawledUrls = crawledUrls + amount;
            }
        }

        /**
         * returns the value of the specified counter
         */
        public static long getCrawledUrls()
        {
            lock (crawledUrls)
            {
                return crawledUrls;
            }
        }

        /**
         * returns the number of pages has been crawled since the last time this 
         * function has been called 
         */
        public static long getPagesCrawledNum()
        {
            lock (PagesCrawled)
            {
                long lastValue = PagesCrawled;
                PagesCrawled = 0;
                return lastValue;
            }
        }

        /**
         * adds the specified amount to the errors counter
         */
        public static void addToErrors(Long amount)
        {
            lock (totalErrors)
            {
                totalErrors = totalErrors + amount;
            }
        }

        /**
         * returns the value of the specified counter
         */
        public static long getTotalErrors()
        {
            lock (totalErrors)
            {
                return totalErrors;
            }
        }

        /**
         * adds the specified amount to the extracted urls counter
         */
        public static void addToExtractedUrls(Long amount)
        {
            lock (extractedUrls)
            {
                extractedUrls = extractedUrls + amount;
            }
        }

        /**
         * returns the value of the specified counter
         */
        public static long getExtractedUrls()
        {
            lock (extractedUrls)
            {
                return extractedUrls;
            }
        }

        /**
         * adds the specified amount to the extracted urls counter
         */
        public static void addToFeedUrls(Long amount)
        {
            lock (feedUrls)
            {
                feedUrls = feedUrls + amount;
            }
        }

        /**
         * returns the value of the specified counter
         */
        public static long getFeedUrls()
        {
            lock (feedUrls)
            {
                return feedUrls;
            }
        }
    }
}

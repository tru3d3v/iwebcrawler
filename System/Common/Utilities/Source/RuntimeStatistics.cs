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

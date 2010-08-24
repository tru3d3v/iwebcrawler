using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Runtime.InteropServices;


namespace CrawlerNameSpace.Utilities
{
    /**
     * This class is responsible about displaying the overall status of the crawler
     * including the usage of the system resources and the statistics
     */
    public class StatusDisplay
    {
        private static void DisplayBasicInfo(int workerThreads)
        {
            System.Console.WriteLine("--- BASIC INFO ---------------------------------------------------------------");
            System.Console.WriteLine("  Crawler Name               : " + "iWebCrawler Development Version");
            System.Console.WriteLine("  Task Name                  : " + "None");
            System.Console.WriteLine("  Task Owner                 : " + "None");
            System.Console.WriteLine("  Task Worker Threads        : " + workerThreads);
            long total = System.GC.GetTotalMemory(false) / (1024*1024);
            System.Console.WriteLine("  Total Memory Allocated     : " + total + " MB");
        }

        private static long DisplayStatus(Queue<Url> feedBackQueue, List<Queue<Url>> serversQueues)
        {
            long totalRequests = 0;
            System.Console.WriteLine("--- SYSTEM STATUS -------------------------------------------------------------");
            lock (feedBackQueue)
            {
                totalRequests = totalRequests + feedBackQueue.Count;
                System.Console.WriteLine("  Requests in the frontier\t about ~ {0} ", feedBackQueue.Count);
            }
            for (int queueNum = 0; queueNum < serversQueues.Count; queueNum++)
            {
                lock (serversQueues[queueNum])
                {
                    totalRequests = totalRequests + serversQueues[queueNum].Count;
                    System.Console.WriteLine("  Requests in the Thread[{0}]\t about ~ {1} ", queueNum, serversQueues[queueNum].Count);
                }
            }
            return totalRequests;
        }

        private static void DisplayEntriesInSystems(long totalRequests)
        {
            System.Console.WriteLine("--- URL ENTRIES ---------------------------------------------------------------");
            System.Console.WriteLine("  Url to Visit In the Crawler : {0} \t\t\t X ??? Bytes for Entry", totalRequests);
        }

        private static void DisplayRuntimeStatistics()
        {
            System.Console.WriteLine("--- RUNTIME STATISTICS --------------------------------------------------------");
            try
            {
                long crawled = RuntimeStatistics.getCrawledUrls(), extracted = RuntimeStatistics.getExtractedUrls();
                long feedback = RuntimeStatistics.getFeedUrls(), errors = RuntimeStatistics.getTotalErrors();
                long pages = RuntimeStatistics.getPagesCrawledNum();
                float extarctedPercentage = extracted / crawled;
                float feedBackPercentage = feedback / crawled;
                float errorsPercentage = ((float)errors / (float)crawled) * 100;

                System.Console.WriteLine("  Total Crawled   Urls : {0}\t\t (Rate {1} pages per refresh rate)", crawled, pages);
                System.Console.WriteLine("  Total Extracted Urls : {0}\t\t [{1}X]", extracted, extarctedPercentage);
                System.Console.WriteLine("  Total FeedBack  Urls : {0}\t\t [{1}X]", feedback, feedBackPercentage);
                System.Console.ForegroundColor = ConsoleColor.Red;
                System.Console.WriteLine("  Total Errors Occured : {0}\t\t [{1}%]", errors, errorsPercentage);
                System.Console.ForegroundColor = ConsoleColor.White;
            }
            catch (Exception e)
            {
                System.Console.WriteLine("  No Statistics Avalibale right now");
            }
        }

        public static void DisplayOnScreen(Queue<Url> feedBackQueue, List<Queue<Url>> serversQueues)
        {
            System.Console.Clear();
            System.Console.ForegroundColor = ConsoleColor.White;
            DisplayBasicInfo(serversQueues.Count);
            long totalRequests = DisplayStatus(feedBackQueue, serversQueues);
            DisplayEntriesInSystems(totalRequests);
            DisplayRuntimeStatistics();
        }
    }
}

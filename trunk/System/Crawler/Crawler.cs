using System;
using System.Collections.Generic;
using System.Text;

/**
 * This is the main class for crawler system. this system defines a crawler application
 * which get connected to the storage system to get a task and start progressing it.
 * This application will get all the task configuration has been stored by the user and
 * start downloading urls from the web. all the results will be saved in the Storage System.
 */
namespace CrawlerNameSpace
{
    class Crawler
    {
        static void Main(String[] args)
        {
            System.Console.WriteLine("[1] Resource Content Test");
            ResourceContent.Test();
            System.Console.WriteLine("[2] Resource Processor Manager Test");
            ResourceProcessorManager.Test();
            System.Console.WriteLine("[3] HttpResourceFetcher Test");
            HttpResourceFetcher.Test();
            System.Console.WriteLine("[4] Extractor Test");
            Extractor.Test();
        }
    }
}

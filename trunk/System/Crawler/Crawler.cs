using System;
using System.Collections.Generic;
using System.Text;
using CrawlerNameSpace.Tests;
using CrawlerNameSpace.Utilities;
using CrawlerNameSpace.Utilities.Tests;
using System.Threading;

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
            int numOfWorkers = 4;
            while (true)
            {
                // getting init data
                List<Category> categories = new List<Category>();
                Constraints constraints = new Constraints(1, false, "", ".com");
                Initializer initializer = new Initializer(constraints, categories);

                // init queues
                List<Queue<Url>> serversQueues = new List<Queue<Url>>();
                Queue<Url> feedBackQueue = new Queue<Url>();

                for (int serverNum = 0; serverNum < numOfWorkers; serverNum++)
                {
                    serversQueues.Add(new Queue<Url>());
                }

                // getting seeds
                Url task1 = new Url("http://www.playboy.com/", 34243432, 35, "http://www.playboy.com/", 34243432);
                //Url task2 = new Url("http://www.nana10.co.il/", 34223432, 35, "http://www.nana10.co.il/", 34223432);
                feedBackQueue.Enqueue(task1);

                // initing worker threads
                for (int threadNum = 0; threadNum < numOfWorkers; threadNum++)
                {
                    Worker worker = new Worker(initializer, serversQueues[threadNum], feedBackQueue);
                    Thread workerThread = new Thread(new ThreadStart(worker.run));
                    workerThread.Start();
                }

                // init the Frontier thread
                Frontier frontier = new Frontier(feedBackQueue, serversQueues);
                Thread frontierThread = new Thread(new ThreadStart(frontier.sceduleTasks));
                frontierThread.Start();

                // polling to the user requests
                while (true)
                {
                    Thread.Sleep(5000);
                    StatusDisplay.DisplayOnScreen(feedBackQueue, serversQueues);
                }
            }
        }
    }
}

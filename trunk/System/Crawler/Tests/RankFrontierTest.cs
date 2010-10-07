using System;
using System.Collections.Generic;
using System.Text;
using CrawlerNameSpace.Utilities;
using System.Threading;

namespace CrawlerNameSpace.Tests
{
    public class RankFrontierTest
    {
        private Queue<Url> feedback = new Queue<Url>();
        private List<Queue<Url>> serverQueues = new List<Queue<Url>>();
        
        public void test1()
        {
            initQueues();
            //DateTime startTime = DateTime.Now;
            RankFrointer rankFrontier = new RankFrointer(feedback, serverQueues);
            Thread frontierThread = new Thread(new ThreadStart(rankFrontier.sceduleTasks));
            frontierThread.Start();
            Thread workerThread = new Thread(new ThreadStart(workerSimulator));
            workerThread.Start();

            int iteration = 1;
            while (true) 
            {
                Thread.Sleep(1000);
                if (iteration < 10) iteration++;
                fillQueue(1000 * iteration, 2000 * iteration);
            }
            //DateTime endTime = DateTime.Now;
            //workerThread.Abort();
            //frontierThread.Abort();
        }

        private void initQueues()
        {
            serverQueues.Add(new Queue<Url>());
            fillQueue(0,5000);
        }

        private void fillQueue(int minRange,int maxRange)
        {
            for (int i = minRange; i < maxRange; i++)
            {
                String url = generateString(i);
                SyncAccessor.putInQueue<Url>(feedback, new Url(url, 0, i % 100, url, i));
            }
        }

        private string generateString(int count)
        {
            return count.ToString();
        }

        private void workerSimulator()
        {
            int errCount = 0, requestNum = 1;
            while (true)
            {
                Thread.Sleep(10);
                if (SyncAccessor.queueSize<Url>(serverQueues[0]) > 0)
                {
                    Url url = SyncAccessor.getFromQueue<Url>(serverQueues[0], 100);
                    if (url != null)
                    {
                        Console.WriteLine("******************************************************");
                        Console.WriteLine("URL DEQUEUED: ");
                        Console.WriteLine(url.getUrl());
                        Console.WriteLine("RANK OF URL");
                        Console.WriteLine(url.getRank());
                        Console.WriteLine("REQUEST NUM");
                        Console.WriteLine(requestNum++);
                    }
                    else
                    {
                        errCount++;
                        Console.WriteLine("ERROR - " + errCount.ToString());
                    }
                }
            }
        }

        public void test2()
        {
            initQueuesTest2();
            DateTime startTime = DateTime.Now;
            RankFrointer rankFrontier = new RankFrointer(feedback, serverQueues);
            Thread frontierThread = new Thread(new ThreadStart(rankFrontier.sceduleTasks));
            frontierThread.Start();
            Thread workerThread = new Thread(new ThreadStart(workerSimulator));
            workerThread.Start();
            
            int iteration = 5001;
            while (true)
            {
                Thread.Sleep(1000);
                //fillQueue(iteration, iteration + 20);
                iteration ++;
                if (iteration < 10000) break;
            }
            
            DateTime endTime = DateTime.Now;
            TimeSpan totalTime = endTime - startTime;
        }

        private void initQueuesTest2()
        {
            serverQueues.Add(new Queue<Url>());
            fillQueue(0, 5000);
        }

        public void test3()
        {
            initQueues();
            DateTime startTime = DateTime.Now;
            BFSFrontier rankFrontier = new BFSFrontier(feedback, serverQueues);
            Thread frontierThread = new Thread(new ThreadStart(rankFrontier.sceduleTasks));
            frontierThread.Start();
            Thread workerThread = new Thread(new ThreadStart(workerSimulator));
            workerThread.Start();

            int iteration = 5000;
            while (iteration < 1000000)
            {
                Thread.Sleep(1000);
                iteration += 20;
                fillQueue(iteration, iteration + 19);
            }
            DateTime endTime = DateTime.Now;
            TimeSpan totalTime = endTime - startTime;
        }  
    }
}

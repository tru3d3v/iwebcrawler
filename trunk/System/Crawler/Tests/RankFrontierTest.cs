using System;
using System.Collections.Generic;
using System.Text;
using CrawlerNameSpace.Utilities;
using System.Threading;
using System.IO;

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

        //test for measuring performance
        public void test2()
        {
            initQueuesTest2();
            DateTime startTime = DateTime.Now;
            RankFrointer rankFrontier = new RankFrointer(feedback, serverQueues);
            Thread frontierThread = new Thread(new ThreadStart(rankFrontier.sceduleTasks));
            frontierThread.Start();
            Thread workerThread = new Thread(new ThreadStart(workerSimulator2));
            workerThread.Start();
            
            int iteration = 5000;
            while (true)
            {
                Thread.Sleep(10);
                fillQueue(iteration, iteration + 5000);
                iteration += 5000;
                if (iteration > 50000)
                    break;
            }
            //frontierThread.Join();
            workerThread.Join();

            DateTime endTime = DateTime.Now;
            TimeSpan totalTime = endTime - startTime;
        }

        private void initQueuesTest2()
        {
            serverQueues.Add(new Queue<Url>());
            fillQueue(0, 5000);
        }

        //test for the bfs
        public void test3()
        {
            initQueuesTest2();
            DateTime startTime = DateTime.Now;
            BFSFrontier bfsFrontier = new BFSFrontier(feedback, serverQueues);
            Thread frontierThread = new Thread(new ThreadStart(bfsFrontier.sceduleTasks));
            frontierThread.Start();
            Thread workerThread = new Thread(new ThreadStart(workerSimulator2));
            workerThread.Start();

            int iteration = 5000;
            while (true)
            {
                Thread.Sleep(10);
                fillQueue(iteration, iteration + 5000);
                iteration += 5000;
                if (iteration > 50000)
                    break;
            }
            //frontierThread.Join();
            workerThread.Join();

            DateTime endTime = DateTime.Now;
            TimeSpan totalTime = endTime - startTime;
        }

        private void workerSimulator3()
        {
            int errCount = 0, emptyQueueCount = 0;
            bool queueFirstNotEmpty = false;
            while (true)
            {
                Thread.Sleep(10);
                    
                if (SyncAccessor.queueSize<Url>(serverQueues[0]) > 0)
                {
                    queueFirstNotEmpty = true;
                    emptyQueueCount = 0;
                    Url url = SyncAccessor.getFromQueue<Url>(serverQueues[0], 100);
                    if (url != null)
                    {
                        RuntimeStatistics.addToCrawledUrls(1);
                    }
                    else
                    {
                        RuntimeStatistics.addToErrors(1);
                        errCount++;
                        Console.WriteLine("ERROR - " + errCount.ToString());
                    }
                }
                else
                {
                    Thread.Sleep(1000);
                    if (emptyQueueCount > 2)
                        break;
                    if (queueFirstNotEmpty)
                        emptyQueueCount++;


                }
            }
        }

        //test that displays statistics
        public void test4()
        {
            RuntimeStatistics.addToExtractedUrls(1);
            initQueuesTest2();
            RuntimeStatistics.addToFeedUrls(5000);
            DateTime startTime = DateTime.Now;
            RankFrointer rankFrontier = new RankFrointer(feedback, serverQueues);
            Thread frontierThread = new Thread(new ThreadStart(rankFrontier.sceduleTasks));
            frontierThread.Start();
            Thread workerThread = new Thread(new ThreadStart(workerSimulator3));
            workerThread.Start();

            int iteration = 5000;
            while (true)
            {
                StatusDisplay.DisplayOnScreen(feedback, serverQueues);
                Thread.Sleep(1000);
                if (iteration > 40000)
                {
                    break;
                }
                fillQueue(iteration,iteration+5000);
                iteration += 5000;
                RuntimeStatistics.addToFeedUrls(5000);
            }
            while (true)
            {
                StatusDisplay.DisplayOnScreen(feedback, serverQueues);
                Thread.Sleep(1000);
            }
        }

        private void workerSimulator2()
        {
            int errCount = 0, emptyQueueCount = 0, urlsNum = 1;
            bool queueFirstNotEmpty = false;
            while (true)
            {
                //Thread.Sleep(10);
                if (SyncAccessor.queueSize<Url>(serverQueues[0]) > 0)
                {
                    queueFirstNotEmpty = true;
                    emptyQueueCount = 0;
                    Url url = SyncAccessor.getFromQueue<Url>(serverQueues[0], 100);
                    if (url != null)
                    {
                        Console.WriteLine("******************************************************");
                        Console.WriteLine("URL DEQUEUED: ");
                        Console.WriteLine(url.getUrl());
                        Console.WriteLine("RANK OF URL");
                        Console.WriteLine(url.getRank());
                        Console.WriteLine("NumberOfUrls");
                        Console.WriteLine(urlsNum++);
                    }
                    else
                    {
                        errCount++;
                        Console.WriteLine("ERROR - " + errCount.ToString());
                    }
                }
                else
                {
                    Thread.Sleep(1000);
                    if (emptyQueueCount > 2)
                        break;
                    if (queueFirstNotEmpty)
                        emptyQueueCount++;


                }
            }
        }

        public void test5()
        {

            initQueuesTest2();
            RuntimeStatistics.addToExtractedUrls(1);
            RuntimeStatistics.addToFeedUrls(5000);
            RankFrointer rankFrontier = new RankFrointer(feedback, serverQueues);
            Thread frontierThread = new Thread(new ThreadStart(rankFrontier.sceduleTasks));
            frontierThread.Start();
            Thread workerThread = new Thread(new ThreadStart(workerSimulator4));
            workerThread.Start();

            while (true)
            {
                Thread.Sleep(1000);
                StatusDisplay.DisplayOnScreen(feedback, serverQueues);
            }

        }

        private void workerSimulator4()
        {
            int errCount = 0, urlsNum = 1,emptyQueueCount = 0;
            bool queueFirstNotEmpty = false;
            while (true)
            {
                Thread.Sleep(10);
                if (SyncAccessor.queueSize<Url>(serverQueues[0]) > 0)
                {
                    queueFirstNotEmpty = true;
                    emptyQueueCount = 0;
                    Url url = SyncAccessor.getFromQueue<Url>(serverQueues[0], 100);
                    if (url != null)
                    {
                        RuntimeStatistics.addToCrawledUrls(1);
                        StreamWriter sw = new
                        StreamWriter("FrontierDebugger1.txt", true);
                        sw.WriteLine("******************************************************");
                        sw.WriteLine("URL DEQUEUED: ");
                        sw.WriteLine(url.getUrl());
                        sw.WriteLine("RANK OF URL");
                        sw.WriteLine(url.getRank());
                        sw.WriteLine("NumberOfUrls");
                        sw.WriteLine(urlsNum++);
                        sw.Close();
                    }
                    else
                    {
                        errCount++;
                        Console.WriteLine("ERROR - " + errCount.ToString());
                    }
                }
                else
                {
                    Thread.Sleep(1000);
                    if (emptyQueueCount > 2)
                        break;
                    if (queueFirstNotEmpty)
                        emptyQueueCount++;
                }

            }
        }

        private void workerSimulator5()
        {
            int errCount = 0, urlsNum = 1, emptyQueueCount = 0;
            bool queueFirstNotEmpty = false;
            while (true)
            {
                Thread.Sleep(10);
                if (SyncAccessor.queueSize<Url>(serverQueues[1]) > 0)
                {
                    queueFirstNotEmpty = true;
                    emptyQueueCount = 0;
                    Url url = SyncAccessor.getFromQueue<Url>(serverQueues[1], 100);
                    if (url != null)
                    {
                        RuntimeStatistics.addToCrawledUrls(1);
                        StreamWriter sw = new
                        StreamWriter("FrontierDebugger2.txt", true);
                        sw.WriteLine("******************************************************");
                        sw.WriteLine("URL DEQUEUED: ");
                        sw.WriteLine(url.getUrl());
                        sw.WriteLine("RANK OF URL");
                        sw.WriteLine(url.getRank());
                        sw.WriteLine("NumberOfUrls");
                        sw.WriteLine(urlsNum++);
                        sw.Close();
                    }
                    else
                    {
                        errCount++;
                        Console.WriteLine("ERROR - " + errCount.ToString());
                    }
                }
                else
                {
                    Thread.Sleep(1000);
                    if (emptyQueueCount > 2)
                        break;
                    if (queueFirstNotEmpty)
                        emptyQueueCount++;
                }

            }
        }

        //tests a multi-threaded workers
        public void test6()
        {
            DateTime startTime = DateTime.Now;
            initQueuesTest3();
            RuntimeStatistics.addToExtractedUrls(1);
            RuntimeStatistics.addToFeedUrls(5000);
            RankFrointer rankFrontier = new RankFrointer(feedback, serverQueues);
            Thread frontierThread = new Thread(new ThreadStart(rankFrontier.sceduleTasks));
            frontierThread.Start();
            Thread workerThread = new Thread(new ThreadStart(workerSimulator4));
            workerThread.Start();

            Thread workerThread2 = new Thread(new ThreadStart(workerSimulator5));
            workerThread2.Start();

            int iteration = 5000;
            while (true)
            {
                Thread.Sleep(10);
                fillQueue(iteration, iteration + 5000);
                iteration += 5000;
                RuntimeStatistics.addToFeedUrls(5000);
                StatusDisplay.DisplayOnScreen(feedback, serverQueues);
                if (iteration > 1000000)
                    break;
            }
            while (true)
            {
                Thread.Sleep(1000);
                StatusDisplay.DisplayOnScreen(feedback, serverQueues);
                //workerThread.Interrupt();
                ThreadState state = workerThread.ThreadState;
                Console.WriteLine("Workerthread is : " + state.ToString());
                ThreadState state2 = workerThread2.ThreadState;
                if ((state == ThreadState.Stopped)&&(state2==ThreadState.Stopped))
                {
                    //continue;
                    //workerThread2.Join();
                    DateTime endTime = DateTime.Now;
                    TimeSpan totalTime = endTime - startTime;
                }
                Console.WriteLine("Workerthread2 is : " + state2.ToString());
            }
        }

        private void initQueuesTest3()
        {
            serverQueues.Add(new Queue<Url>());
            serverQueues.Add(new Queue<Url>());
            fillQueue(0, 5000);
        }
    }
}

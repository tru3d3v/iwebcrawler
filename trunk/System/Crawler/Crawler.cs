using System;
using System.Collections.Generic;
using System.Text;
using CrawlerNameSpace.Tests;
using CrawlerNameSpace.Utilities;
using System.Threading;
using CrawlerNameSpace.StorageSystem;

/**
 * This is the main class for crawler system. this system defines a crawler application
 * which get connected to the storage system to get a task and start progressing it.
 * This application will get all the task configuration has been stored by the user and
 * start downloading urls from the web. all the results will be saved in the Storage System.
 */
namespace CrawlerNameSpace
{
    public class Crawler
    {
        protected static int _numWorkers  = 4;
        protected static int _refreshRate = 5;
        protected static List<String> _seedList = new List<string>();
        protected static operationMode_t _operationMode = operationMode_t.Manual;

        protected static List<Category> _categories;
        protected static Constraints _constraints;
        protected static Initializer _initializer;

        protected static List<Queue<Url>> _serversQueues;
        protected static Queue<Url> _feedBackQueue;

        protected static List<Thread> _threadsPool = new List<Thread>();
        protected static List<Worker> _workersPool = new List<Worker>();
        protected static List<Frontier> _frontiersPool = new List<Frontier>();

        /**
         * selects very suitable task to run
         */
        protected static void SelectTask(ref String user, ref String task)
        {
            System.Console.Write("$$$ Selecting Suitable task .. ");
            if (_operationMode == operationMode_t.Auto)
            {
                // TODO: need to select the suitable user also
                int size = 0;
                List<TaskStatus> tasks = null;
                while (size == 0)
                {
                    tasks = StorageSystem.StorageSystem.getInstance().getWorkDetails(user, QueryOption.WaitingTasks);
                    size = tasks.Count;
                    Thread.Sleep(_refreshRate * 1000);
                }
                TaskStatus newStatus = tasks[0];
                newStatus.setTaskStatus(Status.Active);
                StorageSystem.StorageSystem.getInstance().changeWorkDetails(newStatus);
                task = tasks[0].getTaskID();
            }
            System.Console.WriteLine("SUCCESS");
        }

        /**
         * initialize the initializer object which will be used in the system objects
         */
        protected static void SetInitializer(String taskId)
        {
            System.Console.Write("$$$ Getting Constraints .. ");
            if (_operationMode == operationMode_t.Manual)
            {
                _categories  = new List<Category>();
                _constraints = new Constraints(1, false, "", ".com");
            }
            else if (_operationMode == operationMode_t.Auto)
            {
                _categories  = StorageSystem.StorageSystem.getInstance().getCategories(taskId);
                _constraints = StorageSystem.StorageSystem.getInstance().getRestrictions(taskId);
            }
            _initializer = new Initializer(taskId, _constraints, _categories);
            System.Console.WriteLine("SUCCESS");
        }

        /**
         * init the queues which will be used as link points between the threads
         */
        protected static void InitQueues(String taskId)
        {
            System.Console.Write("$$$ Initalizing Requests .. ");
            _serversQueues = new List<Queue<Url>>();
            _feedBackQueue = new Queue<Url>();

            for (int serverNum = 0; serverNum < _numWorkers; serverNum++)
            {
                _serversQueues.Add(new Queue<Url>());
            }

            // getting seeds
            if (_operationMode == operationMode_t.Manual)
            {
                foreach (string url in _seedList)
                {
                    Url task = new Url(url, 0, 100, url, 0);
                    _feedBackQueue.Enqueue(task);
                }
            }
            else if (_operationMode == operationMode_t.Auto)
            {
                List<String> seeds = StorageSystem.StorageSystem.getInstance().getSeedList(taskId);
                foreach (string url in seeds)
                {
                    Url task = new Url(url.Trim(), 0, 100, url.Trim(), 0);
                    _feedBackQueue.Enqueue(task);
                    //System.Console.WriteLine("SEED: " + url);
                }
            }
            System.Console.WriteLine("SUCCESS");
        }

        /**
         * invokes the threads to start the work
         */
        protected static void InvokeThreads()
        {
            System.Console.Write("$$$ Start Invoking threads .. ");
            // init the Frontier thread
            //Frontier frontier = new BFSFrontier(_feedBackQueue, _serversQueues);
            Frontier frontier = chooseFrontier();
            Thread frontierThread = new Thread(new ThreadStart(frontier.sceduleTasks));
            frontierThread.Start();
            _frontiersPool.Add(frontier);
            _threadsPool.Add(frontierThread);

            for (int threadNum = 0; threadNum < _numWorkers; threadNum++)
            {
                Worker worker = new Worker(_initializer, _serversQueues[threadNum], _feedBackQueue);
                Thread workerThread = new Thread(new ThreadStart(worker.run));
                workerThread.Start();
                _threadsPool.Add(workerThread);
                _workersPool.Add(worker);
            }
            System.Console.WriteLine("SUCCESS");
        }

        /**
         * terminate all the working and frontier threads
         */
        protected static void TerminateThreads()
        {
            System.Console.Write("$$$ Start Termnating threads .. ");
            _threadsPool[0].Abort();
            //_frontiersPool[0].RequestStop();
            //_threadsPool[0].Join();
            //Console.WriteLine("$ Frontier has been finished ...");

            for (int threadNum = 0; threadNum < _numWorkers; threadNum++)
            {
                _threadsPool[threadNum + 1].Abort();
                //_workersPool[threadNum].RequestStop();
                //_threadsPool[threadNum + 1].Join();
            }
            //Console.WriteLine("$ Session Terminated ...");
            _threadsPool.Clear();
            _workersPool.Clear();
            _frontiersPool.Clear();
            System.Console.WriteLine("SUCCESS");
        }

        /**
         * This method chooses the frontier as seen in the data base.
         */
        private static Frontier chooseFrontier()
        {
            String frontierType = StorageSystem.StorageSystem.getInstance().getProperty(WorkDetails.getTaskId(),
                                    TaskProperty.FRONTIER.ToString());

            String BFS = FrontierDesign.FIFO_BFS.ToString();
            String SSEv0 = FrontierDesign.RANK_SSEv0.ToString();

            if (frontierType == null) return new BFSFrontier(_feedBackQueue, _serversQueues);

            if (frontierType.Trim().Equals(BFS))
                return new BFSFrontier(_feedBackQueue, _serversQueues);
            else if (frontierType.Trim().Equals(SSEv0)) // TODO: rechange to RankFrontier
                return new RankFrointer(_feedBackQueue, _serversQueues);
            else
                return new BFSFrontier(_feedBackQueue, _serversQueues);
        }
    }
}

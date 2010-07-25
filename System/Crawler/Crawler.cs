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
        enum operationMode_t { Auto, Manual };

        private static int _numWorkers  = 4;
        private static int _refreshRate = 5;
        private static List<String>    _seedList      = new List<string>();
        private static operationMode_t _operationMode = operationMode_t.Manual;

        private static List<Category> _categories;
        private static Constraints    _constraints;
        private static Initializer    _initializer;

        private static List<Queue<Url>> _serversQueues;
        private static Queue<Url>       _feedBackQueue;

        /**
         * Set Flag Method
         * this functions gets flag and value it will process the flag parameters
         * as listed in the value
         */
        private static bool SetFlag(string flag, string value)
        {
            switch (flag)
            {
                case "numThreads":
                    try
                    {
                        _numWorkers = Convert.ToInt16(value);
                    }
                    catch (Exception e)
                    {
                        System.Console.WriteLine("ERROR: Cannot Convert to Integer Value " + value);
                        return false;
                    }
                    break;
                case "refreshRate":
                    try
                    {
                        _refreshRate = Convert.ToInt16(value);
                    }
                    catch (Exception e)
                    {
                        System.Console.WriteLine("ERROR: Cannot Convert to Integer Value " + value);
                        return false;
                    }
                    break;
                case "seedList":
                    _seedList.Clear();
                    foreach (String seed in value.Split(','))
                    {
                        _seedList.Add("http://" + seed);
                    }
                    break;
                case "operationMode":
                    if (value.Equals("Auto") == true)
                        _operationMode = operationMode_t.Auto;
                    else if (value.Equals("Manual") == true)
                        _operationMode = operationMode_t.Manual;
                    else
                    {
                        System.Console.WriteLine("ERROR: Unknown Operation Mode " + value);
                        return false;
                    }
                    break;
                default:
                    System.Console.WriteLine("ERROR: Unknown Flag " + flag);
                    return false;
            }
            return true;
        }

        /**
         * prints usage help on the screen
         */
        private static void PrintHelp()
        {
            System.Console.WriteLine("iWebCrawler Supported flags:");
            System.Console.WriteLine("-numThreads:<NUM>            {to specifiy how much workers to allocate - default is 4}");
            System.Console.WriteLine("-seedList:url1,url2,..,urln  {to specifiy the seed list of the crawler}");
            System.Console.WriteLine("-operationMode:[Auto/Manual] {Auto means that the crawler will run as service and will");
            System.Console.WriteLine("                              get it's options from the database, Manual is for manual");
            System.Console.WriteLine("                              usage for the crawler}");
            System.Console.WriteLine("-refreshRate:<NUM>           {to specifiy the statistics refresh rate in sec - default is 5}");
        }

        /**
         * parses the arguements and moves them to be processed
         */
        private static bool ParseArguements(String[] args)
        {
            foreach (String option in args)
            {
                if (option.StartsWith("-") == false || option.Contains(":") == false
                    || option.Split(':').Length != 2)
                {
                    if (option.Substring(1).Split(':')[0] == "help")
                    {
                        PrintHelp();
                        return false;
                    }
                    System.Console.WriteLine("ERROR: You have inserted option in illegal format : " + option);
                    return false;
                }

                String flag = option.Substring(1).Split(':')[0];
                String value = option.Substring(1).Split(':')[1];

                SetFlag(flag, value);
            }
            return true;
        }

        /**
         * initialize the initializer object which will be used in the system objects
         */
        private static void SetInitializer()
        {
            if (_operationMode == operationMode_t.Manual)
            {
                _categories  = new List<Category>();
                _constraints = new Constraints(1, false, "", ".com");
                _initializer = new Initializer(_constraints, _categories);
            }
        }

        /**
         * init the queues which will be used as link points between the threads
         */
        private static void InitQueues()
        {
            _serversQueues = new List<Queue<Url>>();
            _feedBackQueue = new Queue<Url>();

            for (int serverNum = 0; serverNum < _numWorkers; serverNum++)
            {
                _serversQueues.Add(new Queue<Url>());
            }

            // TODO: if the mode is auto need to reset the seedList from the db info
            // getting seeds
            if (_operationMode == operationMode_t.Manual)
            {
                foreach (string url in _seedList)
                {
                    Url task = new Url(url, 0, 100, url, 0);
                    _feedBackQueue.Enqueue(task);
                }
            }
        }

        /**
         * invokes the threads to start the work
         */
        private static void InvokeThreads()
        {
            for (int threadNum = 0; threadNum < _numWorkers; threadNum++)
            {
                Worker worker = new Worker(_initializer, _serversQueues[threadNum], _feedBackQueue);
                Thread workerThread = new Thread(new ThreadStart(worker.run));
                workerThread.Start();
            }

            // init the Frontier thread
            Frontier frontier = new Frontier(_feedBackQueue, _serversQueues);
            Thread frontierThread = new Thread(new ThreadStart(frontier.sceduleTasks));
            frontierThread.Start();
        }
        /*
        public static void Main(String[] args)
        {
            bool toContinue = ParseArguements(args);
            if (toContinue == false) return;

            while (true)
            {
                // getting init data
                SetInitializer();
                
                // init queues
                InitQueues();

                // initing worker and frontier threads
                InvokeThreads();
                
                // polling to the user requests
                while (true)
                {
                    Thread.Sleep(_refreshRate * 1000);
                    StatusDisplay.DisplayOnScreen(_feedBackQueue, _serversQueues);
                }
            }
        }*/
    }
}

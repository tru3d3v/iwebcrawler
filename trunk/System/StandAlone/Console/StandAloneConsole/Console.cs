using System;
using System.Collections.Generic;
using System.Text;
using CrawlerNameSpace.Tests;
using CrawlerNameSpace.Utilities;
using System.Threading;
using CrawlerNameSpace.StorageSystem;

namespace CrawlerNameSpace
{
    class StandAloneConsole : Crawler
    {
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
                    {
                        _operationMode = operationMode_t.Auto;
                        WorkDetails.setOperationMode(operationMode_t.Auto);
                    }
                    else if (value.Equals("Manual") == true)
                    {
                        _operationMode = operationMode_t.Manual;
                        WorkDetails.setOperationMode(operationMode_t.Manual);
                    }
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
         * Sets the number of threads,if it is mode of operation is Auto
         */
        public static void SetNumberOfThreads()
        {
            if (_operationMode == operationMode_t.Auto)
            {
                String numberOfThreads = StorageSystem.StorageSystem.getInstance().getProperty(WorkDetails.getTaskId(), TaskProperty.THREADS.ToString());
                if ((numberOfThreads != null) && (numberOfThreads != ""))
                    if (Convert.ToInt16(numberOfThreads)>0)
                        SetFlag("numThreads", numberOfThreads);
            }
        }

        /**
         * Main method of the console application
         */
        public static void Main(String[] args)
        {
            bool toContinue = ParseArguements(args), needToRestart = false;
            if (toContinue == false) return;
            Queue<int> keepAlive = new Queue<int>();
            String currentUser = "5df16977-d18e-4a0a-b81b-0073de3c9a7f", currentTask = "";

            LogDebuggerControl.getInstance().debugCategorization = true;
            LogDebuggerControl.getInstance().debugCategorizationInRanker = false;
            LogDebuggerControl.getInstance().debugRanker = true;

            while (true)
            {
                // select which task to invoke
                SelectTask(ref currentUser, ref currentTask);

                //update the WorkDetails class with the new taskId
                WorkDetails.setTaskId(currentTask);

                // getting init data
                SetInitializer(currentTask);

                // init queues
                InitQueues(currentTask);

                //Set Number of Threads
                SetNumberOfThreads();
                
                // initing worker and frontier threads
                InvokeThreads();

                // polling to the user requests
                while (needToRestart == false)
                {
                    Thread.Sleep(_refreshRate * 1000);
                    StatusDisplay.DisplayOnScreen(_feedBackQueue, _serversQueues);
                    if (_operationMode == operationMode_t.Auto)
                    {
                        List<TaskStatus> tasks =
                            StorageSystem.StorageSystem.getInstance().getWorkDetails(currentUser, QueryOption.ActiveTasks);

                        needToRestart = true;
                        foreach (TaskStatus task in tasks)
                        {
                            if (task.getTaskID() == currentTask)
                            {
                                task.setTaskElapsedTime(task.getTaskElapsedTime() + _refreshRate);
                                StorageSystem.StorageSystem.getInstance().changeWorkDetails(task);
                                needToRestart = false;
                                continue;
                            }
                        }
                    }
                }

                // Terminate all the threads
                TerminateThreads();
                needToRestart = false;
                RuntimeStatistics.resetStatistics();
            }    
        }
    }
}

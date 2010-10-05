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
         * This method sets ALL the constants needed for the task.
         */
        public static void SetAllConstants()
        {
            //sets the number of threads
            SetNumberOfThreads();

            //get anchor options
            CategorizerOptions anchorOptions = getOptions("anchor");
            RankerOptions.ANC_ALPHA = anchorOptions.ALPHA;
            RankerOptions.ANC_BETA = anchorOptions.BETA;
            RankerOptions.ANC_GAMMA = anchorOptions.GAMMA;
            RankerOptions.ANC_MIN = anchorOptions.MIN_WORDS_LIMIT;
            RankerOptions.ANC_PENLTY = anchorOptions.MIN_WORDS_PENLTY;

            //get nearby options 
            CategorizerOptions nearbyOptions = getOptions("nearby");
            RankerOptions.NER_ALPHA = nearbyOptions.ALPHA;
            RankerOptions.NER_BETA = nearbyOptions.BETA;
            RankerOptions.NER_GAMMA = nearbyOptions.GAMMA;
            RankerOptions.NER_MIN = nearbyOptions.MIN_WORDS_LIMIT;
            RankerOptions.NER_PENLTY = nearbyOptions.MIN_WORDS_PENLTY;

            //get category Options
            CategorizerOptions categoryOptions = getOptions("Category");
            RankerOptions.CAT_ALPHA = categoryOptions.ALPHA;
            RankerOptions.CAT_BETA = categoryOptions.BETA;
            RankerOptions.CAT_GAMMA = categoryOptions.GAMMA;
            RankerOptions.CAT_MIN = categoryOptions.MIN_WORDS_LIMIT;
            RankerOptions.CAT_PENLTY = categoryOptions.MIN_WORDS_PENLTY;

            //get Ranker options
            getRankerOptions();
            //set symmitric line
            String symmetric = StorageSystem.StorageSystem.getInstance().getProperty(WorkDetails.getTaskId()
                                            , TaskProperty.SYMMETRIC_LINE.ToString());
            //TODO:continue the assigning of symmitric line
        }

        /**
         * This method creates Categorizer Options and returns it.
         * The values of the variables of the new object are brought from the data base.
         * Note : In case the operation mode is manual the variables will be default ones,
         *        such is the case when the returns values from the data base are nulls.
         */
        private static CategorizerOptions getOptions(String optionsType)
        {
            CategorizerOptions options = new CategorizerOptions();

            if (WorkDetails.getOperationMode() == operationMode_t.Auto)
            {
                String alphaSearch = null, bettaSearch = null, gammaSearch = null, minSearch = null, penaltySearch = null;
                switch (optionsType)
                {
                    case "anchor":
                        alphaSearch = TaskProperty.ANC_ALPHA.ToString();
                        bettaSearch = TaskProperty.ANC_BETA.ToString();
                        gammaSearch = TaskProperty.ANC_GAMMA.ToString();
                        minSearch = TaskProperty.ANC_MIN.ToString();
                        penaltySearch = TaskProperty.ANC_PENLTY.ToString();
                        break;
                    case "Category":
                        alphaSearch = TaskProperty.CAT_ALPHA.ToString();
                        bettaSearch = TaskProperty.CAT_BETA.ToString();
                        gammaSearch = TaskProperty.CAT_GAMMA.ToString();
                        minSearch = TaskProperty.CAT_MIN.ToString();
                        penaltySearch = TaskProperty.CAT_PENLTY.ToString();
                        break;
                    case "nearby":
                        alphaSearch = TaskProperty.NER_ALPHA.ToString();
                        bettaSearch = TaskProperty.NER_BETA.ToString();
                        gammaSearch = TaskProperty.NER_GAMMA.ToString();
                        minSearch = TaskProperty.NER_MIN.ToString();
                        penaltySearch = TaskProperty.NER_PENLTY.ToString();
                        break;
                    default:
                        goto case "Category";
                }

                String alpha = StorageSystem.StorageSystem.getInstance().getProperty(WorkDetails.getTaskId(), alphaSearch);
                String betta = StorageSystem.StorageSystem.getInstance().getProperty(WorkDetails.getTaskId(), bettaSearch);
                String gamma = StorageSystem.StorageSystem.getInstance().getProperty(WorkDetails.getTaskId(), gammaSearch);
                String min = StorageSystem.StorageSystem.getInstance().getProperty(WorkDetails.getTaskId(), minSearch);
                String penalty = StorageSystem.StorageSystem.getInstance().getProperty(WorkDetails.getTaskId(), penaltySearch);

                if (isRealNum(alpha))
                    options.ALPHA = Convert.ToDouble(alpha);
                if (isRealNum(betta))
                    options.BETA = Convert.ToDouble(betta);
                if (isRealNum(gamma))
                    options.GAMMA = Convert.ToDouble(gamma);
                if (isRealNum(min))
                    options.MIN_WORDS_LIMIT = Convert.ToDouble(min);
                if (isRealNum(penalty))
                    options.MIN_WORDS_PENLTY = Convert.ToDouble(penalty);
            }

            return options;
        }

        /**
         * This method creates a Ranker Options object,and sets the options of the Ranker, and returns
         * the created object.
         * It gets the options from the data base.
         * note:In case the returned values from the data base are nulls or
         *      the operation mode of the crawler is manual this 
         *      method will set defualt numbers.
         */
        private static void getRankerOptions()
        {

            if (WorkDetails.getOperationMode() == operationMode_t.Auto)
            {
                String alpha = StorageSystem.StorageSystem.getInstance().getProperty(WorkDetails.getTaskId(),
                                TaskProperty.RAN_ALPHA.ToString());
                String betta = StorageSystem.StorageSystem.getInstance().getProperty(WorkDetails.getTaskId(),
                                TaskProperty.RAN_BETA.ToString());
                String gamma = StorageSystem.StorageSystem.getInstance().getProperty(WorkDetails.getTaskId(),
                                TaskProperty.RAN_GAMMA.ToString());
                String delta = StorageSystem.StorageSystem.getInstance().getProperty(WorkDetails.getTaskId(),
                                TaskProperty.RAN_DELTA.ToString());

                if (isRealNum(alpha))
                    RankerOptions.ALPHA = Convert.ToDouble(alpha);
                if (isRealNum(betta))
                    RankerOptions.BETTA = Convert.ToDouble(betta);
                if (isRealNum(gamma))
                    RankerOptions.GAMMA = Convert.ToDouble(gamma);
                if ((delta != null) && (delta != "") && (!Convert.IsDBNull(delta)) && ((Convert.ToInt16(delta)) >= 0))
                    RankerOptions.ConfidenceLevelOfAnchor = Convert.ToInt16(delta);
            }
        }

        /**
         * This method check wether the given string is  a real number bigger than 0 
         * Or not.
         */
        private static bool isRealNum(String num)
        {
            double realNum = Convert.ToDouble(num);
            if ((num != null) && (num != "") && (!(Convert.IsDBNull(num))) && (realNum >= 0))
                return true;
            else
                return false;
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

            LogDebuggerControl.getInstance().debugCategorization = false;
            LogDebuggerControl.getInstance().debugCategorizationInRanker = false;
            LogDebuggerControl.getInstance().debugRanker = false;

            while (true)
            {
                // select which task to invoke
                SelectTask(ref currentUser, ref currentTask);

                //update the WorkDetails class with the new taskId
                WorkDetails.setTaskId(currentTask);

                //Set ALL constants of the task
                SetAllConstants();

                // getting init data
                SetInitializer(currentTask);

                // init queues
                InitQueues(currentTask);
                
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

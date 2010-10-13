using System;
using System.Collections.Generic;
using System.Text;
using CrawlerNameSpace.Utilities;
using System.Threading;
using System.IO;

namespace CrawlerNameSpace
{
    /**
     * This class represent one worker thread which accepts requests to crawl urls and 
     * start processing them by fetching them from the internet moving them to the processor
     * in order to process thier content, after that the results will be sent to the storage
     * and the feed of the comming urls will be added to the frontier Queue
     */
    public class Worker
    {
        // url queues of the tasks to crawl and the feed to the frontier
        Queue<Url> _tasks, _feedback;

        // queue query timer 
        int _timer;
        // number of iterations to do while checking the status
        private int _checkStatusLimit;

        // managers of the protocols
        FetcherManager _fetchers;
        ResourceProcessorManager _processors;

        /**
         * constructs a new worker with the specified initialdata, it will work on the tasks
         *  and return the feed to the feedback, note it won't create new queues it will use
         *  the passed arguments - and they may need to be thread safe
         */
        public Worker(Initializer initialData, Queue<Url> tasks, Queue<Url> feedback)
        {
            _tasks    = tasks;
            _feedback = feedback;

            // sets default timer
            _timer    = 1000;

            // initailizing the fetcher - page downloaders
            _fetchers = new FetcherManager();
            HttpResourceFetcher httpfetcher = new HttpResourceFetcher();
            _fetchers.addProtocol("http", httpfetcher);

            // initailizing the processors - will process the downloaded urls
            _processors = new ResourceProcessorManager();
           
            HtmlPageCategorizationProcessor htmlProcessor = new HtmlPageCategorizationProcessor(initialData, feedback);
            _processors.attachProcessor("PageProc", htmlProcessor);

            _checkStatusLimit = 0;
        }
        
        /**
         * it will invoke the worker to start working on the tasks - never returns
         */ 
        public void run()
        {
            int requestNum = 0, timeoutCounter = 0;
            bool needToTerminate = false;
            TimeSpan totalProcessTime;
            while (needToTerminate == false)
            {
                DateTime startTime = DateTime.Now;
                try
                {
                    //System.Console.WriteLine("-<>--------------------------------------------------------------------------");
                    Url task = SyncAccessor.getFromQueue<Url>(_tasks, _timer);
                    
                    //System.Console.WriteLine(" Start Working on : " + task.getUrl() + " ...");
                    ResourceContent content = _fetchers.fetchResource(task.getUrl());
                    
                    if (content.isValid() != true)
                    {
                        timeoutCounter++;
                        //System.Console.WriteLine(" Fetch Failed Ignoring ... ");
                        continue;
                    }
                    
                    //System.Console.WriteLine(" Fetched Successfully ... ");
                    
                    ResourceContent modifiedContent = new ResourceContent(content.getResourceUrl(), content.getResourceType()
                        , content.getResourceContent(), content.getReturnCode(), task.getRank());

                    DateTime startProcess = DateTime.Now;
                    _processors.processResource(modifiedContent);
                    DateTime endProcess = DateTime.Now;
                    totalProcessTime = endProcess - startProcess;
                    //System.Console.WriteLine(" URL Processed Successfully ... ");
                    
                    System.Console.WriteLine(" URL Processed Successfully ... ");
                }
                catch (Exception e)
                {
                    //System.Console.WriteLine("[Exception Happened] " + e);
                    RuntimeStatistics.addToErrors(1);
                    continue;
                }
                DateTime endTime = DateTime.Now;
                TimeSpan totalRequestTime = endTime - startTime;

                // write request time to timing log file
                StreamWriter sw = new
                        StreamWriter("_DEBUG_INFO_TIMING@" + System.Threading.Thread.CurrentThread.ManagedThreadId + ".txt", true);
                sw.WriteLine(" TIMING FOR REQ - " + requestNum++ + " takes about " + totalRequestTime.TotalSeconds + " s, Processed At " + totalProcessTime.TotalSeconds + " s");
                sw.Close();
            }
        }

        /**
         * sets the polling timeout for the queries on the queues in ms
         */
        public void setPollingTimer(int period)
        {
            _timer = period;
        }
    }
}

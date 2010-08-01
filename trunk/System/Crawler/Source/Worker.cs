using System;
using System.Collections.Generic;
using System.Text;
using CrawlerNameSpace.Utilities;
using System.Threading;

namespace CrawlerNameSpace
{
    /**
     * This class represent one worker thread which accepts requests to crawl urls and 
     * start processing them by fetching them from the internet moving them to the processor
     * in order to process thier content, after that the results will be sent to the storage
     * and the feed of the comming urls will be added to the frontier Queue
     */
    class Worker
    {
        // url queues of the tasks to crawl and the feed to the frontier
        Queue<Url> _tasks, _feedback;

        // queue query timer 
        int _timer;
        // number of iterations to do while checking the status
        private int _checkStatusLimit;

        // needed in order to keep alive status of the current worker thread
        private volatile bool _shouldStop;

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

            _shouldStop = false;

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
            int iterations = 0;
            bool needToTerminate = false;
            while (needToTerminate == false)
            {
                try
                {
                    //System.Console.WriteLine("-<>--------------------------------------------------------------------------");
                    Url task = SyncAccessor.getFromQueue<Url>(_tasks, _timer);
                    //System.Console.WriteLine(" Start Working on : " + task.getUrl() + " ...");
                    ResourceContent content = _fetchers.fetchResource(task.getUrl());
                    if (content.isValid() != true)
                    {
                        //System.Console.WriteLine(" Fetch Failed Ignoring ... ");
                        continue;
                    }
                    //System.Console.WriteLine(" Fetched Successfully ... ");

                    ResourceContent modifiedContent = new ResourceContent(content.getResourceUrl(), content.getResourceType()
                        , content.getResourceContent(), content.getReturnCode(), task.getRank());
                    _processors.processResource(modifiedContent);
                    //System.Console.WriteLine(" URL Processed Successfully ... ");
                    iterations++;
                    if (iterations >= _checkStatusLimit)
                    {
                        iterations = 0;
                        if (_shouldStop)
                        {
                            System.Console.WriteLine(" Recieved should Stop in worker thread");
                            needToTerminate = true;
                        }
                    }
                }
                catch (Exception e)
                {
                    //System.Console.WriteLine("[Exception Happened] " + e);
                    RuntimeStatistics.addToErrors(1);
                    continue;
                }
            }
        }

        /**
         * can be called in order to stop work on the worker
         */
        public void RequestStop()
        {
            _shouldStop = true;
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

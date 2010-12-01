using System;
using System.Collections.Generic;
using System.Text;
using CrawlerNameSpace.Utilities;
using System.Threading;

namespace CrawlerNameSpace
{
    /**
     * This class is responsible for ordering the tasks (urls) that the crawler have to 
     * visit in the next steps, so it can order the requests, remove already visited urls
     * and so;
     * NOTE: Currently implemented as dummy
     */
    public class Frontier
    {
        private Queue<Url> _tasksQueue;
        private List<Queue<Url>> _serversQueues;

        // queue query timer in msec
        private int _timer;
        // number of requests which can reside in the thread queue (max.)
        private int _limit;
        // number of iterations to do while checking the status
        private int _checkStatusLimit;

        // needed in order to keep alive status of the frontier thread
        private volatile bool _shouldStop;

        /**
         * constructs a new fronier instance which will be linked to the tasks queue 
         * and the specified server queue list, so the frontier will schedule it's tasks
         * between the servers
         */
        public Frontier(Queue<Url> tasksQueue, List<Queue<Url>> serversQueues)
        {
            _tasksQueue = tasksQueue;
            _serversQueues = serversQueues;
            _timer = 250;
            _limit = 100;
            _checkStatusLimit = 0;
            _shouldStop = false;
        }

        /**
         * This method scedules the tasks given to the server queues in order to be processed
         * if there is no tasks so the method will wait until there is something to be processed
         * NOTE: This method never returns
         */
        public void sceduleTasks()
        {
            Dictionary<String, String> dictionary = new Dictionary<String, String>();
            int serverTurn = 0, iterations = 0;
            bool getNewRequest = true, needToTerminate = false;;
            Url request = null;

            while (needToTerminate == false)
            {
                try
                {
                    if (getNewRequest)
                    {
                        request = SyncAccessor.getFromQueue<Url>(_tasksQueue, _timer);
                        getNewRequest = false;
                    }
                    getNewRequest = true;
                    if (dictionary.ContainsKey(request.getUrl())) continue;
                    dictionary.Add(request.getUrl(), null);

                    if (SyncAccessor.queueSize<Url>(_serversQueues[serverTurn]) <= _limit)
                    {
                        SyncAccessor.putInQueue<Url>(_serversQueues[serverTurn], request);
                    }
                    else
                    {
                        getNewRequest = false;
                    }
                    serverTurn = (serverTurn + 1) % _serversQueues.Count;
                    iterations++;
                    if (iterations >= _checkStatusLimit)
                    {
                        iterations = 0;
                        if (_shouldStop)
                        {
                            //System.Console.WriteLine("Frontier Thread recieved should stop");
                            needToTerminate = true;
                        }
                    }
                }
                catch (Exception e)
                {
                    RuntimeStatistics.addToErrors(1);
                }
            }
        }

        /**
         * can be called in order to stop work on the frontier
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

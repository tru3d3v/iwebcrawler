using System;
using System.Collections.Generic;
using System.Text;
using CrawlerNameSpace.Utilities;
using System.Threading;

namespace CrawlerNameSpace
{
    /**
     * This is an abstract class for frontier, it supply the basic functionality
     * and the basic fields
     */
    public abstract class Frontier
    {
        protected Queue<Url> _tasksQueue;
        protected List<Queue<Url>> _serversQueues;

        // queue query timer in msec
        protected int _timer;
        // number of requests which can reside in the thread queue (max.)
        protected int _limit;
        // number of iterations to do while checking the status
        protected int _checkStatusLimit;

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
            _limit = 10;
            _checkStatusLimit = 0;
        }

        /**
         * this method should be responsible about scehduling tasks for the workers
         */
        public abstract void sceduleTasks();

        /**
         * sets the polling timeout for the queries on the queues in ms
         */
        public void setPollingTimer(int period)
        {
            _timer = period;
        }
    }
}

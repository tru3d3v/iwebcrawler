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
     * NOTE: This is RANK Trie Based implementation
     */
    public class RankFrointer : Frontier
    {
        static private int MAX_INSERTS_IN_TIME = 100;
        private RankingTrie _rankingTrie;

        /**
         * constructs a new fronier instance which will be linked to the tasks queue 
         * and the specified server queue list, so the frontier will schedule it's tasks
         * between the servers
         */
        public RankFrointer(Queue<Url> tasksQueue, List<Queue<Url>> serversQueues) 
            : base(tasksQueue, serversQueues)
        {
            _rankingTrie = new RankingTrie();
        }

        /**
         * This method scedules the tasks given to the server queues in order to be processed
         * if there is no tasks so the method will wait until there is something to be processed
         * NOTE: This method never returns
         */
        public override void sceduleTasks()
        {
            int serverTurn = 0;
            bool getNewRequest = true, needToTerminate = false; ;
            Url request = null;

            while (needToTerminate == false)
            {
                try
                {
                    int inserts = 0;
                    while (SyncAccessor.queueSize<Url>(_tasksQueue) != 0 && inserts < MAX_INSERTS_IN_TIME)
                    {
                        request = SyncAccessor.getFromQueue<Url>(_tasksQueue, _timer);
                        _rankingTrie.add(request);
                        inserts++;
                    }

                    if (getNewRequest)
                    {
                        RuntimeStatistics.setFrontierUrls(_rankingTrie.count());
                        if (_rankingTrie.count() == 0)
                        {
                            Thread.Sleep(_timer);
                            continue;
                        }
                        request = _rankingTrie.pop();
                        getNewRequest = false;
                    }
                    getNewRequest = true;

                    if (SyncAccessor.queueSize<Url>(_serversQueues[serverTurn]) < _limit)
                    {
                        SyncAccessor.putInQueue<Url>(_serversQueues[serverTurn], request);
                    }
                    else
                    {
                        getNewRequest = false;
                    }
                    serverTurn = (serverTurn + 1) % _serversQueues.Count;
                }
                catch (Exception e)
                {
                    RuntimeStatistics.addToErrors(1);
                }
            }
        }
    }
}

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
     * NOTE: This is BFS implementation
     */
    public class BFSFrontier : Frontier
    {
        // how many retries to put the request in specific queue while it's full
        public const int MAX_RETRY_COUNTER = 1;

        /**
         * constructs a new fronier instance which will be linked to the tasks queue 
         * and the specified server queue list, so the frontier will schedule it's tasks
         * between the servers
         */
        public BFSFrontier(Queue<Url> tasksQueue, List<Queue<Url>> serversQueues) 
            : base(tasksQueue, serversQueues)
        {
        }

        /**
         * This method scedules the tasks given to the server queues in order to be processed
         * if there is no tasks so the method will wait until there is something to be processed
         * NOTE: This method never returns
         */
        public override void sceduleTasks()
        {
            Dictionary<String, String> dictionary = new Dictionary<String, String>();
            int serverTurn = 0;
            Url request = null;

            while (true)
            {
                try
                {
                    // get new request
                    SyncAccessor.getSlot(2, 0);
                    request = SyncAccessor.getFromQueue<Url>(_tasksQueue, _timer);

                    // handle the request
                    if (dictionary.ContainsKey(request.getUrl()))
                    {
                        // if it already exists need to pick another one
                        continue;
                    }
                    else
                    {
                        // if not just mark it as old and continue
                        dictionary.Add(request.getUrl(), null);
                    }

                    // now there's a new request we should put it in the server queues
                    bool needToPutRequest = true;
                    int retryCount = 0;
                    while (needToPutRequest)
                    {
                        SyncAccessor.getSlot(2, 0);
                        if (SyncAccessor.queueSize<Url>(_serversQueues[serverTurn]) < _limit)
                        {
                            needToPutRequest = false;
                            SyncAccessor.getSlot(2, 0);
                            SyncAccessor.putInQueue<Url>(_serversQueues[serverTurn], request);
                        }
                        else
                        {
                            retryCount++;
                            if (retryCount > MAX_RETRY_COUNTER)
                            {
                                serverTurn = (serverTurn + 1) % _serversQueues.Count;
                            }
                            else
                            {
                                Thread.Sleep(_timer * 3);
                            }
                        }
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

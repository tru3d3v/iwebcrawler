using System;
using System.Collections.Generic;
using System.Text;
using CrawlerNameSpace.Utilities;
using System.Threading;

namespace CrawlerNameSpace.Utilities
{
    /**
     * This class supply methods which can access the shared resources in thread safe
     * way
     */
    public class SyncAccessor
    {
        private static Random randomizer = new Random();

        static public void getSlot(int dim, int id)
        {
            id = id % dim;
            int second = DateTime.Now.Second;
            int milisec = DateTime.Now.Millisecond % 1000;
            if (id == (second % dim)) return;
            int time = id - (second % dim);
            Thread.Sleep(Math.Max(time * 1000 - milisec, 0));
        }

        /**
         * puts the elemnt in the queue, this method is thread safe so it can be invoked 
         *  via more than one thread which want access to the shared resource
         */
        static public void putInQueue<T>(Queue<T> queue, T elemnt)
        {
            lock (queue)
            {
                queue.Enqueue(elemnt);
            }
        }

        /**
         * puts the elemnt in the queue, this method is thread safe so it can be invoked 
         *  via more than one thread which want access to the shared resource
         */
        static public int queueSize<T>(Queue<T> queue)
        {
            lock (queue)
            {
                return queue.Count;
            }
        }

        /**
         * returns the elemnt in the queue, this method is thread safe so it can be invoked 
         *  via more than one thread which want access to the shared resource
         * NOTE: if the shared resource is empty it will wait the time and retry.
         */
        static public T getFromQueue<T>(Queue<T> queue, int time)
        {
            bool toSleep = false;
            while (true)
            {
                T elemnt;
                if (toSleep) Thread.Sleep(time + randomizer.Next(time / 2));
                toSleep = false;

                lock (queue)
                {
                    if (queue.Count == 0)
                    {
                        toSleep = true;
                        continue;
                    }
                    elemnt = queue.Dequeue();
                }            
                return elemnt;
            }
        }
    }
}

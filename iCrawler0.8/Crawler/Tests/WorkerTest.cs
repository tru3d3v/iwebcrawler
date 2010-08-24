using System;
using System.Collections.Generic;
using System.Text;
using CrawlerNameSpace.Utilities;

namespace CrawlerNameSpace.Tests
{
    class WorkerTest
    {
        public static void MainTest()
        {
            List<Category> categories = new List<Category>();
            Constraints constraints   = new Constraints(5, true, "", "www.facebook.com");
            Initializer initializer   = new Initializer(constraints, categories);

            Url task1 = new Url("http://www.facebook.com/admirer4", 34243432, 35, "http://www.facebook.com/", 34243432);
            //Url task2 = new Url("http://www.nana10.co.il/", 34223432, 35, "http://www.nana10.co.il/", 34223432);

            Queue<Url> taskQueue = new Queue<Url>();
            taskQueue.Enqueue(task1);
            //taskQueue.Enqueue(task2);

            Queue<Url> feedBackQueue = new Queue<Url>();

            Worker worker = new Worker(initializer, taskQueue, feedBackQueue);
            worker.run();
        }
    }
}

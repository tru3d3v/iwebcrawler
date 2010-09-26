using System;
using System.Collections.Generic;
using System.Text;
using CrawlerNameSpace.Utilities;

namespace CrawlerNameSpace.Tests
{
    class RankerTest
    {
        private ResourceContent resource = new ResourceContent("www.ynet." +
            "co.il", ResourceType.HtmlResource, "This is an example test content of this resource", 200, 0);

        private Ranker testRanker = new Ranker(new Categorizer(new List<Category>()));

        public void Test1()
        {
            LinkItem item = new LinkItem();

            if (testRanker.rankUrl(resource,item) == 0)
            {
                Console.WriteLine("rankUrl(int parentRank, String parentContent, String url) PASSED");
            }
            else
            {
                Console.WriteLine("rankUrl(int parentRank, String parentContent, String url) FAILED");
                Console.WriteLine("Should have returned 0");
            }
        }

        /**
         * This method is the main test that gathers all the tests.
         */
        public static void MainTest()
        {
            RankerTest rankerTest = new RankerTest();
            rankerTest.Test1();
        }
    }
}

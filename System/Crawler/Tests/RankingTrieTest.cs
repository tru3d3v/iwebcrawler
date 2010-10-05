using System;
using System.Collections.Generic;
using System.Text;
using CrawlerNameSpace.Utilities;

namespace CrawlerNameSpace.Tests
{
    public class RankingTrieTest
    {
        RankingTrie testTrie = new RankingTrie();

        public void Test1()
        {
            testTrie.add(new Url("www.ynet.co.il",0,45,"www.ynet.co.il",1));
            testTrie.add(new Url("www.ynet.co.il", 0, 50, "www.ynet.co.il", 1));
            testTrie.add(new Url("www.ynet.co.il", 0, 78, "www.ynet.co.il", 1));
            testTrie.add(new Url("", 0, 45, "www.ynet.co.il", 1));
            //testTrie.add(new Url(null, 0, 60, "www.ynet.co.il", 1));
            testTrie.add(new Url("www.facebook.com", 0, 45, "www.facebook.com", 1));
            testTrie.add(new Url("www.nana10.co.il", 0, 30, "www.ynet.co.il", 1));
            testTrie.add(new Url("www.ynet.co.il", 0, 78, "www.ynet.co.il", 1));
            testTrie.add(new Url("www.nana10.co.il", 0, 80, "www.nana10.co.il", 1));
            testTrie.add(new Url("www.nana10.co.il", 0, 80, "www.nana10.co.il", 1));
            testTrie.add(null);

            Console.WriteLine("The maximum rank is: " + testTrie.getMaxRank());
            Console.WriteLine("The minimum ramk is: " + testTrie.getMinRank());

            Console.WriteLine("The maximum url is : " + testTrie.pop().getUrl());

            Url pop = testTrie.pop();
            while (pop != null)
            {
                Console.WriteLine("The new max is : " + testTrie.getMaxRank());
                Console.WriteLine("The new min is : " + testTrie.getMinRank());
                Console.WriteLine("The new maximum url is :" + pop.getUrl());
                pop = testTrie.pop();
            }
        }

        public void Test2()
        {
            char[] arbitrary=new char[20];

            DateTime startTime = DateTime.Now;
            for (int i = 0; i < 500000; i++)
            {
                testTrie.add(new Url("www.ynet.co.il", 0, 45, "www.ynet.co.il", 1));
                testTrie.add(new Url("www.ynet.co.il", 0, 50, "www.ynet.co.il", 1));
                testTrie.add(new Url("www.ynet.co.il", 0, 78, "www.ynet.co.il", 1));
                testTrie.add(new Url("", 0, 45, "www.ynet.co.il", 1));
                //testTrie.add(new Url(null, 0, 60, "www.ynet.co.il", 1));
                testTrie.add(new Url("www.facebook.com", 0, 45, "www.facebook.com", 1));
                testTrie.add(new Url("www.nana10.co.il", 0, 30, "www.ynet.co.il", 1));
                testTrie.add(new Url("www.ynet.co.il", 0, 78, "www.ynet.co.il", 1));
                testTrie.add(new Url("www.nana10.co.il", 0, 80, "www.nana10.co.il", 1));
                testTrie.add(new Url("www.nana10.co.il", 0, 80, "www.nana10.co.il", 1));
                testTrie.add(null);
            }
            DateTime endTime = DateTime.Now;
            TimeSpan totalRequest = endTime - startTime;
        }

        private char[] generateString()
        {
            char[] arb = new char[20];
            return arb;
        }
    }
}

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

            char c;
            for (int i = 0; i < 93; i++)
            {
                c = Convert.ToChar(i+33);
                Console.WriteLine(c);
            }
        }

        public void Test2()
        {
            //char[] arbitrary=new char[20];

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
            char[] arb = new char[50];
            Random rand = new Random();
            for (int i = 0; i < 50; i++)
            {
                
                int randNum = rand.Next(33, 126);
                arb[i] = Convert.ToChar(randNum);
            }
            return arb;
        }

        public void test3()
        {
            DateTime startTime = DateTime.Now;
            //DateTime startAddingTime = DateTime.Now;

            Random rand = new Random();

            for (int i = 0; i < 5000000; i++)
            {
                char[] arb = generateString();
                String url = new String(arb);

                //if ((i % 1000) == 0) Console.WriteLine(url);
                testTrie.add(new Url(url, 0,rand.Next(0, 100), url, i));
            }
            DateTime endAddingTime = DateTime.Now;

            TimeSpan addingTime = endAddingTime - startTime;
            
            Url pop = testTrie.pop();
            int k = 0;
            while (pop != null)
            {
                //Console.WriteLine("The new max is : " + testTrie.getMaxRank());
                //Console.WriteLine("The new min is : " + testTrie.getMinRank());
                if ((k % 10) == 0)
                    Console.WriteLine("The new maximum url is :" + pop.getUrl());
                pop = testTrie.pop();
                k++;
            }
             
            DateTime endAllTime = DateTime.Now;
            TimeSpan popingTime = endAllTime - endAddingTime;
            TimeSpan totalTime = endAllTime - startTime;

        }

        public void test4()
        {
            for (int i = 0; i < 100000; i++)
            {
                testTrie.add(new Url("www.game.com", 0, 100, "www.game.com", 3));
            }

            Url pop = testTrie.pop();

            if (pop != null) Console.WriteLine(pop.getUrl() + " " + pop.getRank());

        }

        public void test5()
        {
            Random rand = new Random();
            for (int i = 0; i < 600000; i++)
            {
                testTrie.add(new Url(i.ToString(), i, rand.Next(0, 100), i.ToString(), 600000 - i));
            }

            Url pop = testTrie.pop();
            int k = 0;
            while (pop != null)
            {
                if ((k % 100) == 0)
                    Console.WriteLine("The new maximum url is :" + pop.getUrl());
                pop = testTrie.pop();
                k++;
            }
        }
    }
}

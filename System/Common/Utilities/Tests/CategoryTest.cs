using System;
using System.Collections.Generic;
using System.Text;

namespace CrawlerNameSpace.Utilities.Tests
{
    public class CategoryTest
    {
        private Category testCategory = new Category("012", "sport", "soccer", new List<string>("ball goal sport play grass team".Split(new char[] { ' ' })), 75); 
        
        /**
         * This method tests the getCategoryID()method.
         */
        public void test1()
        {
            if (testCategory.getCategoryID() == "012")
            {
                Console.WriteLine("getCategoryID() PASSED");
            }
            else
            {
                Console.WriteLine("getCategoryID() FAILED");
            }
        }
        
        /**
         * This method tests the getParentName() method.
         */
        public void test2()
        {
            if (testCategory.getParentName() == "sport")
            {
                Console.WriteLine("getParentName() PASSED");
            }
            else
            {
                Console.WriteLine("getParentName() FAILED");
            }
        }

        /**
         * This method tests the getCatrgoryName() method.
         */
        public void test3()
        {
            if (testCategory.getCatrgoryName() == "soccer")
            {
                Console.WriteLine("getCatrgoryName() PASSED");
            }
            else
            {
                Console.WriteLine("getCatrgoryName() FAILED");
            }
        }

        /**
         * This method tests the  getConfidenceLevel() method.
         */
        public void test4()
        {
            if (testCategory.getConfidenceLevel() == 75)
            {
                Console.WriteLine("getConfidenceLevel() PASSED");
            }
            else
            {
                Console.WriteLine("getConfidenceLevel() FAILED");
            }
        }

        /**
         * This method tests the getKeywordList() method.
         */
        public void test5()
        {
            List<String> checkList =new List<string>(("ball goal sport " +
                "play grass team").Split(new char[] { ' ' }));
            List<String> returnedList= testCategory.getKeywordList();
            foreach (String keyword in checkList)
            {
                if (!(returnedList.Contains(keyword)))
                {
                    Console.WriteLine("getKeywordList() FAILED");
                    Console.WriteLine("The returned KeywordLIst should have contained the word:" + keyword); 
                }
            }
            Console.WriteLine("getKeywordList() PASSED");
        }

        /*
         * This method tests the getMatchLevel(List<String> wordList) method.
         */
        public void test6()
        {
            Console.WriteLine("The match level of :");
            Console.WriteLine("Association football or soccer, is a team sport played between two " +
            "teams of eleven players ball. sport grass  a goal the ball goal. play,"+
            "the goalkeepers  players ");
            String checkWordList = "Association football or soccer, " + 
                "is a team sport played between" +
                "two teams of eleven players ball. sport grass  a goal the ball goal." + 
                " play, the goalkeepers  players";
            Console.WriteLine("is: " + testCategory.getMatchLevel(checkWordList,2500,5000,false));
        }

        /**
          * This method is the main test that gathers all the tests.
          */
        public static void MainTest()
        {
            CategoryTest testingCategory = new CategoryTest();
            testingCategory.test1();
            testingCategory.test2();
            testingCategory.test3();
            testingCategory.test4();
            testingCategory.test5();
            testingCategory.test6();
        }
    }
}

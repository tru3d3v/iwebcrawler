using System;
using System.Collections.Generic;
using System.Text;

namespace CrawlerNameSpace.Utilities.Tests
{
    public class ConstraintsTest
    {
        /**
         * This is a private Constraints variable that holds an instance of the
         * class Constraints,and it will be the one that this class will use for
         * testing
         */
        private Constraints testConstraints = new Constraints(4, true, "technion ynet nana", " co.il org gov");

        /**
         * This method tests the getAllowedDepth() method.
         */
        public void test1()
        {
            if (testConstraints.getAllowedDepth() == 4)
            {
                Console.WriteLine("getAllowedDepth() PASSED");
            }
            else
            {
                Console.WriteLine("getAllowedDepth() FAILED");
            }
        }

        /**
         * This method tests the getRestrictionList() method.
         */
        public void test2()
        {
            List<String> restrictionList = new List<string>();
            restrictionList = testConstraints.getRestrictionList();
            if ((restrictionList.Contains("technion")) && (restrictionList.Contains("ynet")) && (restrictionList.Contains("nana")))
            {
                Console.WriteLine("getRestrictionList() PASSED");
            }
            else
            {
                Console.WriteLine("getRestrictionList() FAILED");
                Console.WriteLine("Returned List should have contained technion,ynet,nana");
            }
        }

        /**
         * This method tests the getCrawlList() method.
         */
        public void test3()
        {
            List<String> crawlList = new List<string>();
            crawlList = testConstraints.getCrawlList();
            if ((crawlList.Contains("co.il")) && (crawlList.Contains("org")) && (crawlList.Contains("gov")))
            {
                Console.WriteLine("getCrawlList() PASSED");
            }
            else
            {
                Console.WriteLine("getCrawlList() FAILED");
                Console.WriteLine("Returned List should have contained co.il,org,gov");
            }
        }

        /**
         * This method tests the isParametrizationAllowed() method.
         */
        public void test4()
        {
            if (testConstraints.isParametrizationAllowed())
            {
                Console.WriteLine("isParametrizationAllowed() PASSED");
            }
            else
            {
                Console.WriteLine("isParametrizationAllowed() FAILED");
                Console.WriteLine("Reason: the method should have returned true");
            }
        }

        /**
         * This method tests the isUrlValid(string url) method.
         */
        public void test5()
        {
            bool passed = true;
            String error = "";

            //testing url when its depth is bigger depth than allowed(should return false) 
            if ((testConstraints.isUrlValid("www://technion.co.il/ee/math/something/again")))
            {
                passed = false;
                error = "Testing url when its depth is bigger failed";
            }
            
            //testing url when its depth is smaller depth than allowed(should return true) 
            if (!(testConstraints.isUrlValid("www://technion.co.il/again")))
            {
                passed = false;
                error = "Testing url when its depth is smaller failed";
            }

            //testing url containing ?(it should be allowed,should return true)
            if (!(testConstraints.isUrlValid("www://technion.co.il/again?")))
            {
                passed = false;
                error = "Testing url containing ?,when it should be allowed failed";
            }

            //testing url that does not contain "?"(it should be allowed,should return true)
            if (!(testConstraints.isUrlValid("www://technion.co.il/again")))
            {
                passed = false;
                error = "Testing url that does not contain ? failed";
            }

            //testing url that contains "?",when its not allowed(should return false)
            testConstraints = new Constraints(4, false, "technion ynet nana", " co.il org gov");
            
            //testing url containing ?(it should not be allowed,should return false)
            if ((testConstraints.isUrlValid("www://technion.co.il/again?")))
            {
                passed = false;
                error = "Testing url containing ? when its not allowed failed";
            }

            //testing url that does not contain "?"(it should  be allowed,should return true)
            if (!(testConstraints.isUrlValid("www://technionagain.co.il")))
            {
                passed = false;
                error="Testing url that does not contain ?,when paramater is not allowed failed";
            }

            //testing url that does not matches one of the crawl networks(should return false)
            if ((testConstraints.isUrlValid("www://technion.uk/again.ac.il")))
            {
                passed = false;
                error = "Testing url that does not matches one of the crawl networks failed";
            }

            //testing url that matches one of the restricted networks(should return false)
            if ((testConstraints.isUrlValid("www://technion/again.ynet")))
            {
                passed = false;
                error = "testing url that matches one of the restricted networks failed";
            }

            //chek if all tests passed
            if (passed)
            {
                Console.WriteLine("isUrlValid(string url) PASSED");
            }
            else
            {
                Console.WriteLine("isUrlValid(string url) FAILED");
                Console.WriteLine(error);
            }

        }

        /**
          * This method is the main test that gathers all the tests.
          */
        public static void MainTest()
        {
            ConstraintsTest testingConstraints = new ConstraintsTest();
            testingConstraints.test1();
            testingConstraints.test2();
            testingConstraints.test3();
            testingConstraints.test4();
            testingConstraints.test5();
        }
    }
}

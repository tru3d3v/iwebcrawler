using System;
using System.Collections.Generic;
using System.Text;
using CrawlerNameSpace.Utilities;

namespace CrawlerNameSpace.Tests
{
    public class FilterTest
    {
        public void Test1()
        {
            Constraints constraintObj = new Constraints(4, true,"co.il gov uk", "com ");
            Filter testFilter = new Filter("http",constraintObj);
            char[] delimiters={' '};

            List<String> links = new List<string>("http://www.facebook.com http://www.sun.com http://www.ynet.co.il ".Split(delimiters));
            links.Add("http://www.street.uk/something.asp");
            links.Add("http://morfix.mako.com/default.aspx?q=check");
            links.Add("http://www.walla.co.il");
            links.Add("http://www.google.com/addon/mov/appear");
            links.Add("www.adam.com");
            links.Add("ftp://www.google.com/addon");
            links.Add("http://www.google.com/addon/shape/mov/appear");
            links.Add("http://www.yalla.gov");

            List<String> filteredLinks = testFilter.filterLinks(links);
            List<String> checkLinks = new List<string>("http://www.facebook.com http://www.sun.com http://morfix.mako.com/default.aspx?q=check ".Split(delimiters, StringSplitOptions.RemoveEmptyEntries));
            checkLinks.Add("http://www.google.com/addon/mov/appear");
            checkLinks.Add("http://www.adam.com");
            bool passed= true;
            foreach(String link in checkLinks)
            {
                if (!(filteredLinks.Contains(link)))
                {
                    passed = false;
                }
            }

            if (passed)
            {
                Console.WriteLine("Filter.filterLinks(String links) PASSED");
            }
            else 
            {
                Console.WriteLine("Filter.filterLinks(String links) FAILED");
            }
        }

        /**
         * This method is the main test that gathers all the tests.
         */
        public static void MainTest()
        {
            FilterTest testingFilter = new FilterTest();
            testingFilter.Test1();
        }


    }
}

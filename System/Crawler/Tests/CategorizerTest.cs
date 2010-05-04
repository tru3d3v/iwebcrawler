using System;
using System.Collections.Generic;
using System.Text;
using CrawlerNameSpace.Utilities;

namespace CrawlerNameSpace.Tests
{
    public class CategorizerTest
    {
        private String resource = "This is an example test content of this resource";

        private Categorizer testCategorizer= new Categorizer(new List<Category>());

        /**
         * This method tests the getSuitableCategoryName(resource) method.
         */
        public void Test1()
        {
            testCategorizer.classifyContent(resource,"www.test.com");
            if (testCategorizer.getSuitableCategoryName(resource).Contains( "webpage"))
            {
                Console.WriteLine("getSuitableCategoryName(String resource) PASSED");
            }
            else
            {
                Console.WriteLine("getSuitableCategoryName(String resource) FAILED");
            }
        }

        /**
         * This method tests getMatchLevel(ResourceContent resource,String categoryName) method.
         */
        public void Test2()
        {
            List<String> categoryName = testCategorizer.getSuitableCategoryName(resource);
            if (testCategorizer.getMatchLevel(resource, categoryName[0]) == 100)
            {
                Console.WriteLine("getMatchLevel(String resource, String categoryName) PASSED");
            }
            else
            {
                Console.WriteLine("getMatchLevel(String resource, String categoryName) FAILED");
            }
        }

        /**
         * This method is the main test that gathers all the tests.
         */
        public static void MainTest()
        {
            CategorizerTest categorizerTest = new CategorizerTest();
            categorizerTest.Test1();
            categorizerTest.Test2();
        }


    }
}

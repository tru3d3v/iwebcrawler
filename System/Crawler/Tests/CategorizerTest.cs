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
            List<Result> results = testCategorizer.classifyContent(resource,"www.test.com");
            Boolean passed = false;

            foreach (Result result in results)
            {
                if (result.getCategoryID() == "0")
                {
                    passed = true;
                }
            }
            
            if (passed)
            {
                Console.WriteLine("classifyContent(String resource,String url) PASSED");
            }
            else
            {
                Console.WriteLine("classifyContent(String resource,String url) FAILED");
            }
        }
        /**
         * This method is the main test that gathers all the tests.
         */
        public static void MainTest()
        {
            CategorizerTest categorizerTest = new CategorizerTest();
            categorizerTest.Test1();
        }


    }
}

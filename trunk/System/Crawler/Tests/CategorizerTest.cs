using System;
using System.Collections.Generic;
using System.Text;
using CrawlerNameSpace.Utilities;

namespace CrawlerNameSpace.Tests
{
    public class CategorizerTest
    {
        private ResourceContent resource = new ResourceContent("www.ynet." +
            "co.il", ResourceType.HtmlResource, "This is an example test content of this resource", 200);

        private Categorizer testCategorizer= new Categorizer(new List<Category>());

        /**
         * This method tests the getSuitableCategoryName(resource) method.
         */
        public void Test1()
        {
            testCategorizer.classifyContent(resource);
            if (testCategorizer.getSuitableCategoryName(resource).Contains( "webpage"))
            {
                Console.WriteLine("getSuitableCategoryName(resource) PASSED");
            }
            else
            {
                Console.WriteLine("getSuitableCategoryName(resource) FAILED");
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
                Console.WriteLine("getMatchLevel(ResourceContent resource,String categoryName) PASSED");
            }
            else
            {
                Console.WriteLine("getMatchLevel(ResourceContent resource,String categoryName) FAILED");
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

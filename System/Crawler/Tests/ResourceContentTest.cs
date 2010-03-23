using System;
using System.Collections.Generic;
using System.Text;

namespace CrawlerNameSpace.Tests
{
    public class ResourceContentTest
    {
        ResourceContent resource = new ResourceContent("www.ynet.co.il", ResourceType.HtmlResource, "Please click the next"
            + " buttom to start crawling !!", 200);

        /**
         * This method tests the getResourceUrl() method.
         */
        public void test1()
        {
            Console.WriteLine("The Class ResourceContent is tested on: ");
            Console.WriteLine(resource.ToString());
            if (resource.getResourceUrl().Equals("www.ynet.co.il"))
            {
                Console.WriteLine("getResourceUrl() PASSED");
            }
            else
            {
                Console.WriteLine("getResourceUrl() FAILED");
            }
        }

        /**
         * This method tests the getResourceType() method.
         */
        public void test2()
        {
            if (resource.getResourceType() == ResourceType.HtmlResource)
            {
                Console.WriteLine("getResourceType() PASSED");
            }
            else
            {
                Console.WriteLine("getResourceType() FAILED");
            }
        }

        /**
         * This method tests the getResourceContent() method.
         */
        public void test3()
        {
            if (resource.getResourceContent().Equals("Please click the next buttom to start crawling !!"))
            {
                Console.WriteLine("getResourceContent() PASSED");
            }
            else
            {
                Console.WriteLine("getResourceContent() FAILED");
            }
        }

        /**
         * This method tests the getReturnCode() method.
         */
        public void test4()
        {
            if (resource.getReturnCode()==200)
            {
                Console.WriteLine("getReturnCode() PASSED");
            }
            else
            {
                Console.WriteLine("getReturnCode() FAILED");
            }
        }

        /**
         * This method tests the isValid() method.
         */
        public void test5()
        {
            if (resource.isValid())
            {
                Console.WriteLine("isValid() PASSED");
            }
            else
            {
                Console.WriteLine("isValid() FAILED");
            }
        }
        
        /**
          * This method is the main test that gathers all the tests.
          */
        public static void MainTest()
        {
            ResourceContentTest resourceTest = new ResourceContentTest();

            resourceTest.test1();
            resourceTest.test2();
            resourceTest.test3();
            resourceTest.test4();
            resourceTest.test5();
        }
    }
}

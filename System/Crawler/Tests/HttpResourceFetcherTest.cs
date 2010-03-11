using System;
using System.Collections.Generic;
using System.Text;

namespace CrawlerNameSpace.Tests
{
    class HttpResourceFetcherTest
    {
        private const int VALID_CODE = 200;
        private const int IN_VALID_CODE = 400;
        /**
         * Testing fetching correct page,the method will fetch a page and print it's content. 
         * 
         */
        public void test1()
        {
            Console.WriteLine("Testing correct url:");
            Console.WriteLine("Fetching the url: http://www.example-code.com/csharp/spider.asp");
            ResourceContent resource = new HttpResourceFetcher().fetch("http://www.example-code.com/csharp/spider.asp", 10000);
            System.Console.WriteLine("The returnCode of the request is:  " +  resource.getReturnCode());
            Console.WriteLine("Content of the fetched page:");
            Console.WriteLine(resource.getResourceContent());
            if (resource.getReturnCode()==VALID_CODE)
            {
                Console.WriteLine("Test1 has passed");
            }
            else
            {
                 Console.WriteLine("Test1 has failed");
            }
        }

        /**
         * This method tests the fetch method using ftp,the return code
         * should be 400 and the content of the resource should be null.
         */ 
        public void test2()
        {
            Console.WriteLine("Trying to fetch using ftp:");
            Console.WriteLine("Fetching the url:ftp://ftp.site.com/dir1/dir2/file.ext.");
            ResourceContent resource1 = new HttpResourceFetcher().fetch("ftp://ftp.site.com/dir1/dir2/file.ext.",10000);
            Console.WriteLine("The returnCode of the request is: {0} ", resource1.getReturnCode());
            Console.WriteLine("The content of the returned resource is:" + resource1.getResourceContent());
            if ((resource1.getReturnCode()==IN_VALID_CODE)&&(resource1.getResourceContent()==null))
            {
                Console.WriteLine("Test1 has passed");
            }
            else
            {
                Console.WriteLine("Test1 has failed");
            }
        }

        /**
         * This method tests the fetch method when it trys to fetch a url that does not exist
         */
        public void test3()
        {
            Console.WriteLine("Trying to fetch corrupted url");
            Console.WriteLine("Fetching the url :http://www.adamshobash.com");
            ResourceContent resource2 = new HttpResourceFetcher().fetch("http://www.adamshobash.com",10000);
            Console.WriteLine("The returnCode of the request is: " + resource2.getReturnCode());
            Console.WriteLine("The content of the returned resource is:", resource2.getResourceContent());
            if ((resource2.getReturnCode()==IN_VALID_CODE)&&(resource2.getResourceContent()==null))
            {
                Console.WriteLine("Test3 has passed");
            }
            else
            {
                Console.WriteLine("Test3 has failed");
            }
        }

        /**
         * This method tests the fetch method when it trys to fetch with a small timeout
         */
        public void test4()
        {
            Console.WriteLine("Trying to fetch using small timeout: ");
            Console.WriteLine("Fetching the url: http://www.example-code.com/csharp/spider.asp with timeout =1ms");
            ResourceContent resource3 = new HttpResourceFetcher().fetch("http://www.example-code.com/csharp/spider.asp", 1);
            Console.WriteLine("The returnCode of the request is: " + resource3.getReturnCode());
            Console.WriteLine("The content of the returned resource is:", resource3.getResourceContent());
            if ((resource3.getReturnCode() == IN_VALID_CODE) && (resource3.getResourceContent() == null))
            {
                Console.WriteLine("Test4 has passed");
            }
            else
            {
                Console.WriteLine("Test4 has failed");
            }
        }
        
        public static void MainTest()
        {
            HttpResourceFetcherTest httpFetcher = new HttpResourceFetcherTest();
            httpFetcher.test1();
            httpFetcher.test2();
            httpFetcher.test3();
            httpFetcher.test4();
        }
    }
}

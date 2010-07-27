using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using CrawlerNameSpace.Utilities;

namespace CrawlerNameSpace.StorageSystem
{
    public class ResultStorageTest
    {
        //This method tests the getURLResults method
        public void Test1()
        {
            ResultsStorageImp st = new ResultsStorageImp();
            Console.WriteLine("[] Trying to get the results that have been crawled from task :");
            Console.WriteLine("[]\"3012e088-1519-4a78-9986-89683a7901a3\"");
            Console.WriteLine("[] For the Url : http://www.sex.com");

            List<Result> resultedUrls = st.getURLResults("3012e088-1519-4a78-9986-89683a7901a3", "http://www.sex.com");
     
            Console.WriteLine("The results of the url are :");
            foreach (Result resultUrl in resultedUrls)
            {
                Console.WriteLine("The resultID is : " + resultUrl.getResultID());
                Console.WriteLine("The categoryID is : " + resultUrl.getCategoryID());
                Console.WriteLine("The rank of the url is :" + resultUrl.getRank());
                Console.WriteLine("The trustmeter of the url is :" + resultUrl.getTrustMeter());
                Console.WriteLine("============================================================");
            }
            Console.WriteLine("- PASSED -");
        }

        //This method tests the getURLsFromCategory method
        public void Test2()
        {
            ResultsStorageImp st = new ResultsStorageImp();
            Console.WriteLine("[] Trying to get all the urls that belong to the category with id :");
            Console.WriteLine("75d7e9f8-a1aa-49eb-8ec6-274e8036f3f5");

            List<Result> urls = st.getURLsFromCategory("7ec2bfb1-0a0d-406b-9132-2d65abb811f0",
                                "75d7e9f8-a1aa-49eb-8ec6-274e8036f3f5", Order.NormalOrder, 1, 4);
            Console.WriteLine("The urls in a normal order: ");
            foreach (Result resultUrl in urls)
            {
                Console.WriteLine("The url is : " + resultUrl.getUrl());
                Console.WriteLine("The resultID is : " + resultUrl.getResultID());
                Console.WriteLine("The categoryID is : " + resultUrl.getCategoryID());
                Console.WriteLine("The rank of the url is :" + resultUrl.getRank());
                Console.WriteLine("The trustmeter of the url is :" + resultUrl.getTrustMeter());
                Console.WriteLine("============================================================");
            }

            urls = st.getURLsFromCategory("7ec2bfb1-0a0d-406b-9132-2d65abb811f0",
                                "75d7e9f8-a1aa-49eb-8ec6-274e8036f3f5", Order.AscendingRank, 1, 2);
            Console.WriteLine("The urls in a ascending order of the rank: ");
            foreach (Result resultUrl in urls)
            {
                Console.WriteLine("The url is : " + resultUrl.getUrl());
                Console.WriteLine("The resultID is : " + resultUrl.getResultID());
                Console.WriteLine("The categoryID is : " + resultUrl.getCategoryID());
                Console.WriteLine("The rank of the url is :" + resultUrl.getRank());
                Console.WriteLine("The trustmeter of the url is :" + resultUrl.getTrustMeter());
                Console.WriteLine("============================================================");
            }

            urls = st.getURLsFromCategory("7ec2bfb1-0a0d-406b-9132-2d65abb811f0",
                                "75d7e9f8-a1aa-49eb-8ec6-274e8036f3f5", Order.DescendingRank, 0, 3);
            Console.WriteLine("The urls in a descending order of the rank: ");
            foreach (Result resultUrl in urls)
            {
                Console.WriteLine("The url is : " + resultUrl.getUrl());
                Console.WriteLine("The resultID is : " + resultUrl.getResultID());
                Console.WriteLine("The categoryID is : " + resultUrl.getCategoryID());
                Console.WriteLine("The rank of the url is :" + resultUrl.getRank());
                Console.WriteLine("The trustmeter of the url is :" + resultUrl.getTrustMeter());
                Console.WriteLine("============================================================");
            }

            urls = st.getURLsFromCategory("7ec2bfb1-0a0d-406b-9132-2d65abb811f0",
                                "75d7e9f8-a1aa-49eb-8ec6-274e8036f3f5", Order.AscendingTrust, 1, 3);
            Console.WriteLine("The urls in a ascending order of the trustmeter: ");
            foreach (Result resultUrl in urls)
            {
                Console.WriteLine("The url is : " + resultUrl.getUrl());
                Console.WriteLine("The resultID is : " + resultUrl.getResultID());
                Console.WriteLine("The categoryID is : " + resultUrl.getCategoryID());
                Console.WriteLine("The rank of the url is :" + resultUrl.getRank());
                Console.WriteLine("The trustmeter of the url is :" + resultUrl.getTrustMeter());
                Console.WriteLine("============================================================");
            }

            urls = st.getURLsFromCategory("7ec2bfb1-0a0d-406b-9132-2d65abb811f0",
                                "75d7e9f8-a1aa-49eb-8ec6-274e8036f3f5", Order.DescendingTrust, 1, 4);
            Console.WriteLine("The urls in a descending order of the trustmeter: ");
            foreach (Result resultUrl in urls)
            {
                Console.WriteLine("The url is : " + resultUrl.getUrl());
                Console.WriteLine("The resultID is : " + resultUrl.getResultID());
                Console.WriteLine("The categoryID is : " + resultUrl.getCategoryID());
                Console.WriteLine("The rank of the url is :" + resultUrl.getRank());
                Console.WriteLine("The trustmeter of the url is :" + resultUrl.getTrustMeter());
                Console.WriteLine("============================================================");
            }

            Console.WriteLine("-PASSED-");
        }
        //////////////////FINISH THE SECOND TEST(Test2) FIRST AND DELETE THIS MESSAGE///////////////////
    }
}

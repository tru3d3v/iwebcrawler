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
            Console.WriteLine("[]\"e4a55914-0847-4d70-be22-d2ecac77cdfa\"");
            Console.WriteLine("[] For the Url : facebook.com");

            List<Result> resultedUrls = st.getURLResults("e4a55914-0847-4d70-be22-d2ecac77cdfa", "facebook.com");
     
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

        //This method tests the getTotalURLs method
        public void Test3()
        {
            ResultsStorageImp st = new ResultsStorageImp();
            Console.WriteLine("[] Trying to get the Total number of crawled urls from task with ID :");
            Console.WriteLine("\"7ec2bfb1-0a0d-406b-9132-2d65abb811f0\"");

            ulong numUrls = st.getTotalURLs("7ec2bfb1-0a0d-406b-9132-2d65abb811f0");

            Console.WriteLine("Total number of Urls of the Task is : ");
            Console.WriteLine(numUrls);
            Console.WriteLine("=====================================================================");
            
            Console.WriteLine("[] Trying to get the Total number of crawled urls from task with ID :");
            Console.WriteLine("\"b80dcfdd-d8c4-4176-aef1-6b76add16fb3\"");

            numUrls = st.getTotalURLs("b80dcfdd-d8c4-4176-aef1-6b76add16fb3");

            Console.WriteLine("Total number of Urls of the Task is : ");
            Console.WriteLine(numUrls);
            Console.WriteLine("=====================================================================");

            Console.WriteLine("[] Trying to get the Total number of crawled urls from task with ID :");
            Console.WriteLine("\"e4a55914-0847-4d70-be22-d2ecac77cdfa\"");

            numUrls = st.getTotalURLs("e4a55914-0847-4d70-be22-d2ecac77cdfa");

            Console.WriteLine("Total number of Urls of the Task is : ");
            Console.WriteLine(numUrls);
            Console.WriteLine("=====================================================================");

        }

        //This method tests the removeURLResult method
        public void Test4()
        {
            ResultsStorageImp st = new ResultsStorageImp();

            Console.WriteLine("[] Trying to delete the result row with the resultID:");
            Console.WriteLine("\"dad56e38-d678-418d-bc8e-0b9e50843092\"");

            st.removeURLResult(new Result("dad56e38-d678-418d-bc8e-0b9e50843092","www.facebook.com",
                                    "f9e26230-6da1-4e60-9c88-dc4eaa87f9da",30,79));

            Console.WriteLine("-PASSED-");
        }

        //This method tests the addURLResult method
        public void Test5()
        {
            ResultsStorageImp st = new ResultsStorageImp();

            Console.WriteLine("[] Trying to add the url : http://www.one.co.il to the category boy_soccer.");
            Console.WriteLine("[] The Id of this category is : 8e0d87ef-b04e-47ab-be25-56af4af3e013");
            Console.WriteLine("[] The Id of the task to which it is added is : 7ec2bfb1-0a0d-406b-9132-2d65abb811f0");

           // st.addURLResult("7ec2bfb1-0a0d-406b-9132-2d65abb811f0", new Result("8e0d87ef-b04e-47ab-be25-56af4af3e013",
           //                   "http://www.one.co.il", "8e0d87ef-b04e-47ab-be25-56af4af3e013",56,70));

            st.addURLResult("7ec2bfb1-0a0d-406b-9132-2d65abb811f0", new Result("0",
                               "http://www.one.co.il", "0", 0, 100));
            Console.WriteLine("-PASSED-");
        }

        //This method tests the replaceURLResult method
        public void Test6()
        {
            ResultsStorageImp st = new ResultsStorageImp();

            Console.WriteLine("[] Trying to replace the url:yahoo.com from the news category" + 
                                    " to the world_basketball category");


            st.replaceURLResult("7ec2bfb1-0a0d-406b-9132-2d65abb811f0",new Result("","yahoo.com","",83,91),
                                    new Result("","yahoo.com","3575315a-5db6-4699-bd49-ce36f67b91b9",83,91));

            Console.WriteLine("-PASSED-");
        }

        //This method tests all the methods when the data searched does not exist in the data base
        public void Test7()
        {
            ResultsStorageImp st = new ResultsStorageImp();
            //testing the getURLResults method with a task that does not exist.
            Console.WriteLine("[] Trying to get the results that have been crawled from task that does not exist:");
            Console.WriteLine("[] For the Url : http://www.sex.com");
            List<Result> resultedUrls = st.getURLResults("3012e088-2319-4a78-9986-89683a7901a3", "http://www.sex.com");

            if (resultedUrls==null)
                Console.WriteLine("-PASSED-");

            //testing the getTotalURLs method with task that does not exist
            Console.WriteLine("[] Trying to get the Total number of crawled urls from task that does not exist :");

            ulong numUrls = st.getTotalURLs("7ec2bfb8-0a0d-406b-9132-2d65abb811f0");

            if (numUrls == 0) Console.WriteLine("-PASSED-");

            //testing the getURlsFromCategory method with category that does not exist
            Console.WriteLine("[] Trying to get all the urls that belong to the a category that does not exist :");
           
            List<Result> urls = st.getURLsFromCategory("7e32bfb1-0a0d-406b-9132-2d65abb811f0",
                                "75d7e9f8-a1aa-49eb-8ec6-274la036f3f5", Order.NormalOrder, 1, 4);

            if (urls.Count == 0) Console.WriteLine("-PASSED-");

        }
    }
}

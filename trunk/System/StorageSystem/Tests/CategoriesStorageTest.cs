using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using CrawlerNameSpace.Utilities;

namespace CrawlerNameSpace.StorageSystem
{
    public class CategoriesStorageTest
    {
        //Tests the getCategories method
        public void Test1()
        {
            CategoriesStorageImp ss = new CategoriesStorageImp();
            //trying to get the categories of the task which his task id is :
            //"7ec2bfb1-0a0d-406b-9132-2d65abb811f0"
            List<Category> categories = ss.getCategories("7ec2bfb1-0a0d-406b-9132-2d65abb811f0");

            Console.WriteLine("[] Categories saved for the taskID : \"7ec2bfb1-0a0d-406b-9132-2d65abb811f0\" ");
            foreach (Category category in categories)
            {
                Console.WriteLine("CategoryID         : " + category.getCategoryID());
                Console.WriteLine("CategoryName       : " + category.getCatrgoryName());
                Console.Write("Keywords           : " );
                if (category.getKeywordList() != null)
                {
                    foreach (String keyword in category.getKeywordList())
                    {
                        Console.Write(keyword + " ");
                    }
                }
                Console.WriteLine("");
                Console.WriteLine("ParentCategory     : " + category.getParentName());
                Console.WriteLine("ConfidenceLevel    : " + category.getConfidenceLevel());
                Console.WriteLine("=============================================================");
            }
            if (categories.Count == 0) Console.WriteLine("Test Not Found !!");
            Console.WriteLine(" - PASSED -");
            Console.WriteLine("=============================================================");
        }

        //Test the setCategories method
        public void Test2()
        {
            CategoriesStorageImp ss = new CategoriesStorageImp();

            List<Category> categories = new List<Category>();

            List<String> keywords1 = new List<string>();
            keywords1.Add("game");
            keywords1.Add("screen");
            keywords1.Add("barbi");
            categories.Add(new Category("","","games",keywords1,78));
            List<String> keywords2 = new List<string>();
            keywords2.Add("computers");
            keywords2.Add("disc");
            keywords2.Add("memory");
            keywords2.Add("motherboard");
            categories.Add(new Category("", "", "computers", keywords2, 84));
            List<String> keywords3 = new List<string>();
            keywords3.Add("studies");
            keywords3.Add("research");
            keywords3.Add("student");
            keywords3.Add("lecture");
            categories.Add(new Category("","","studies",keywords3,60));

            Console.WriteLine("[] Trying to set categories ...");
            ss.setCategories("3012e088-1519-4a78-9986-89683a7901a3", categories);

            Console.WriteLine(" - PASSED -");
        }

        //Tests the setParentTOSon method
        public void Test3()
        {
            CategoriesStorageImp ss = new CategoriesStorageImp();

            Console.WriteLine("[] Trying to set the category : \"f9e26230-6da1-4e60-9c88-dc4eaa87f9da\" ...");
            Console.WriteLine("   As a parent to the category: \"75d7e9f8-a1aa-49eb-8ec6-274e8036f3f5\"");

            ss.setParentToSon("f9e26230-6da1-4e60-9c88-dc4eaa87f9da", "75d7e9f8-a1aa-49eb-8ec6-274e8036f3f5");
            Console.WriteLine(" - PASSED -");
        }
    }
}

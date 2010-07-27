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
                Console.WriteLine("Keywords           : " + category.getKeywordList());
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

            List<String> keywords = new List<string>();
            keywords.Add("game");
            keywords.Add("screen");
            keywords.Add("barbi");
            categories.Add(new Category("","","games",keywords,78));
            keywords.Clear();
            keywords.Add("computers");
            keywords.Add("disc");
            keywords.Add("memory");
            keywords.Add("motherboard");
            categories.Add(new Category("", "", "computers", keywords, 84));
            keywords.Clear();
            keywords.Add("studies");
            keywords.Add("research");
            keywords.Add("student");
            keywords.Add("lecture");
            categories.Add(new Category("","","studies",keywords,60));

            Console.WriteLine("[] Trying to set categories ...");
            ss.setCategories("3012e088-1519-4a78-9986-89683a7901a3", categories);

            Console.WriteLine(" - PASSED -");
        }

        //Tests the setParentTOSon method
        public void Test3()
        {
            CategoriesStorageImp ss = new CategoriesStorageImp();

            Console.WriteLine("[] Trying to set the category : \"544a4fce-eb54-4756-8830-70b0599a0547\" ...");
            Console.WriteLine("   As a parent to the category: \"0fb02dca-a22d-4f61-925e-1d0ae656ce95\"");

            ss.setParentToSon("544a4fce-eb54-4756-8830-70b0599a0547", "0fb02dca-a22d-4f61-925e-1d0ae656ce95");
            Console.WriteLine(" - PASSED -");
        }
    }
}

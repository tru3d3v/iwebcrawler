using System;
using System.Collections.Generic;
using System.Text;
using CrawlerNameSpace.Utilities;

namespace CrawlerNameSpace
{
    /**
     * This Class is responsible for matching a category for
     * a given html page represented in string object.
     */
    class Categorizer
    {
        //This is a list containing all the categories that were created
        private List<Category> categoryList;


        public Categorizer(List<Category> list)
        {
            categoryList = list;
        }

        /**
         * This method recieves a word list which contains the content of the 
         * resource(html page) and then classifies it to the suitable category.
         */
        public List<Result> classifyContent(String resource,String url)
        {
            List<Result> results = new List<Result>();
            results.Add(new Result("0", url, "0", 0, 100));
            return results;
        }

        /**
         * This method finds and returns the Id of the categories to whom
         * the given resource is belonged.
         */
        /*
        public List<String> getSuitableCategories(String resource, String url)
        {
            List<String> suitableCategories = new List<string>();
            if (currentUrl == url)
            {
                foreach (Category cat in currentCategoryList)
                {
                    suitableCategories.Add(cat.getCategoryID());
                }
            }
            return suitableCategories;
        }
        */

        /**
         * This method returns the match level of the given resource to the given category ID.
         */
        /*
        public int getMatchLevel(String resource, String categoryId)
        {
            return 100;
        }
        */
    }
}

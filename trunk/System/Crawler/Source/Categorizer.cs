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
        
        //This is a dictionary map that contains the categoryName, as a key, and
        //its list of urls that belong to that category.
        private Dictionary<String,List<ResourceContent>> urlsMap;

        public Categorizer(List<Category> list)
        {
            categoryList = list;
            urlsMap = new Dictionary<string,List<ResourceContent>>();
            urlsMap.Add("webpage",new List<ResourceContent>());
        }

        /**
         * This method recieves a word list which contains the content of the 
         * resource(html page) and then classifies it to the suitable category.
         */
        public void classifyContent(ResourceContent resource)
        {
            urlsMap["webpage"].Add(resource);
        }

        /**
         * This method finds and returns the names of the categories to whom
         * the given resource is belonged.
         */
        public List<String> getSuitableCategoryName(ResourceContent resource)
        {
            List<String> suitableCategories = new List<string>();
            suitableCategories.Add("webpage");
            return suitableCategories;
        }

        /**
         * This method returns the match level of the given resource to the given category.
         */
        public int getMatchLevel(ResourceContent resource, String categoryName)
        {
            if (urlsMap.ContainsKey(categoryName.ToLower()))
            {
                return 100;
            }
            else
            {
                return 0;
            }
           
        }
    }
}

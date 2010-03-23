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
        public void classifyContent(List<String> wordList)
        {
            //To DO
        }

        /**
         * This method finds and returns the CategoryName of the given resource.
         */
        public String getSuitableCategoryName(String resource)
        {
            return null;
        }

        /**
         * This method returns the match level of the given resource to its category.
         */
        public int getMatchLevel(String resource)
        {
            return 0;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using CrawlerNameSpace.Utilities;
using System.IO;

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
            foreach (Category category in categoryList)
            {
                if (LogDebuggerControl.getInstance().debugCategorization)
                {
                    StreamWriter sw = new
                        StreamWriter("_DEBUG_INFO_CATEGORIZER@" + System.Threading.Thread.CurrentThread.ManagedThreadId + ".txt", true);
                    sw.WriteLine(" ***** HEAD REQUEST ************************************************* ");
                    sw.WriteLine(" URL : " + url);
                    sw.Close();
                }

                CategorizerOptions options = new CategorizerOptions();
                int matchLevel = category.getMatchLevel(resource,options);
                
                
                if (matchLevel > category.getConfidenceLevel())
                {
                    results.Add(new Result("0", url, category.getCategoryID(), 0, matchLevel));
                }
                
            }
            //results.Add(new Result("0", url, "0", 0, 100));
            return results;
        }

        /**
         * This method gets a HTML page, and returns a list of all the 
         * match levels of the resource to all the categories.
         */
        public List<int> classifyContentToAllCategories(String resource, CategorizerOptions options)
        {
            List<int> matchLevelResults = new List<int>();
            foreach (Category category in categoryList)
            {
                int matchLevel = category.getMatchLevel(resource,options);

                matchLevelResults.Add(matchLevel);
            }
            //results.Add(new Result("0", url, "0", 0, 100));
            return matchLevelResults;
        }
    }
}

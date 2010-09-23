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
                
                CategorizerOptions options = getCategorizerOptions();
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
         * This method creates Categorizer Options and returns it.
         * The values of the variables of the new object are brought from the data base.
         * Note : In case the operation mode is manual the variables will be default ones,
         *        such is the case when the returns values from the data base are nulls.
         */
        private CategorizerOptions getCategorizerOptions()
        {
            CategorizerOptions options = new CategorizerOptions();

            if (WorkDetails.getOperationMode() == operationMode_t.Auto)
            {
                String alpha = StorageSystem.StorageSystem.getInstance().getProperty(WorkDetails.getTaskId(),
                TaskProperty.CAT_ALPHA.ToString());
                String betta = StorageSystem.StorageSystem.getInstance().getProperty(WorkDetails.getTaskId(),
                                TaskProperty.CAT_BETA.ToString());
                String gamma = StorageSystem.StorageSystem.getInstance().getProperty(WorkDetails.getTaskId(),
                                TaskProperty.CAT_GAMMA.ToString());
                String min = StorageSystem.StorageSystem.getInstance().getProperty(WorkDetails.getTaskId(),
                                TaskProperty.CAT_MIN.ToString());
                String penalty = StorageSystem.StorageSystem.getInstance().getProperty(WorkDetails.getTaskId(),
                                TaskProperty.CAT_PENLTY.ToString());

                if (isRealNum(alpha))
                    options.ALPHA = Convert.ToDouble(alpha);
                if (isRealNum(betta))
                    options.BETA = Convert.ToDouble(betta);
                if (isRealNum(gamma))
                    options.GAMMA = Convert.ToDouble(gamma);
                if (isRealNum(min))
                    options.MIN_WORDS_LIMIT = Convert.ToDouble(min);
                if (isRealNum(penalty))
                    options.MIN_WORDS_PENLTY = Convert.ToDouble(penalty);
            }

            return options;
        }

        /**
         * This method check wether the given string is  a real number bigger than 0 
         * Or not.
         */
        private bool isRealNum(String num)
        {
            double realNum = Convert.ToDouble(num);
            if ((num != null) && (num != "") && (!(Convert.IsDBNull(num))) && (realNum >= 0))
                return true;
            else
                return false;
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

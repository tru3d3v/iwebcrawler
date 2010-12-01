using System;
using System.Collections.Generic;
using System.Text;
using CrawlerNameSpace.Utilities;

namespace CrawlerNameSpace.StorageSystem
{
    /**
     * This interface defines the functionality used in order to save or to get the 
     * results of the crawling operation.
     */ 
    interface ResultsStorage
    {
        /**
         * This function gets results of a task,  the results will be represented as list of Result object:
         * categoryId, rank, trustMeter.
         */
        List<Result> getURLResults(String taskId, String url);

        /**
         * This function returns a list of urls which suits a specific category, every url will be described 
         * as object which contains data about: url, categoryId, rank, trustMeter.
         */
        List<Result> getURLsFromCategory(String taskId, String categoryId, Order order, int from, int to);

        /**
         * This function returns the number of the urls which already has been crawled.
         */
        ulong getTotalURLs(String taskId, String categoryId);

        /**
         * This function replaces the URL result to the new one (and it's fathers).
         */
        void replaceURLResult(String taskId,Result oldResult, Result newResult);

        /**
         * This function removes one entry of the URL result, which specified in the given argument.
         */
        void removeURLResult(Result result);

        /**
         * This function removes all entries for specific task, which specified in the given argument.
         */
        void removeAllResults(String taskID);

        /**
         * This function adds the URL result to the given categories (and it's fathers).
         */
        void addURLResult(String taskId, Result result);
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using CrawlerNameSpace.Utilities;
using System.IO;
using System.Threading;
using CrawlerNameSpace.StorageSystem;

namespace CrawlerNameSpace
{
    /*
     * This Class is responsible of processing the html page content 
     * which consists from Extracting it's links, giving them a rank, and
     * match the given page to suitable category.
     */
    class HtmlPageCategorizationProcessor : ResourceProcessor
    {
        /*
         * This variable is used to check if the given resource content 
         * is valid or not.
         */
        private const int VALID_CODE = 200;

        private Extractor extractor;
        private Categorizer categorizer;
        private Ranker ranker;
        private Filter filter;
        private Queue<Url> queueFronier;
        private string taskId;

        public HtmlPageCategorizationProcessor(Initializer initializer,Queue<Url> frontier)
        {
            extractor = new Extractor();
            categorizer = new Categorizer(initializer.getCategoryList());
            ranker = new Ranker(categorizer);
            filter = new Filter("http://",initializer.getContraints());
            queueFronier = frontier;
            taskId = initializer.getTaskId();
        }
        /**
         * This method tries to process the given content assuming that the given content
         * can be processed via this processor
         */
        public void process(ResourceContent resource)
        {
            DateTime startTime = DateTime.Now;

            List<LinkItem> listOfLinks;
            //extract all the links in page
            listOfLinks = extractor.extractLinks(resource.getResourceUrl(), resource.getResourceContent());
            RuntimeStatistics.addToExtractedUrls(listOfLinks.Count);

            DateTime extEndTime = DateTime.Now;
            
            /*** 1. Extracting the link from the request ***/ 
            TimeSpan extRequest = extEndTime - startTime;

            //reset the dictionary in filter that contains the urls from the same page
            filter.resetDictionary();
            foreach(LinkItem item in listOfLinks )
            {
                //Filter the links and return only links that can be crawled
                List<String>links = new List<String>();
                links.Add(item.getLink());
                List<String> filteredLinks = filter.filterLinks(links);

                //If filteredLinks is not empty 
                if (filteredLinks.Count > 0)
                {
 
                    Url url = new Url(filteredLinks[0], hashUrl(filteredLinks[0]), ranker.rankUrl(resource,item), 
                                      item.getDomainUrl(), hashUrl(item.getDomainUrl()));
                    deployLinksToFrontier(url);
                    RuntimeStatistics.addToFeedUrls(1);
                    
                }
            }

            DateTime catStartTime = DateTime.Now;

            /*** 2. Ranking and deployment to the frontier ***/ 
            TimeSpan rankTotalRequest = catStartTime - extEndTime;

            //Ascribe the url to all the categories it is belonged to.
            List<Result> classifiedResults = categorizer.classifyContent(resource.getResourceContent(),
                                                                            resource.getResourceUrl());
            if (classifiedResults.Count != 0) RuntimeStatistics.addToCrawledUrls(1);
            DateTime catEndTime = DateTime.Now;

            /*** 3. Classification of the current request ***/ 
            TimeSpan catTotalRequest = catEndTime - catStartTime;

            //Save all the results to Storage
            foreach (Result classifiedResult in classifiedResults)
            {
                Result result = new Result("0", classifiedResult.getUrl(),classifiedResult.getCategoryID(),
                            resource.getRankOfUrl(), classifiedResult.getTrustMeter());
                deployResourceToStorage(result);
            }

            DateTime endTime = DateTime.Now;
            /*** 4. deployment to the database (result) ***/ 
            TimeSpan deployRequest = endTime - catEndTime;

            /*** $. Total processing time ***/ 
            TimeSpan totalRequest = endTime - startTime;
        }

        /**
         * This method returns a boolean value which indicates if the given resource
         * can be processed by the processor or not
         */
        public bool canProcess(ResourceContent resource)
        {
            return (resource.isValid());
        }

        /*
         * This method saves one result to the data base.
         */
        public void deployResourceToStorage(Result result)
        {
            //System.Console.WriteLine("[*] System.Threading.Thread.CurrentThread.ID is " + System.Threading.Thread.CurrentThread.ManagedThreadId);
            if (taskId != "")
            {
                StorageSystem.StorageSystem.getInstance().addURLResult(taskId, result);
            }
            /*
            StreamWriter sw = new StreamWriter("Storage" + System.Threading.Thread.CurrentThread.ManagedThreadId + ".txt", true);
            string temp = result.ToString().Replace("[-]", " " + System.Environment.NewLine);
            sw.WriteLine(temp);
            sw.Close();
            */
        }

        /*
         * This method saves all the extracted links to a file.
         * this method is temporary, may not be necessary 
         * anymore once we  built the frontier.
         */
        public void deployLinksToFrontier(Url urlProcessed)
        {
            /*
            StreamWriter sw = new StreamWriter("Frontier.txt", true);
            string temp = urlProcessed.ToString().Replace("[-]", " " + System.Environment.NewLine);
            sw.WriteLine(temp);
            sw.Close();
            */
            SyncAccessor.putInQueue<Url>(queueFronier, urlProcessed);
        }

        /*
         * This method calculates a unique hash number for the
         * url.
         */
        public int hashUrl(String urlName)
        {
            return urlName.GetHashCode();
        }

    }
}

using System;
using System.Collections.Generic;
using System.Text;
using CrawlerNameSpace.Utilities;
using System.IO;

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


        public HtmlPageCategorizationProcessor(Initializer initializer,Queue<Url> frontier)
        {
            extractor = new Extractor();
            categorizer = new Categorizer(initializer.getCategoryList());
            ranker = new Ranker(categorizer);
            filter = new Filter("http://",initializer.getContraints());
            queueFronier = frontier;
        }
        /**
         * This method tries to process the given content assuming that the given content
         * can be processed via this processor
         */
        public void process(ResourceContent resource)
        {
            List<LinkItem> listOfLinks;
            //extract all the links in page
            listOfLinks = extractor.extractLinks(resource.getResourceUrl(), resource.getResourceContent()); 
            
            foreach(LinkItem item in listOfLinks )
            {
                //Filter the links and return only links that can be crawled
                List<String>links = new List<String>();
                links.Add(item.getLink());
                List<String> filteredLinks = filter.filterLinks(links);

                //If filteredLinks is not empty 
                if (filteredLinks.Count > 0)
                {
                    Url url = new Url(filteredLinks[0], hashUrl(filteredLinks[0]), ranker.rankUrl(resource.getRankOfUrl(),
                      resource.getResourceContent(), filteredLinks[0]), item.getDomainUrl(), hashUrl(item.getDomainUrl()));
                    deployLinksToFrontier(url);
                }
            }

            //Ascribe the url to all the categories it is belonged to.
            List<Result> classifiedResults = categorizer.classifyContent(resource.getResourceContent(),
                                                                            resource.getResourceUrl());

            //Save all the results to Storage
            foreach (Result classifiedResult in classifiedResults)
            {
                Result result = new Result("0", classifiedResult.getUrl(),classifiedResult.getCategoryID(),
                            resource.getRankOfUrl(), classifiedResult.getTrustMeter());
                deployResourceToStorage(result);
            }
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
            StreamWriter sw = new StreamWriter("Storage.txt", true);
            string temp = result.ToString().Replace("[-]", " " + System.Environment.NewLine);
            sw.WriteLine(temp);
            sw.Close();
        }

        /*
         * This method saves all the extracted links to a file.
         * this method is temporary, may not be necessary 
         * anymore once we  built the frontier.
         */
        public void deployLinksToFrontier(Url urlProcessed)
        {
            /*StreamWriter sw = new StreamWriter("Frontier.txt", true);
            string temp = urlProcessed.ToString().Replace("[-]", " " + System.Environment.NewLine);
            sw.WriteLine(temp);
            sw.Close();
            */
            queueFronier.Enqueue(urlProcessed); 
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

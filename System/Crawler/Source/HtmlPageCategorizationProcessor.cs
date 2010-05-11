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


        public HtmlPageCategorizationProcessor(Initializer initializer)
        {
            extractor = new Extractor();
            categorizer = new Categorizer(initializer.getCategoryList());
            ranker = new Ranker(categorizer);
            filter = new Filter("http://",initializer.getContraints());
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

            //transfer the list "listOfLinks" to list of strings
            List<String> links = new List<string>(); 
            
            foreach(LinkItem item in listOfLinks )
            {
                //Filter the links and return only links that can be crawled
                links = new List<String>();
                links.Add(item.getLink());
                List<String> filteredLinks = filter.filterLinks(links);

                //If filteredLinks is not empty 
                if (filteredLinks.Count > 0)
                {
                    Url url = new Url(filteredLinks[0], hashUrl(filteredLinks[0]), ranker.rankUrl(resource.getRankOfUrl(),
                      resource.getResourceContent(), filteredLinks[0]));
                    deployLinksToFrontier(url);
                }
            }

            //Ascribe the url to all the categories it is belonged to.
            categorizer.classifyContent(resource.getResourceContent(), resource.getResourceUrl());
            List<String> categories = categorizer.getSuitableCategoryName(resource.getResourceContent(),resource.getResourceUrl());

            //Save all the results to Storage
            foreach (String category in categories)
            {
                Result result = new Result("0", resource.getResourceUrl(),"0",
                            resource.getRankOfUrl(), 
                            categorizer.getMatchLevel(resource.getResourceContent(),category));
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
            sw.WriteLine(result.ToString());
            sw.Close();
        }

        /*
         * This method saves all the extracted links to a file.
         * this method is temporary, may not be necessary 
         * anymore once we  built the frontier.
         */
        public void deployLinksToFrontier(Url urlProcessed)
        {
            StreamWriter sw = new StreamWriter("Frontier.txt", true);
            sw.WriteLine(urlProcessed.ToString());
            sw.Close();
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

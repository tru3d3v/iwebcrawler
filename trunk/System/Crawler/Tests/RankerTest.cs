using System;
using System.Collections.Generic;
using System.Text;
using CrawlerNameSpace.Utilities;

namespace CrawlerNameSpace.Tests
{
    public class RankerTest
    {
        private ResourceContent newresource = new ResourceContent("www.ynet." +
            "co.il", ResourceType.HtmlResource, "This is an example test content of this resource", 200, 0);

        private Ranker testRanker = new Ranker(new Categorizer(new List<Category>()));

        private Queue<Url> queueFronier = new Queue<Url>();

        private String taskId = "20dae795-95d2-4397-b5af-ecf75494b64a";

        public void Test1()
        {
            LinkItem item = new LinkItem();

            if (testRanker.rankUrl(newresource, item) == 0)
            {
                Console.WriteLine("rankUrl(int parentRank, String parentContent, String url) PASSED");
            }
            else
            {
                Console.WriteLine("rankUrl(int parentRank, String parentContent, String url) FAILED");
                Console.WriteLine("Should have returned 0");
            }
        }

        public void Test2()
        {
            List<String> urls = new List<string>();
            urls.Add("http://www.autonews.com/");
            urls.Add("http://www.geonius.com/www/");
            urls.Add("http://en.wikipedia.org/wiki/Main_Page");
            urls.Add("http://www.computerworld.com/");
            List<string> seeds = StorageSystem.StorageSystem.getInstance().getSeedList(taskId);
            foreach (string seed in seeds)
            {
                urls.Add(seed);
            }

            List<Category> _categories;
            Constraints _constraints;
            
            _categories = StorageSystem.StorageSystem.getInstance().getCategories(taskId);
            _constraints = StorageSystem.StorageSystem.getInstance().getRestrictions(taskId);

            StorageSystem.StorageSystem.getInstance().getSeedList(taskId);
            Filter filter = new Filter("http://", _constraints);
            Categorizer categorizer = new Categorizer(_categories);
            Ranker ranker = new Ranker(categorizer);
            Extractor extractor = new Extractor();

            HttpResourceFetcher httpfetcher = new HttpResourceFetcher();


            foreach (String url in urls)
            {
                DateTime startTime = DateTime.Now;
                ResourceContent resource = null;
                if (httpfetcher.canFetch(url))
                    resource = httpfetcher.fetch(url, 10000, 100);

                DateTime fetchEndTime = DateTime.Now;

                if ((resource == null)||(resource.getResourceContent()==null))
                    continue;

                /*** 0. fetching the link from the internet ***/
                TimeSpan fetchingTime = fetchEndTime - startTime;

                List<LinkItem> listOfLinks = new List<LinkItem>();
                //extract all the links in page
                listOfLinks = extractor.extractLinks(resource.getResourceUrl(), resource.getResourceContent());
                RuntimeStatistics.addToExtractedUrls(listOfLinks.Count);

                DateTime extEndTime = DateTime.Now;

                /*** 1. Extracting the link from the request ***/
                TimeSpan extRequest = extEndTime - fetchEndTime;

                //reset the dictionary in filter that contains the urls from the same page
                filter.resetDictionary();
                int filteredUrlsCount = 0;
                foreach (LinkItem item in listOfLinks)
                {
                    //Filter the links and return only links that can be crawled
                    List<String> links = new List<String>();
                    links.Add(item.getLink());
                    List<String> filteredLinks = filter.filterLinks(links);

                    //If filteredLinks is not empty 
                    if (filteredLinks.Count > 0)
                    {
                        filteredUrlsCount++;
                        Url url1 = new Url(filteredLinks[0], hashUrl(filteredLinks[0]), ranker.rankUrl(resource, item),
                                          item.getDomainUrl(), hashUrl(item.getDomainUrl()));
                        deployLinksToFrontier(url1);
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

                foreach (Result classifiedResult in classifiedResults)
                {
                     Result result = new Result("0", classifiedResult.getUrl(), classifiedResult.getCategoryID(),
                                 resource.getRankOfUrl(), classifiedResult.getTrustMeter());
                     deployResourceToStorage(result);
                }

                DateTime endTime = DateTime.Now;

                /*** 4. deployment to the database (result) ***/
                TimeSpan deployRequest = endTime - catEndTime;

                /*** 5. Total processing time ***/
                TimeSpan totalRequest = endTime - startTime;
            }
        }

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

        public int hashUrl(String urlName)
        {
            return urlName.GetHashCode();
        }

        /**
         * This method is the main test that gathers all the tests.
         */
        public static void MainTest()
        {
            RankerTest rankerTest = new RankerTest();
            rankerTest.Test1();
        }
    }
}

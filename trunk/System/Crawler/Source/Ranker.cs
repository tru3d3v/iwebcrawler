using System;
using System.Collections.Generic;
using System.Text;
using CrawlerNameSpace.Utilities;
using System.IO;

namespace CrawlerNameSpace
{
    /**
     * This Class is responsible for giving a rank to  the url
     * that has been fetched.
     */
    class Ranker
    {
        private Categorizer categorizer;
        private  const int defualtRank = 0;
        private const double MinAndMaxRATIO = 0.7;
        private const int ConfidenceLevelOfAnchor = 50;
        private const double ALPHA = 0.5;
        private const double BETTA = 0.6;
        private const double GAMMA = 0.2;
       
        public Ranker(Categorizer categorizer)
        {
            this.categorizer = categorizer;
        }

        /**
         * This method calculates the rank of a given url and returns it.
         */
        public int rankUrl(ResourceContent parentResource,LinkItem item)
        {
            //These variables will contain the ranks for the whole content match and nearby text match and 
            //anchor match and the parentrank.
            int rankParentUrl = parentResource.getRankOfUrl();
            int anchorRank = 0;
            int wholePageRank = 0;
            int nearbyTextRank = 0;

            int neighborhood = 0;
            int context = 0;
            int inherited = 0;

            StreamWriter sw = new
            StreamWriter("DataForRank" + System.Threading.Thread.CurrentThread.ManagedThreadId + ".txt", true);
            sw.WriteLine(" *********HEAD REQUEST *********************************************");
            sw.WriteLine(" ***** DATA FOR RANKER******************************************** ");
            sw.WriteLine(" URL : " + item.getLink());
            sw.WriteLine(" PARENT URL : " + item.getParentUrl());
            sw.Close();

            //rank of the whole page
            //wholePageRank = getRankOfWholeContent(parentResource);

            //rank of the nearby text
            nearbyTextRank =getRankOfNearbyText(item);

            //rank of the anchor url
            anchorRank = getRankOfAnchor(item);

            //rank of the neighborhood,that includes rank of the anchor and the nearby text
            if (anchorRank > ConfidenceLevelOfAnchor)
                context = 100;
            else
                context = nearbyTextRank;

            neighborhood = (int)(BETTA * anchorRank + (1 - BETTA) * context);

            //rank of the inherited,that includes the rank of the parentUrl and paren content
            inherited = (int)(ALPHA * rankParentUrl + (1-ALPHA) * wholePageRank);

            sw = new StreamWriter("DataForRank" + System.Threading.Thread.CurrentThread.ManagedThreadId + ".txt", true);
            sw.WriteLine(" .PARENT RANK: ");
            sw.WriteLine(rankParentUrl);
            sw.WriteLine(" .NEIGHBORHOOD: ");
            sw.WriteLine(neighborhood);
            sw.WriteLine(" .INHERITED: ");
            sw.WriteLine(inherited);
            sw.WriteLine(" .RANK OF THE URL: ");
            sw.WriteLine((int)(GAMMA * inherited + (1 - GAMMA) * neighborhood));
           // sw.WriteLine(" * END ****************************************************************** ");
            sw.Close();

            return ((int)(GAMMA * inherited + (1 - GAMMA) * neighborhood));
        }

        /**
         * This method gets a list of integer numbers and returns the maximum number of them.
         */
        public int calculateMax(List<int> integerNums)
        {
            int max = 0;

            foreach (int num in integerNums)
            {
                if (num > max) max = num;
            }

            return max;
        }

        /**
         * This method gets a list of integer numbers and returns the average number of the 
         * numbers.
         */
        public int calculateAvg(List<int> integerNums)
        {
            int sum = 0;
            int N = integerNums.Count;

            foreach (int num in integerNums)
            {
                sum += num;
            }

            return (sum / N);
        }
        
        /**
         * This method gets a url and parses it and returns the name of the url,without the prefix and suffix.
         */
        private String getParsedUrl(String url)
        {
            char[] sperator = {'.'};
          
            String[] firstParse = url.Split(sperator, System.StringSplitOptions.RemoveEmptyEntries);

            if (firstParse.Length != 3)
                return null;
            else
                return firstParse[1];
        }

        /**
         * This method returns a rank for the whole page content.
         */
        private int getRankOfWholeContent(ResourceContent resource)
        {
            //These variables will contain the max and avg of the match levels of the whole
            //content of parentReasource.
            int maxMatchLevelForContent = 0;
            int avgMatchLevelForContent = 0;

            StreamWriter sw = new
            StreamWriter("DataForRank" + System.Threading.Thread.CurrentThread.ManagedThreadId + ".txt", true);
            sw.WriteLine(" ***** REQUEST FOR WHOLE CONTENT RANK******************************** ");
            //sw.WriteLine(" URL : " + resource.getResourceUrl());
            sw.Close();
            //calculate the min and max of the match levels of the whole resource content to the categories.
            CategorizerOptions options = new CategorizerOptions();
            List<int> matchLevelsForContent = categorizer.classifyContentToAllCategories(resource.getResourceContent().Substring(0),options);
            maxMatchLevelForContent = calculateMax(matchLevelsForContent);
            avgMatchLevelForContent = calculateAvg(matchLevelsForContent);

            sw = new StreamWriter("DataForRank" + System.Threading.Thread.CurrentThread.ManagedThreadId + ".txt", true);
            sw.WriteLine(" .MAX MATCH LEVEL OF WHOLE CONTENT: ");
            sw.WriteLine(maxMatchLevelForContent);
            sw.WriteLine(" .AVG MATCH LEVEL OF WHOLE CONTENT: ");
            sw.WriteLine(avgMatchLevelForContent);
            sw.WriteLine(" .RANK OF WHOLE CONTENT: ");
            sw.WriteLine((int)(MinAndMaxRATIO * maxMatchLevelForContent + (1 - MinAndMaxRATIO) * avgMatchLevelForContent));
           // sw.WriteLine(" * END ****************************************************************** ");
            sw.Close();

            return ((int)(MinAndMaxRATIO * maxMatchLevelForContent + (1 - MinAndMaxRATIO) * avgMatchLevelForContent));
        }

        /**
         * This method returns a rank for the nearby textof the url
         */
        private int getRankOfNearbyText(LinkItem item)
        {
            //These variables will contain the max and avg of the match levels of the nearby
            //text of the extracted url.
            int maxMatchLevelForNearby = 0;
            int avgMatchLevelForNearby = 0;

            StreamWriter sw = new
            StreamWriter("DataForRank" + System.Threading.Thread.CurrentThread.ManagedThreadId + ".txt", true);
            sw.WriteLine(" ***** REQUEST FOR NEARBY TEXT RANK************************************ ");
            sw.WriteLine(" URL : " + item.getLink());
            sw.WriteLine(" CONTENT OF NEARBY TEXT:");
            sw.WriteLine(item.getText());
            sw.Close();
            //calculate the min and max of the match levels of the nearby text to the categories.
            CategorizerOptions options = new CategorizerOptions();
            options.ALPHA = 450;
            options.GAMMA = 120;
            options.MIN_WORDS_LIMIT = 1;
            options.isRank = true;
            List<int> matchLevelsForNearby = categorizer.classifyContentToAllCategories(item.getText(),options);
            maxMatchLevelForNearby = calculateMax(matchLevelsForNearby);
            avgMatchLevelForNearby = calculateAvg(matchLevelsForNearby);

            sw = new StreamWriter("DataForRank" + System.Threading.Thread.CurrentThread.ManagedThreadId + ".txt", true);
            sw.WriteLine(" .MAX MATCH LEVEL OF NEARBY TEXT: ");
            sw.WriteLine(maxMatchLevelForNearby);
            sw.WriteLine(" .AVG MATCH LEVEL OF NEARBY TEXT: ");
            sw.WriteLine(avgMatchLevelForNearby);
            sw.WriteLine(" .RANK OF NEARBY TEXT: ");
            sw.WriteLine((int)(MinAndMaxRATIO * maxMatchLevelForNearby + (1 - MinAndMaxRATIO) * avgMatchLevelForNearby));
           // sw.WriteLine(" * END ****************************************************************** ");
            sw.Close();

            return ((int)(MinAndMaxRATIO * maxMatchLevelForNearby + (1 - MinAndMaxRATIO) * avgMatchLevelForNearby));
        }

        /**
         * This method returns a rank for the anchor url
         */
        private int getRankOfAnchor(LinkItem item)
        {
            //These variables will contain the max and avg of the match levels of the Anchor Url
            int maxMatchLevelForAnchor = 0;
            int avgMatchLevelForAnchor = 0;

            if (item.getAnchor() == null)
                return 0;
            StreamWriter sw = new
            StreamWriter("DataForRank" + System.Threading.Thread.CurrentThread.ManagedThreadId + ".txt", true);
            sw.WriteLine(" ***** REQUEST FOR ANCHOR URL RANK************************************ ");
            sw.WriteLine(" URL : " + item.getLink());
            sw.WriteLine(" CONTENT OF ANCHOR:");
            sw.WriteLine(item.getAnchor());
            sw.Close();
            //calculate the min and max of the match levels of the anchor url to the categories.
            CategorizerOptions options = new CategorizerOptions();
            options.ALPHA = 10;
            options.GAMMA = 150;
            options.MIN_WORDS_LIMIT = 1;
            options.isRank = true;
            List<int> matchLevelsForAnchor = categorizer.classifyContentToAllCategories(item.getAnchor(),options);
            maxMatchLevelForAnchor = calculateMax(matchLevelsForAnchor);
            avgMatchLevelForAnchor = calculateAvg(matchLevelsForAnchor);

            sw = new StreamWriter("DataForRank" + System.Threading.Thread.CurrentThread.ManagedThreadId + ".txt", true);
            sw.WriteLine(" .MAX MATCH LEVEL OF ANCHOR: ");
            sw.WriteLine(maxMatchLevelForAnchor);
            sw.WriteLine(" .AVG MATCH LEVEL OF ANCHOR: ");
            sw.WriteLine(avgMatchLevelForAnchor);
            sw.WriteLine(" .RANK OF ANCHOR: ");
            sw.WriteLine((int)(MinAndMaxRATIO * maxMatchLevelForAnchor + (1 - MinAndMaxRATIO) * avgMatchLevelForAnchor));
            //sw.WriteLine(" * END ****************************************************************** ");
            sw.Close();
            return ((int)(MinAndMaxRATIO * maxMatchLevelForAnchor + (1 - MinAndMaxRATIO) * avgMatchLevelForAnchor));
        }
    }
}

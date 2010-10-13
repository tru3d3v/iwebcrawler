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
        //private RankerOptions RankParams;

        private static String lastResourceContent = null;
        private static int wholePageRank = 0;

        private static CategorizerOptions anchorOptions = null;
        private static CategorizerOptions nearbyOptions =null;
        private static CategorizerOptions wholeContentOptions = null;

        int sumOfTotalNearbyWords = 0;
        int NumOfLinks = 0;
        int sumOfTotalAnchorWords = 0;
        /*
        private const double MinAndMaxRATIO = 0.75;
        private const int ConfidenceLevelOfAnchor = 75;
        private const double ALPHA = 0.5;
        private const double BETTA = 0.4;
        private const double GAMMA = 0.2;
        */
        public Ranker(Categorizer categorizer)
        {
            this.categorizer = categorizer;
            //this.RankParams = RankerParameters;
        }

        /**
         * This method calculates the rank of a given url and returns it.
         */
        public int rankUrl(ResourceContent parentResource,LinkItem item)
        {
            DateTime startRankTime = DateTime.Now;
            //These variables will contain the ranks for the whole content match and nearby text match and 
            //anchor match and the parentrank.
            int rankParentUrl = parentResource.getRankOfUrl();
            int anchorRank = 0;
            //int wholePageRank = 0;
            int nearbyTextRank = 0;

            int neighborhood = 0;
            int context = 0;
            int inherited = 0;

            char[] separators = {' ', '\t', '\n'};
            NumOfLinks++;
            sumOfTotalNearbyWords += item.getText().Split(separators).Length;
            sumOfTotalAnchorWords += item.getAnchor().Split(separators, StringSplitOptions.RemoveEmptyEntries).Length;

            StreamWriter sw = null;
            if (LogDebuggerControl.getInstance().debugRanker)
            {
                sw = new
                StreamWriter("DataForRank" + System.Threading.Thread.CurrentThread.ManagedThreadId + ".txt", true);
                sw.WriteLine(" *********HEAD REQUEST *********************************************");
                sw.WriteLine(" ***** DATA FOR RANKER******************************************** ");
                sw.WriteLine(" URL : " + item.getLink());
                sw.WriteLine(" PARENT URL : " + item.getParentUrl());
                sw.Close();
            }
            DateTime wholePageStartTime = DateTime.Now;

            //rank of the whole page
            if (!((lastResourceContent!=null)&&(lastResourceContent.Equals(parentResource.getResourceContent()))))
            {
                lastResourceContent=parentResource.getResourceContent();
                wholePageRank = getRankOfWholeContent(parentResource);
            }

            DateTime wholePageEndTime = DateTime.Now;

            /****Time of Whole Contet inlize****/
            TimeSpan totalWholePageTime = wholePageEndTime - wholePageStartTime;
            double c = totalWholePageTime.TotalSeconds;
            //rank of the nearby text
            nearbyTextRank =getRankOfNearbyText(item);

            DateTime endTimeOfNearby = DateTime.Now;

            /**** time of nearby text *****/
            TimeSpan totalNearby = endTimeOfNearby - wholePageEndTime;
            double b = totalNearby.TotalSeconds;
            //rank of the anchor url
            anchorRank = getRankOfAnchor(item);

            DateTime endTimeOfAnchor = DateTime.Now;
 
            /*** total time of anchor ****/
            TimeSpan totalAnchorTime = endTimeOfAnchor - endTimeOfNearby;
            double a = totalAnchorTime.TotalSeconds;
            //rank of the neighborhood,that includes rank of the anchor and the nearby text
            if (anchorRank > RankerOptions.ConfidenceLevelOfAnchor)
                context = 100;
            else
            {
                //nearbyTextRank = getRankOfNearbyText(item);
                context = nearbyTextRank;
            }
            neighborhood = (int)(RankerOptions.BETTA * anchorRank + (1 - RankerOptions.BETTA) * context);

            //rank of the inherited,that includes the rank of the parentUrl and paren content
            inherited = (int)(RankerOptions.ALPHA * rankParentUrl + (1 - RankerOptions.ALPHA) * wholePageRank);

            if (LogDebuggerControl.getInstance().debugRanker)
            {
                sw = new StreamWriter("DataForRank" + System.Threading.Thread.CurrentThread.ManagedThreadId + ".txt", true);
                sw.WriteLine("************************DATA CONCLUSION*************************");
                sw.WriteLine(" .PARENT RANK: ");
                sw.WriteLine(rankParentUrl);
                sw.WriteLine(" .RANK OF NEARBY TEXT: ");
                sw.WriteLine(nearbyTextRank);
                sw.WriteLine(" .AVG OF NEARBY WORDS");
                sw.WriteLine((int)(sumOfTotalNearbyWords / NumOfLinks));
                sw.WriteLine(" .RANK OF ANCHOR: ");
                sw.WriteLine(anchorRank);
                sw.WriteLine(" .AVG OF ANCHOR TEXT");
                sw.WriteLine((int)(sumOfTotalAnchorWords / NumOfLinks));
                sw.WriteLine(" .NEIGHBORHOOD: ");
                sw.WriteLine(neighborhood);
                sw.WriteLine(" .RANK OF WHOLE CONTENT: ");
                sw.WriteLine(wholePageRank);
                sw.WriteLine(" .INHERITED: ");
                sw.WriteLine(inherited);
                sw.WriteLine(" .RANK OF THE URL: ");
                sw.WriteLine((int)(RankerOptions.GAMMA * inherited + (1 - RankerOptions.GAMMA) * neighborhood));
                // sw.WriteLine(" * END ****************************************************************** ");
                sw.Close();
            }

            DateTime endTimeOfRanking = DateTime.Now;
            /*** End time of ranking url ****/
            TimeSpan totalRankingTime = endTimeOfRanking - startRankTime;
            Console.WriteLine(totalRankingTime.TotalSeconds);
            return ((int)(RankerOptions.GAMMA * inherited + (1 - RankerOptions.GAMMA) * neighborhood));
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
         * This method returns a rank for the whole page content.
         */
        private int getRankOfWholeContent(ResourceContent resource)
        {
            //These variables will contain the max and avg of the match levels of the whole
            //content of parentReasource.
            int maxMatchLevelForContent = 0;
            int avgMatchLevelForContent = 0;

            StreamWriter sw = null;
            if (LogDebuggerControl.getInstance().debugRanker)
            {
                sw = new
                StreamWriter("DataForRank" + System.Threading.Thread.CurrentThread.ManagedThreadId + ".txt", true);
                sw.WriteLine(" ***** REQUEST FOR WHOLE CONTENT RANK******************************** ");
                //sw.WriteLine(" URL : " + resource.getResourceUrl());
                sw.Close();
            }
            //calculate the min and max of the match levels of the whole resource content to the categories.
            if (wholeContentOptions == null)
            {
                wholeContentOptions = getOptions("wholeContent");
            }

            List<int> matchLevelsForContent = categorizer.classifyContentToAllCategories(resource.getResourceContent().Substring(0), wholeContentOptions);
            maxMatchLevelForContent = calculateMax(matchLevelsForContent);
            avgMatchLevelForContent = calculateAvg(matchLevelsForContent);

            if (LogDebuggerControl.getInstance().debugRanker)
            {
                sw = new StreamWriter("DataForRank" + System.Threading.Thread.CurrentThread.ManagedThreadId + ".txt", true);
                sw.WriteLine(" .MAX MATCH LEVEL OF WHOLE CONTENT: ");
                sw.WriteLine(maxMatchLevelForContent);
                sw.WriteLine(" .AVG MATCH LEVEL OF WHOLE CONTENT: ");
                sw.WriteLine(avgMatchLevelForContent);
                //sw.WriteLine(" .RANK OF WHOLE CONTENT: ");
                //sw.WriteLine((int)(RankParams.MinAndMaxRATIO * maxMatchLevelForContent + (1 - RankParams.MinAndMaxRATIO) * avgMatchLevelForContent));
                // sw.WriteLine(" * END ****************************************************************** ");
                sw.Close();
            }

            return ((int)(RankerOptions.MinAndMaxRATIO * maxMatchLevelForContent + (1 - RankerOptions.MinAndMaxRATIO) * avgMatchLevelForContent));
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

            StreamWriter sw = null;
            if (LogDebuggerControl.getInstance().debugRanker)
            {
                sw = new
                StreamWriter("DataForRank" + System.Threading.Thread.CurrentThread.ManagedThreadId + ".txt", true);
                sw.WriteLine(" ***** REQUEST FOR NEARBY TEXT RANK************************************ ");
                sw.WriteLine(" URL : " + item.getLink());
                sw.WriteLine(" CONTENT OF NEARBY TEXT:");
                sw.WriteLine(item.getText());
                sw.Close();
            }

            //calculate the min and max of the match levels of the nearby text to the categories.
            if (nearbyOptions == null)
            {
                nearbyOptions = getOptions("nearby");
            }

            List<int> matchLevelsForNearby = categorizer.classifyContentToAllCategories(item.getText(), nearbyOptions);
            maxMatchLevelForNearby = calculateMax(matchLevelsForNearby);
            avgMatchLevelForNearby = calculateAvg(matchLevelsForNearby);

            if (LogDebuggerControl.getInstance().debugRanker)
            {
                sw = new StreamWriter("DataForRank" + System.Threading.Thread.CurrentThread.ManagedThreadId + ".txt", true);
                sw.WriteLine(" .MAX MATCH LEVEL OF NEARBY TEXT: ");
                sw.WriteLine(maxMatchLevelForNearby);
                sw.WriteLine(" .AVG MATCH LEVEL OF NEARBY TEXT: ");
                sw.WriteLine(avgMatchLevelForNearby);
                //sw.WriteLine(" .RANK OF NEARBY TEXT: ");
                //sw.WriteLine((int)(RankParams.MinAndMaxRATIO * maxMatchLevelForNearby + (1 - RankParams.MinAndMaxRATIO) * avgMatchLevelForNearby));
                // sw.WriteLine(" * END ****************************************************************** ");
                sw.Close();
            }

            return ((int)(RankerOptions.MinAndMaxRATIO * maxMatchLevelForNearby + (1 - RankerOptions.MinAndMaxRATIO) * avgMatchLevelForNearby));
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

            StreamWriter sw = null;
            if (LogDebuggerControl.getInstance().debugRanker)
            {
                sw = new
                StreamWriter("DataForRank" + System.Threading.Thread.CurrentThread.ManagedThreadId + ".txt", true);
                sw.WriteLine(" ***** REQUEST FOR ANCHOR URL RANK************************************ ");
                sw.WriteLine(" URL : " + item.getLink());
                sw.WriteLine(" CONTENT OF ANCHOR:");
                sw.WriteLine(item.getAnchor());
                sw.Close();
            }

            //calculate the min and max of the match levels of the anchor url to the categories.
            if (anchorOptions == null)
            {
                anchorOptions = getOptions("anchor");
            }

            List<int> matchLevelsForAnchor = categorizer.classifyContentToAllCategories(item.getAnchor(), anchorOptions);
            maxMatchLevelForAnchor = calculateMax(matchLevelsForAnchor);
            avgMatchLevelForAnchor = calculateAvg(matchLevelsForAnchor);

            if (LogDebuggerControl.getInstance().debugRanker)
            {
                sw = new StreamWriter("DataForRank" + System.Threading.Thread.CurrentThread.ManagedThreadId + ".txt", true);
                sw.WriteLine(" .MAX MATCH LEVEL OF ANCHOR: ");
                sw.WriteLine(maxMatchLevelForAnchor);
                sw.WriteLine(" .AVG MATCH LEVEL OF ANCHOR: ");
                sw.WriteLine(avgMatchLevelForAnchor);
                //sw.WriteLine(" .RANK OF ANCHOR: ");
                //sw.WriteLine((int)(RankParams.MinAndMaxRATIO * maxMatchLevelForAnchor + (1 - RankParams.MinAndMaxRATIO) * avgMatchLevelForAnchor));
                //sw.WriteLine(" * END ****************************************************************** ");
                sw.Close();
            }

            return ((int)(RankerOptions.MinAndMaxRATIO * maxMatchLevelForAnchor + (1 - RankerOptions.MinAndMaxRATIO) * avgMatchLevelForAnchor));
        }

        /**
         * This method creates Categorizer Options and returns it.
         * The values of the variables of the new object are brought from the data base.
         * Note : In case the operation mode is manual the variables will be default ones,
         *        such is the case when the returns values from the data base are nulls.
         */
        private CategorizerOptions getOptions(String optionsType)
        {
            CategorizerOptions options = new CategorizerOptions();

            if (WorkDetails.getOperationMode() == operationMode_t.Auto)
            {
                switch (optionsType)
                {
                    case "anchor":
                        options.ALPHA = RankerOptions.ANC_ALPHA;
                        options.BETA = RankerOptions.ANC_BETA;
                        options.GAMMA = RankerOptions.ANC_GAMMA;
                        options.MIN_WORDS_LIMIT = RankerOptions.ANC_MIN;
                        options.MIN_WORDS_PENLTY = RankerOptions.ANC_PENLTY;
                        options.isRank = true;
                        options.NONZERO_MAX_EFFECT = 0;
                        break;
                    case "wholeContent":
                        options.ALPHA = RankerOptions.CAT_ALPHA;
                        options.BETA = RankerOptions.CAT_BETA;
                        options.GAMMA = RankerOptions.CAT_GAMMA;
                        options.MIN_WORDS_LIMIT = RankerOptions.CAT_MIN;
                        options.MIN_WORDS_PENLTY = RankerOptions.CAT_PENLTY;
                        options.isRank = true;
                        break;
                    case "nearby":
                        options.ALPHA = RankerOptions.NER_ALPHA;
                        options.BETA = RankerOptions.NER_BETA;
                        options.GAMMA = RankerOptions.NER_GAMMA;
                        options.MIN_WORDS_LIMIT = RankerOptions.NER_MIN;
                        options.MIN_WORDS_PENLTY = RankerOptions.NER_PENLTY;
                        options.NONZERO_MAX_EFFECT = 40;
                        options.isRank = true;
                        break;
                    default:
                        goto case "wholeContent"; 
                }
            }

            return options;
        }
    }
}

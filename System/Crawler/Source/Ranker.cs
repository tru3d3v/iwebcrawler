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
        private RankerOptions RankParams;

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
        public Ranker(Categorizer categorizer,RankerOptions RankerParameters)
        {
            this.categorizer = categorizer;
            this.RankParams = RankerParameters;
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

            //rank of the whole page
            wholePageRank = getRankOfWholeContent(parentResource);

            //rank of the nearby text
            nearbyTextRank =getRankOfNearbyText(item);

            //rank of the anchor url
            anchorRank = getRankOfAnchor(item);

            //rank of the neighborhood,that includes rank of the anchor and the nearby text
            if (anchorRank > RankParams.ConfidenceLevelOfAnchor)
                context = 100;
            else
            {
                //nearbyTextRank = getRankOfNearbyText(item);
                context = nearbyTextRank;
            }
            neighborhood = (int)(RankParams.BETTA * anchorRank + (1 - RankParams.BETTA) * context);

            //rank of the inherited,that includes the rank of the parentUrl and paren content
            inherited = (int)(RankParams.ALPHA * rankParentUrl + (1-RankParams.ALPHA) * wholePageRank);

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
                sw.WriteLine((int)(RankParams.GAMMA * inherited + (1 - RankParams.GAMMA) * neighborhood));
                // sw.WriteLine(" * END ****************************************************************** ");
                sw.Close();
            }

            return ((int)(RankParams.GAMMA * inherited + (1 - RankParams.GAMMA) * neighborhood));
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
            CategorizerOptions options = getOptions("wholeContent");
            options.isRank = true;
            List<int> matchLevelsForContent = categorizer.classifyContentToAllCategories(resource.getResourceContent().Substring(0),options);
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

            return ((int)(RankParams.MinAndMaxRATIO * maxMatchLevelForContent + (1 - RankParams.MinAndMaxRATIO) * avgMatchLevelForContent));
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
            CategorizerOptions options = getOptions("nearby");
            /*
            options.ALPHA = 450;
            options.GAMMA = 120;
            options.MIN_WORDS_LIMIT = 1;
             */
            options.NONZERO_MAX_EFFECT = 40;
            options.isRank = true;
            List<int> matchLevelsForNearby = categorizer.classifyContentToAllCategories(item.getText(),options);
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

            return ((int)(RankParams.MinAndMaxRATIO * maxMatchLevelForNearby + (1 - RankParams.MinAndMaxRATIO) * avgMatchLevelForNearby));
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
            CategorizerOptions options = getOptions("anchor");
            
            /*options.ALPHA = 350;
            options.GAMMA = 0;
            options.MIN_WORDS_LIMIT = 1;
             */
            options.NONZERO_MAX_EFFECT = 0;
            options.isRank = true;
            List<int> matchLevelsForAnchor = categorizer.classifyContentToAllCategories(item.getAnchor(),options);
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

            return ((int)(RankParams.MinAndMaxRATIO * maxMatchLevelForAnchor + (1 - RankParams.MinAndMaxRATIO) * avgMatchLevelForAnchor));
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
                String alphaSearch = null,bettaSearch=null,gammaSearch=null,minSearch=null,penaltySearch=null; 
                switch (optionsType)
                {
                    case "anchor":
                        alphaSearch = TaskProperty.ANC_ALPHA.ToString();
                        bettaSearch = TaskProperty.ANC_BETA.ToString();
                        gammaSearch = TaskProperty.ANC_GAMMA.ToString();
                        minSearch = TaskProperty.ANC_MIN.ToString();
                        penaltySearch = TaskProperty.ANC_PENLTY.ToString();
                        break;
                    case "wholeContent":
                        alphaSearch = TaskProperty.CAT_ALPHA.ToString();
                        bettaSearch = TaskProperty.CAT_BETA.ToString();
                        gammaSearch = TaskProperty.CAT_GAMMA.ToString();
                        minSearch = TaskProperty.CAT_MIN.ToString();
                        penaltySearch = TaskProperty.CAT_PENLTY.ToString();
                        break;
                    case "nearby":
                        alphaSearch = TaskProperty.NER_ALPHA.ToString();
                        bettaSearch = TaskProperty.NER_BETA.ToString();
                        gammaSearch = TaskProperty.NER_GAMMA.ToString();
                        minSearch = TaskProperty.NER_MIN.ToString();
                        penaltySearch = TaskProperty.NER_PENLTY.ToString();
                        break;
                    default:
                        goto case "wholeContent"; 
                }

                String alpha = StorageSystem.StorageSystem.getInstance().getProperty(WorkDetails.getTaskId(),alphaSearch);
                String betta = StorageSystem.StorageSystem.getInstance().getProperty(WorkDetails.getTaskId(), bettaSearch);
                String gamma = StorageSystem.StorageSystem.getInstance().getProperty(WorkDetails.getTaskId(),gammaSearch);
                String min = StorageSystem.StorageSystem.getInstance().getProperty(WorkDetails.getTaskId(),minSearch);
                String penalty = StorageSystem.StorageSystem.getInstance().getProperty(WorkDetails.getTaskId(),penaltySearch);

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
    }
}

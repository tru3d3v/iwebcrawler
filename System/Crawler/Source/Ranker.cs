using System;
using System.Collections.Generic;
using System.Text;

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

        public Ranker(Categorizer categorizer)
        {
            this.categorizer = categorizer;
        }

        /**
         * This method calculates the rank of a given url and returns it.
         */
        public int rankUrl(ResourceContent parentResource,LinkItem item)
        {
            //These variables will contain the max and avg of the match levels of the whole
            //content of parentReasource.
            int maxMatchLevelForContent = 0;
            int avgMatchLevelForContent = 0;

            //These variables will contain the max and avg of the match levels of the nearby
            //text of the extracted url.
            int maxMatchLevelForNearby = 0;
            int avgMatchLevelForNearby = 0;

            int rankParentUrl = parentResource.getRankOfUrl();

            List<int> matchLevelsForContent = categorizer.classifyContentToAllCategories(parentResource.getResourceContent());
            maxMatchLevelForContent = calculateMax(matchLevelsForContent);
            avgMatchLevelForContent = calculateAvg(matchLevelsForContent);

            List<int> matchLevelsForNearby = categorizer.classifyContentToAllCategories(item.getText());
            maxMatchLevelForNearby = calculateMax(matchLevelsForNearby);
            avgMatchLevelForNearby = calculateAvg(matchLevelsForNearby);


            System.Console.WriteLine("The Maximum match level ");
            return defualtRank;
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


    }
}

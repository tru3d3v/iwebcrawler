using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace CrawlerNameSpace.Utilities
{
    /**
     * This Class represents a logic components which saves all
     * the data relating to specific category
     */
    public class Category
    {
        //These are consts that are used in the getMatchLevel method.
        private const double ALPHA = 2500;
        private const double BETA = 0.1;
        private const double GAMMA = 55;
        private const double MIN_WORDS_LIMIT = 5000;

        private const int NONZERO_WEIGHT = 50;
        private const int MATCH_WEIGHT   = 100;
        private const double NORMALIZE_CONST = 0.7;

        private const int MAX_MATCH_LEVEL = 100;

        private String       categoryID;
        private String       parentName;
        private String       categoryName;
        private List<String> keywordList;
        private int          confidenceLevel;

        /**
         * Constructor
         */
        public Category(String id, String pname, String name, List<String> keywords, int cl)
        {
            categoryID = id;
            parentName = pname;
            categoryName = name;
            keywordList = keywords;
            confidenceLevel = cl;
        }

        /**
         * This method returns the categoryID of the category
         */
        public String getCategoryID()
        {
            return categoryID;
        }

        /**
         * This method returns the parent name of the category
         */
        public String getParentName()
        {
            return parentName;
        }

        /**
         * This method returns the name of the category
         */
        public String getCatrgoryName()
        {
            return categoryName;
        }

        /**
         * This method returns the confidence level of the category
         */
        public int getConfidenceLevel()
        {
            return confidenceLevel;
        }

        /**
         * This method returns a list of the keywords of the category
         */
        public List<String> getKeywordList()
        {
            return keywordList;
        }

        /**
         * This method returns the match level of the given wordlist according to a
         * certain formula.
         */
        public int getMatchLevel(String wordList,double alpha, double min_words_limit,bool isRank) 
        {
            char[] separators = {' ', '\t', '\n'};
            int numOfWords    = Math.Max(1, wordList.Split(separators).Length);
            int numOfKeywords = Math.Max(1, keywordList.Count);
            int nonZero = 0;
            double sumOfhistogram = 0;
            double threshold = Math.Max(2.0, ((numOfWords * BETA) / numOfKeywords));

            // keywordList and wordList are copied to a new arrays so that we won't change them(the originals)
            List<String> keywordListCopied = new List<string>(keywordList);
            String wordListCopied = (String)wordList.Clone();
            
            // Transforming the keywordListCopied and wordListCopied to canonical form
            keywordListCopied.ForEach(canonicForm);
            canonicForm(wordListCopied);

            int[] histogram = new int[numOfKeywords];
            //Initialising the histogram array to zeros
            for (int i = 0; i < numOfKeywords; i++)
            {
                histogram[i] = 0;
            }

            foreach (String keyword in keywordListCopied)
            {
                Regex objPattern = new Regex(keyword);
                int count = objPattern.Matches(wordListCopied,0).Count;

                int index = keywordListCopied.IndexOf(keyword);
                if (count != 0 && histogram[index] == 0)
                    if (isRank)
                        nonZero += 2;
                    else
                        nonZero++;

                if (histogram[index] < threshold)
                {
                    int add = Math.Min(histogram[index] + count, (int)threshold) - histogram[index];
                    histogram[index] = histogram[index] + add;
                    sumOfhistogram = sumOfhistogram + add;
                }
            }
            
            double nonZeroBonus = (nonZero * GAMMA) / numOfKeywords;
            nonZeroBonus = Math.Min(NONZERO_WEIGHT, nonZeroBonus);
            double matchPercent = (sumOfhistogram * alpha) / numOfWords;
            matchPercent = Math.Min(MATCH_WEIGHT, matchPercent);
            double total = Math.Min(MAX_MATCH_LEVEL, NORMALIZE_CONST * (nonZeroBonus + matchPercent));
            if (numOfWords < min_words_limit)
            {
                total = 0.25 * total;
            }
           /* 
            StreamWriter sw = new 
                StreamWriter("Data" + System.Threading.Thread.CurrentThread.ManagedThreadId + ".txt", true);
            sw.WriteLine(" ***** DATA FOR REQUEST ************************************************* ");
            //sw.WriteLine(" .CONTENT WORDS: ");
            //sw.WriteLine(wordListCopied.ToString());
            sw.WriteLine(" .NUM OF WORDS: ");
            sw.WriteLine(numOfWords.ToString());
            sw.WriteLine(" .KEY WORDS: ");
            sw.WriteLine(keywordListCopied.ToString());
            sw.WriteLine(" .NUM OF KEY WORDS: ");
            sw.WriteLine(numOfKeywords.ToString());
            sw.WriteLine(" .THRESOLD PARAM: ");
            sw.WriteLine(threshold.ToString());
            sw.WriteLine(" .SUM OF HISTOGRAM: ");
            sw.WriteLine(sumOfhistogram.ToString());
            sw.WriteLine(" .NONZERO PARAM: ");
            sw.WriteLine(nonZero.ToString());
            sw.WriteLine(" .HISTOGRAM DATA:");
            for (int j = 0; j < numOfKeywords; j++)
            {
                sw.WriteLine(" .[" + keywordList[j] + "] -> " + histogram[j].ToString());
            }
            sw.WriteLine(" .NON-ZERO BONUS: ");
            sw.WriteLine(nonZeroBonus.ToString());
            sw.WriteLine(" .MATCH PERCENT: ");
            sw.WriteLine(matchPercent.ToString());
            sw.WriteLine(" .TOTAL TRUST: ");
            sw.WriteLine(total.ToString());
            sw.WriteLine(" * END ****************************************************************** ");
            sw.Close();
            */
            if (isRank)
            {
                StreamWriter sw = new StreamWriter("DataForRank" + System.Threading.Thread.CurrentThread.ManagedThreadId + ".txt", true);
                sw.WriteLine(" ***** DATA FOR Categorizer ************************************************* ");
                //sw.WriteLine(" .CONTENT WORDS: ");
                //sw.WriteLine(wordListCopied.ToString());
                sw.WriteLine(" .NUM OF WORDS: ");
                sw.WriteLine(numOfWords.ToString());
                String[] wordListSplited = wordListCopied.Split(separators);
                for (int k = 0; k < numOfWords;k++ )
                {
                    sw.WriteLine(" .[" + k + "] -> " + wordListSplited[k]);
                }
                sw.WriteLine(" .KEY WORDS: ");
                sw.WriteLine(keywordListCopied.ToString());
                sw.WriteLine(" .NUM OF KEY WORDS: ");
                sw.WriteLine(numOfKeywords.ToString());
                sw.WriteLine(" .THRESOLD PARAM: ");
                sw.WriteLine(threshold.ToString());
                sw.WriteLine(" .SUM OF HISTOGRAM: ");
                sw.WriteLine(sumOfhistogram.ToString());
                sw.WriteLine(" .NONZERO PARAM: ");
                sw.WriteLine(nonZero.ToString());
                sw.WriteLine(" .HISTOGRAM DATA:");
                for (int j = 0; j < numOfKeywords; j++)
                {
                    sw.WriteLine(" .[" + keywordList[j] + "] -> " + histogram[j].ToString());
                }
                sw.WriteLine(" .NON-ZERO BONUS: ");
                sw.WriteLine(nonZeroBonus.ToString());
                sw.WriteLine(" .MATCH PERCENT: ");
                sw.WriteLine(matchPercent.ToString());
                sw.WriteLine(" .TOTAL TRUST: ");
                sw.WriteLine(total.ToString());
                sw.WriteLine(" * END ****************************************************************** ");
                sw.Close();
            }
            
            return Convert.ToInt32(total);
        }

        /**
         * This method returns the canonical form of the given string.
         * Canonical form means all letters are low cased
         */
        public void canonicForm(String text)
        {
            text=text.ToLowerInvariant();
        }

        /**
         * This method returns all the synonymous words to the given word string,
         * the method returns them as a list of strings.
         */
        public List<String> synonymousList(String word)
        {
            List<String> synonymous = new List<string>();
            synonymous.Add(word);
            return synonymous;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace CrawlerNameSpace.Utilities
{
    /*
     * This Class represents a logic components which saves all
     * the data relating to specific category
     */
    public class Category
    {
        //These are two consts that are used in the getMatchLevel method.
        private const double ALPHA = 2.5;
        private const double BETA  = 0.4; 
        private String       categoryID;
        private String       parentName;
        private String       categoryName;
        private List<String> keywordList;
        private int          confidenceLevel;

        /*
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

        /*
         * This method returns the categoryID of the category
         */
        public String getCategoryID()
        {
            return categoryID;
        }

        /*
         * This method returns the parent name of the category
         */
        public String getParentName()
        {
            return parentName;
        }

        /*
         * This method returns the name of the category
         */
        public String getCatrgoryName()
        {
            return categoryName;
        }

        /*
         * This method returns the confidence level of the category
         */
        public int getConfidenceLevel()
        {
            return confidenceLevel;
        }

        /*
         * This method returns a list of the keywords of the category
         */
        public List<String> getKeywordList()
        {
            return keywordList;
        }

        /*
         * This method returns the match level of the given wordlist according to a
         * certain formula.
         */
        public int getMatchLevel(List<String> wordList) 
        {
            long n = wordList.Count;
            int  c = keywordList.Count;
            double   threshold = ((n * BETA) / c);
            double[] histogram = new double[c];
            double   sumOfhistogram=0;

            //keywordList and wordList are copied to a new arrays so that we won't change them(the originals)
            List<String> keywordListCopied = new List<string>(keywordList);
            List<String> wordListCopied    = new List<string>(wordList);

            //Transforming the keywordListCopied and wordListCopied to canonical form
            keywordListCopied.ForEach(canonicForm);
            wordListCopied.ForEach(canonicForm);

            //Initialising the histogram array to zeros
            for (int i = 0; i < histogram.Length; i++)
            {
                histogram[i] = 0;
            }

            foreach (String word in wordListCopied)
            {
                foreach (String keyword in keywordListCopied)
                {
                    if (keyword.Contains(word))
                    {
                        int index = keywordListCopied.IndexOf(keyword);
                        if (histogram[index] < threshold)
                        {
                            histogram[index]++;
                        }
                    }
                }
            }

            for (int i = 0; i < histogram.Length; i++)
            {
                sumOfhistogram += histogram[i];
            }

            return (int)((sumOfhistogram * (1.0 / n) * (ALPHA))*100);
            
        }

        /*
         * This method returns the canonical form of the given string.
         * Canonical form means all letters are low cased
         */
        public void canonicForm(String text)
        {
            text=text.ToLowerInvariant();
        }

        /*
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

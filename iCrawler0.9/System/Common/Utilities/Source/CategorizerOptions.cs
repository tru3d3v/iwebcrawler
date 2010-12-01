using System;
using System.Collections.Generic;
using System.Text;

namespace CrawlerNameSpace.Utilities
{
    /**
     * This class holds all the parameters needed for the method 
     * Category.getMatchLevel(), these parameters change the restrictness of the categorizations.
     */
    public class CategorizerOptions
    {
        public double ALPHA;
        public double BETA;
        public double GAMMA;

        public double MIN_WORDS_LIMIT;
        public double MIN_WORDS_PENLTY;

        public int NONZERO_MAX_EFFECT;
        public int MATCH_MAX_EFFECT;
        public int MAX_MATCH_LEVEL;

        public bool isRank;

        //default
        public CategorizerOptions()
        {
            ALPHA = 2950;
            BETA = 0.125;
            GAMMA = 75;

            MIN_WORDS_LIMIT = 3500;
            MIN_WORDS_PENLTY = 0.25;
            
            NONZERO_MAX_EFFECT = 50;
            MATCH_MAX_EFFECT = 100;
            MAX_MATCH_LEVEL = 100;
            isRank = false;
        }
    }
}

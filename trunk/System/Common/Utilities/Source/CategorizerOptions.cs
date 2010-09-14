using System;
using System.Collections.Generic;
using System.Text;

namespace CrawlerNameSpace.Utilities
{
    /**
     * This class holds all the parameters needed for the method 
     * Category.getMatchLevel(), these categories change the restrictness of the categorizations.
     */
    public class CategorizerOptions
    {
        public double ALPHA;
        public double BETA;
        public double GAMMA;
        public  double MIN_WORDS_LIMIT;

        public  int NONZERO_WEIGHT;
        public  int MATCH_WEIGHT ;
        public  double NORMALIZE_CONST;
        public int MAX_MATCH_LEVEL;

        public bool isRank;

        //default
        public CategorizerOptions()
        {
            ALPHA = 2500;
            BETA = 0.1;
            GAMMA = 55;
            MIN_WORDS_LIMIT = 5000;
            NONZERO_WEIGHT = 50;
            MATCH_WEIGHT = 100;
            NORMALIZE_CONST = 0.7;
            MAX_MATCH_LEVEL = 100;
            isRank = false;
        }
        public CategorizerOptions(double alpha, double beta, double gamma, double min_words_limit,
                            int nonzero_weight, int match_weight,double normalize_const,int max_match_level,bool isrank)
        {
            ALPHA=alpha;
            BETA=beta;
            GAMMA=gamma;
            MIN_WORDS_LIMIT=min_words_limit;
            NONZERO_WEIGHT=nonzero_weight;
            MATCH_WEIGHT=match_weight;
            NORMALIZE_CONST=normalize_const;
            MAX_MATCH_LEVEL = max_match_level;
            isRank = isrank;
        }
    }
}

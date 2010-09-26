using System;
using System.Collections.Generic;
using System.Text;

namespace CrawlerNameSpace.Utilities
{
    /**
     * This class holds all the parameters needed for the class Ranker.
     * These parameters change the restrictness of ranking urls.
     */
    public class RankerOptions
    {
        //Ranker Options
        public static double MinAndMaxRATIO = 0.75;
        public static int ConfidenceLevelOfAnchor = 75;
        public static double ALPHA = 0.5;
        public static double BETTA = 0.3;
        public static double GAMMA = 0.2;

        //Category Options
        public static double CAT_ALPHA = 2950;
        public static double CAT_BETA = 0.125;
        public static double CAT_GAMMA = 75;
        public static double CAT_MIN = 3500;
        public static double CAT_PENLTY = 0.25;
        // nearby options
        public static double NER_ALPHA = 450;
        public static double NER_BETA = 0.125;
        public static double NER_GAMMA = 120;
        public static double NER_MIN = 1;
        public static double NER_PENLTY = 0.75;
        // anchor options
        public static double ANC_ALPHA = 350;
        public static double ANC_BETA = 0.125;
        public static double ANC_GAMMA = 0;
        public static double ANC_MIN = 0;
        public static double ANC_PENLTY = 0.8;

        //symmetric number of chars to choose from the nearby text
        public static double SYMMETRIC_LINE = 800;  
    }
}

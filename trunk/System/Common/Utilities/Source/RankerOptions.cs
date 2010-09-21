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
        public double MinAndMaxRATIO;
        public int ConfidenceLevelOfAnchor;
        public double ALPHA;
        public double BETTA;
        public double GAMMA;

        public RankerOptions()
        {
            MinAndMaxRATIO = 0.75;
            ConfidenceLevelOfAnchor = 75;
            ALPHA = 0.5;
            BETTA = 0.3;
            GAMMA = 0.2;
        }
    }
}

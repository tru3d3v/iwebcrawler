using System;
using System.Collections.Generic;
using System.Text;

namespace CrawlerNameSpace.Utilities
{
    // This enumuration is used to specify the proporties option for the task
    public enum TaskProperty { 
        // General crawling options
        SEED, 
        RESTRICT, 
        ALLOW, 
        // threads option
        THREADS,
        // frontier
        FRONTIER,
        // categorizer options
        CAT_ALPHA, 
        CAT_BETA, 
        CAT_GAMMA, 
        CAT_MIN, 
        CAT_PENLTY,
        // nearby options
        NER_ALPHA,
        NER_BETA,
        NER_GAMMA,
        NER_MIN,
        NER_PENLTY,
        // anchor options
        ANC_ALPHA,
        ANC_BETA,
        ANC_GAMMA,
        ANC_MIN,
        ANC_PENLTY,
        // ranker options
        RAN_ALPHA,
        RAN_BETA,
        RAN_GAMMA,
        RAN_DELTA,
        RAN_NEARBY
    };
    // This enumuration is used to specify the frontier designs
    public enum FrontierDesign {
        FIFO_BFS,
        RANK_SSEv0
    };
    //This enumeration is used to specify 
    public enum operationMode_t { Auto, Manual };

    // has the selection of the supported frointer designs in the crawler
    public class supportedFrontierDesigns
    {
        static public FrontierDesign[] selection =
        {
            FrontierDesign.FIFO_BFS,
            FrontierDesign.RANK_SSEv0
        };
    }
}

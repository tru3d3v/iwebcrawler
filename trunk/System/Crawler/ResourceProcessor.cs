using System;
using System.Collections.Generic;
using System.Text;

namespace CrawlerNameSpace
{
    /**
     * This interface declares the methods that every resource processor should support
     */ 
    interface ResourceProcessor
    {
        /**
         * This method returns a boolean value which indicates if the given resource
         * can be processed by the processor or not
         */
        bool canProcess(ResourceContent resource);

        /**
         * This method tries to process the given content assuming that the given content
         * can be processed via this processor
         */ 
        void process(ResourceContent resource);
    }
}


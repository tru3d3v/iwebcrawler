using System;
using System.Collections.Generic;
using System.Text;

/**
 * This class is the container for the processors, each downloaded content
 * may be processed via more than one processor.
 */
namespace CrawlerNameSpace
{
    class ResourceProcessorManager
    {
        // This list will contain the processors which can process the downloaded content
        private Dictionary<String, ResourceProcessor> resourceProcessors;
        // This fields maintains the number of successfull and failed url requests
        int failure, success;

        /**
         * Constracts a new ResourceProcessorManager with an empty processors
         */
        public ResourceProcessorManager()
        {
            resourceProcessors = new Dictionary<String, ResourceProcessor>();
            failure = 0;
            success = 0;
        }

        /**
         * Attach new processor to the manager in order to start working on the content,
         * each processor will be assigned to identity from type string.
         */ 
        public void attachProcessor(String procId, ResourceProcessor proc)
        {
            if(procId == null || proc == null) throw new NullReferenceException();
            if (resourceProcessors.ContainsKey(procId) == true) throw new InvalidOperationException();
            resourceProcessors.Add(procId,proc);
        }

        /**
         * De attachs the processor from the manager container, if the desired processor
         * not found nothing will be done.
         */ 
        public void deAttachProcessor(String procId)
        {
            if (resourceProcessors.ContainsKey(procId) == true)
                resourceProcessors.Remove(procId);
        }

        /**
         * This method process the given content, it will log the return code for 
         * statistic anlaysis and processing the resource via the attached processors;
         * note if the recourse is illegal so the content will be ignored and there's no
         * further processment on it.
         */
        public void processResource(ResourceContent resource)
        {
            if (resource.isValid() == false)
            {
                failure++;
                return;
            }
            else
            {
                success++;
            }

            foreach (String processorId in resourceProcessors.Keys)
            {
                if (resourceProcessors[processorId].canProcess(resource) == true)
                    resourceProcessors[processorId].process(resource);
            }
        }

        /**
         * This method prints the log of the manager on the screen.
         */
        public void printLog()
        {
            System.Console.WriteLine("=| Processor Manager Status |================================================");
            System.Console.WriteLine("   Processors attached in the manager right now: " + resourceProcessors.Count);
            System.Console.WriteLine("   Number of Successfull Resources: " + success);
            System.Console.WriteLine("   Number of Failed      Resources: " + failure);
        }

        public static void Test()
        {
            ResourceProcessorManager manager = new ResourceProcessorManager();
            manager.printLog();
        }
    }
}

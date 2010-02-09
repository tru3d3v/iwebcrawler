using System;
using System.Collections.Generic;
using System.Text;

namespace CrawlerNameSpace
{
    enum ResourceType { HtmlResource };

    /**
     * This class represents the resource type that the crawler will process later on, 
     * the class contains the url of the resource the type of it and its content
     */
    class ResourceContent
    {
       private const int VALID_CODE = 200;
       /**
        * ValidCode must be decided and initialized by the one who calls the constructor, 
        * if returnCode equals to the value that this variable contains then we can 
        * say that the resource is valid.
        */
       private String       url;
       private ResourceType resourceType;
       private String       resourceContent;
       private int          returnCode;

        /**
         * Constructor:
         */
        public ResourceContent( String rsrcurl, ResourceType rsrcType, String rsrcContent, int rtrnCode) 
        {
            url = rsrcurl;
            resourceType=rsrcType;
            resourceContent = rsrcContent;
            returnCode = rtrnCode;
        }
        
        /**
         * returns the url of the resource
         */
        public String getResourceUrl() 
        { 
            return url; 
        }
        
        /**
         * returns the type of the resource
         */
        public ResourceType getResourceType() 
        { 
            return resourceType; 
        }

        /**
         * Returns the content of the resource
         */
        public String getResourceContent() 
        { 
            return resourceContent; 
        }

        /**
         * Returns the return code of the resource
         */
        public int getReturnCode() 
        { 
            return returnCode; 
        }

        /**
         * Returns true if the resource holds a valid data and false otherwise
         */
        public bool isValid() 
        {
            return (returnCode==VALID_CODE);
        }
        
        /**
         * Overides the tostring method so that the new method will return the content of the resource 
         */
        public override String ToString() 
        {
            String resourceString = "=| Resource Content Details |================================================\n";
            if (isValid())
                resourceString += "URL:              " + url + "\n" + 
                                  "Resource Type:    " + resourceType + "\n" + 
                                  "Resource Content: " + resourceContent;
            else
                resourceString += "URL:              " + url + "\n" + 
                                  "Resource Type:    " + resourceType + "\n" + 
                                  "Resource Content: " + "Content is invalid";

            return resourceString;
        }

        /**
         * This is a test method that checks if the class resourceContent works fine 
         */
        public static void Test()
        {
           String rsrcurl = "www.adamsearch.com";
           ResourceType rsrcType = ResourceType.HtmlResource;
           String rsrcContent = "Please click the next buttom to start crawling !!";
           int rtrnCode = 200;
           ResourceContent htmlResource1 = new ResourceContent(rsrcurl, rsrcType, rsrcContent, rtrnCode);
           Console.WriteLine("The resource is : " + htmlResource1.isValid());
           Console.WriteLine("Get url: " + htmlResource1.getResourceUrl());
           Console.WriteLine("Get resourceType: " + htmlResource1.getResourceType());
           Console.WriteLine("Get resourceContent: " + htmlResource1.getResourceContent());
           Console.WriteLine("to string:\n" + htmlResource1);
           
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace CrawlerNamespace
{
    enum Resource {HtmlResource};

    /**
     * This class represents the resource type that the crawler will process later on, 
     * the class contains the url of the resource the type of it and its content
     */
    class ResourceContent
    {
        //Attributes of the resource Class
       /**
        * ValidCode must be decided and initialized by the one who calls the constructor, 
        * if returnCode equals to the value that this variable contains then we can 
        * say that the resource is valid.
        */
       private int validCode;
       private String url;
       private Resource resourceType;
       private String resourceContent;
       private int returnCode;

        //Methods of the ResourceContent Class
        /**
         * Constructor:
         */ 
        public ResourceContent(int rsrcValidCode,String rsrcurl, Resource rsrcType, String rsrcContent, int rtrnCode) 
        {
            validCode =rsrcValidCode;
            url = rsrcurl;
            resourceType=rsrcType;
            resourceContent = rsrcContent;
            returnCode = rtrnCode;
        }
        
        /**
         * returns the url of the resource
         */
        public String getResourceUrl() { return url; }
        
        /**
         * returns the type of the resource
         */
        public Resource getResourceType() { return resourceType; }

        /**
         * Returns the content of the resource
         */
        public String getResourceContent() { return resourceContent; }

        /**
         * Returns the return code of the resource
         */
        public int getReturnCode() { return returnCode; }

        /**
         * Returns true if the resource holds a valid data and false otherwise
         */
        public bool isValid() 
        {
            return (returnCode==validCode);
        }
        
        /**
         * Overides the tostring method so that the new method will return the content of the resource 
         */
        public override String ToString() 
        {
            String resourceString;
            if (isValid())
                resourceString = "url of the resource: " + url + "\nresourceType: " + resourceType + "\nresourceContent: " + resourceContent;
            else
                resourceString = "url of the resource: " + url + "\nresourceType: " + resourceType + "\nresourceContent: " + "Content is invalid";

            return resourceString;
        }

        /**
         * This is a test method that checks if the class resourceContent works fine 
         */
        public static void Test()
        {
           int rsrcValidCode = 0;
           String rsrcurl = "www.adamsearch.com";
           Resource rsrcType = Resource.HtmlResource;
           String rsrcContent = "Please click the next buttom to start crawling !!";
           int rtrnCode = 0;
           ResourceContent htmlResource1 = new ResourceContent(rsrcValidCode, rsrcurl, rsrcType, rsrcContent, rtrnCode);
           Console.WriteLine("The resource is : " + htmlResource1.isValid());
           Console.WriteLine("Get url: " + htmlResource1.getResourceUrl());
           Console.WriteLine("Get resourceType: " + htmlResource1.getResourceType());
           Console.WriteLine("Get resourceContent: " + htmlResource1.getResourceContent());
           Console.WriteLine("to string:\n" + htmlResource1);
           
        }

    }
}

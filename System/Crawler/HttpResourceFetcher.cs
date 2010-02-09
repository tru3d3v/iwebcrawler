using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;


namespace CrawlerNameSpace
{
    /*
     * This class responsible to fetch pages.
     * It fetches only by http.
     */
    class HttpResourceFetcher : ResourceFetcher
    {
        
        /**
         * This method returns boolean value that indicates whether the given url can be fetched
         * or not,it will be abled to be fetched if it starts with "http".
         */
        public bool canFetch(string url)
        {
            String protocol = "http";
            return (url.StartsWith(protocol, true, System.Globalization.CultureInfo.InstalledUICulture));
        }

        /**
         * This method constructs a resourceContent object according to the page that it fetches
         * if it does not succeed in fetching a page for some reason it creates a resourceContent
         * with a null content
         */
        public ResourceContent fetch(String url, int timeOut)
        {
            ResourceContent resource;
            // used to build entire input
            StringBuilder sb = new StringBuilder();

            // used on each read operation
            byte[] buf = new byte[8192];

            // prepare the web page we will be asking for
            try
            {
                if (!(canFetch(url)))
                {
                    resource = new ResourceContent(url, ResourceType.HtmlResource, null, 400);
                    return resource;
                }

                HttpWebRequest request = (HttpWebRequest)
                    WebRequest.Create(url);

                request.Timeout = timeOut;

                // execute the request
                HttpWebResponse response = (HttpWebResponse)
                        request.GetResponse();

                // we will read data via the response stream
                Stream resStream = response.GetResponseStream();

                string tempString = null;
                int count = 0;

                do
                {
                    // fill the buffer with data
                    count = resStream.Read(buf, 0, buf.Length);

                    // make sure we read some data
                    if (count != 0)
                    {
                        // translate from bytes to ASCII text
                        tempString = Encoding.ASCII.GetString(buf, 0, count);

                        // continue building the string
                        sb.Append(tempString);
                    }
                }
                while (count > 0); // any more data to read?

                // print out page source
                resource = new ResourceContent(url,ResourceType.HtmlResource, sb.ToString(), 200);
                return resource;
            }
            catch
            {
                //200 is the success returnCode and 400 is failure returnCode
                resource = new ResourceContent(url, ResourceType.HtmlResource, null, 400);
                return resource;
            }
        }

        public static void Test()
        {
            Console.WriteLine("Testing correct url: ");
            Console.WriteLine("Fetching the url: http://www.example-code.com/csharp/spider.asp");
            ResourceContent resource = new HttpResourceFetcher().fetch("http://www.example-code.com/csharp/spider.asp", 10000);

            // Specify file, instructions, and privelegdes
            FileStream file = new FileStream("SpiderTest.html", FileMode.OpenOrCreate, FileAccess.Write);

            // Create a new stream to write to the file
            StreamWriter sw = new StreamWriter(file);

            // Write a string to the file
            sw.Write(resource.getResourceContent());

            // Close StreamWriter
            sw.Close();

            // Close file
            file.Close();

            System.Console.WriteLine("The returnCode of the request is:  " +  resource.getReturnCode());
            Console.WriteLine("Content of the fetched page saved in the file :\"SpiderTest.html\"");
            Console.WriteLine("Trying to fetch using ftp:");
            Console.WriteLine("Fetching the url:ftp://ftp.site.com/dir1/dir2/file.ext.");
            ResourceContent resource1 = new HttpResourceFetcher().fetch("ftp://ftp.site.com/dir1/dir2/file.ext.",10000);
            Console.WriteLine("The returnCode of the request is: {0} ", resource1.getReturnCode());
            Console.WriteLine("The content of the returned resource is:" + resource1.getResourceContent());
            Console.WriteLine("Trying to fetch corrupted url");
            Console.WriteLine("Fetching the url :http://www.adamshobash.com");
            ResourceContent resource2 = new HttpResourceFetcher().fetch("http://www.adamshobash.com",10000);
            Console.WriteLine("The returnCode of the request is: " + resource2.getReturnCode());
            Console.WriteLine("The content of the returned resource is:", resource2.getResourceContent());
            Console.WriteLine("Trying to fetch using small timeout: ");
            Console.WriteLine("Fetching the url: http://www.example-code.com/csharp/spider.asp with timeout =1ms");
            ResourceContent resource3 = new HttpResourceFetcher().fetch("http://www.example-code.com/csharp/spider.asp", 1);
            Console.WriteLine("The returnCode of the request is: " + resource3.getReturnCode());
            Console.WriteLine("The content of the returned resource is:", resource3.getResourceContent());
        }
    }
}

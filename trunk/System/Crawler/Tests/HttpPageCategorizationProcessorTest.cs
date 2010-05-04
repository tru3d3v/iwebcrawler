using System;
using System.Collections.Generic;
using System.Text;
using CrawlerNameSpace.Utilities;
using System.IO;

namespace CrawlerNameSpace.Tests
{
    public class HttpPageCategorizationProcessorTest
    {
        private List<Category> categories = new List<Category>();

        private Constraints constraints = new Constraints(5, true, "", ".com .co.il");

        public void Test1()
        {
            Initializer initializer = new Initializer(constraints, categories);
            
            ResourceContent resource = new ResourceContent("www.nana10.co.il", ResourceType.HtmlResource, 
                getFileContent("nana10.txt"), 200, 0);

            HtmlPageCategorizationProcessor processor = new HtmlPageCategorizationProcessor(initializer);
            processor.process(resource);
        }

        /**
         * This method reads the given file and returns it as a string 
         */
        private String getFileContent(String filename)
        {
            FileStream file = new FileStream(filename, FileMode.Open, FileAccess.Read);

            // Create a new stream to read from a file
            StreamReader sr = new StreamReader(file);

            // Read contents of file into a string
            string content = sr.ReadToEnd();

            // Close StreamReader
            sr.Close();

            // Close file
            file.Close();

            return content;
        }

        /**
         * This method is the main test that gathers all the tests.
         */
        public static void MainTest()
        {
            HttpPageCategorizationProcessorTest testProcessor = new HttpPageCategorizationProcessorTest();
            testProcessor.Test1();
        }
    }
}

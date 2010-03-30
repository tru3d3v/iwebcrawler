using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace CrawlerNameSpace.Tests
{
    public class ExtractorTest
    {
        Extractor extractor = new Extractor();
        
        public void test1()
        {
            List<LinkItem> links = extractor.extractLinks("", "dasasdsdsad <DSSDS> dssdsadsfdddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddd \n <a href=www.example.com><b>Dot Net Perls</b></a> dshhdddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbjdsf <a href=\"www.example.com\"  deef=84><b>Dot Nsadfs</b></a> uuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuu");

            List<String> compareLinks = new List<String>("www.example.com ".Split(' '));
            bool passed = true;
            
            foreach (LinkItem item in links)
            {
                if (!(compareLinks.Contains(item.getLink())))
                {
                    passed = false;
                }
                //System.Console.WriteLine(item.ToString());
            }
            if (passed)
            {
                Console.WriteLine("test1 of extractLinks(String url, String page) PASSED");
            }
            else
            {
                Console.WriteLine("test1 of extractLinks(String url, String page) FAILED");
            }
        }

        public void test2()
        {
            String contentOfKafe = getFileContent("kafe.htm");
            List<LinkItem> links = extractor.extractLinks("www.netex.mako.co.il", contentOfKafe);

            Console.WriteLine("The links exist in www.netex.mako.co.il are: ");
            foreach (LinkItem link in links)
            {
                Console.WriteLine(link.getLink());
            }
        }

        public void test3()
        {
            String contentOfNana = getFileContent("nana10.htm");
            List<LinkItem> links = extractor.extractLinks("www.netex.mako.co.il", contentOfNana);

            Console.WriteLine("The links exist in www.nana10.co.il are: ");
            int count = 0;
            foreach (LinkItem link in links)
            {
                count+=1;
                Console.WriteLine(link.getLink());
                if (count % 10 == 0)
                {
                    Console.WriteLine("The text nearby " + link.getLink() + " is: ");
                    Console.WriteLine(link.getText());
                }
            }
        }

        /**
         * This method reads the given file and returns it as a string 
         */
        private String getFileContent(String filename)
        {
            FileStream file = new FileStream(filename, FileMode.OpenOrCreate, FileAccess.Read);

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
            ExtractorTest testExtractor = new ExtractorTest();
            testExtractor.test1();
            testExtractor.test2();
            testExtractor.test3();
        }
    }
}

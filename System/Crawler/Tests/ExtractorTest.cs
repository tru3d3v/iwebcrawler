using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace CrawlerNameSpace.Tests
{
    public class ExtractorTest
    {
        Extractor extractor = new Extractor();
        
        /**
         * This method tests simple made html.
         */
        public void test1()
        {
            List<LinkItem> links = extractor.extractLinks("", "dasasdsdsad <DSSDS> dssdsadsfddddddddddddddddddddddddddddddddddddddddddddddddddddddd i bet its not that good \n <a href=www.example.com><b>Dot Net Perls</b></a> somthing very stupid ddddddddddddddddddddddddddddddddddddddddddddddddddddddddddbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbjdsf <a href=\"www.example.com\"  deef=84><b>Dot Nsadfs</b></a> uuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuu");

            List<String> compareLinks = new List<String>("www.example.com ".Split(' '));
            bool passed = true;

            Console.WriteLine("The nearby content of the extracted link is: " + links[0].getText());

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

        /**
         * This method extracts links from www.netex.mako.co.il and
         * writes them in netexlinks.txt.
         */
        public void test2()
        {
            String contentOfNetex = getFileContent(@"..\..\..\..\Test\WebPages\netex.txt");
            List<LinkItem> links = extractor.extractLinks("www.netex.mako.co.il", contentOfNetex);
            writeContentToFile(@"..\..\..\..\Test\outputs\netexlinks.txt", links);

            Console.WriteLine(@"Links from www.netex.mako.co.il have been successfuly" + 
                @" writen in ..\..\..\..\Test\outputs\netexlinks.txt");
        }

        /**
         * This method extracts links from simplehtml.txt and
         * writes them in simplehtmllinks.txt.
         */
        public void test3()
        {
            String contentOfSimple = getFileContent(@"..\..\..\..\Test\WebPages\simplehtml.txt");
            List<LinkItem> links = extractor.extractLinks("www.simplehtml.com", contentOfSimple);

            writeContentToFile(@"..\..\..\..\Test\outputs\simplehtmllinks.txt",links);
            Console.WriteLine(@"Links from www.simplehtml.com have been successfuly" +
                @" writen in ..\..\..\..\Test\outputs\simplehtmllinks.txt");
        }

        /**
         * This method extracts links from Yahoo.txt and
         * writes them in yahoolinks.txt.
         */
        public void test4()
        {
            String contentOfYahoo = getFileContent(@"..\..\..\..\Test\WebPages\Yahoo.txt");
            List<LinkItem> links = extractor.extractLinks("www.Yahoo.com", contentOfYahoo);

            writeContentToFile(@"..\..\..\..\Test\outputs\yahoolinks.txt", links);
            Console.WriteLine(@"Links from www.Yahoo.com have been successfuly" +
                @" writen in ..\..\..\..\Test\outputs\yahoolinks.txt");
        }

        /**
         * This method extracts links from nana10.txt and
         * writes them in nanalinks.txt.
         */
        public void test5()
        {
            String contentOfNana= getFileContent(@"..\..\..\..\Test\WebPages\nana10.txt");
            List<LinkItem> links = extractor.extractLinks("www.Nana10.co.il", contentOfNana);

            writeContentToFile(@"..\..\..\..\Test\outputs\nanalinks.txt", links);
            Console.WriteLine(@"Links from www.Nana10.co.il have been successfuly" +
                @" writen in ..\..\..\..\Test\outputs\nanalinks.txt");
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
         * This method creats and writes the given string to the given file name.
         */
        private void writeContentToFile(String filename, List<LinkItem> content)
        {
            // *** Write to file ***

            // Specify file, instructions, and privelegdes
            FileStream file = new FileStream(filename, FileMode.OpenOrCreate, FileAccess.Write);

            // Create a new stream to write to the file
            StreamWriter sw = new StreamWriter(file);

            // Write a string to the file
            foreach (LinkItem url in content)
            {
                sw.WriteLine(url.getLink());
            } 

            // Close StreamWriter
            sw.Close();

            // Close file
            file.Close();
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
            testExtractor.test4();
            testExtractor.test5();
        }
    }
}

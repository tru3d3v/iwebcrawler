using System;
using System.Collections.Generic;
using System.Text;

namespace CrawlerNameSpace.Tests
{
    public class ExtractorTest
    {
        public static void test1()
        {
            Extractor extractor = new Extractor();
            List<LinkItem> links = extractor.extractLinks("", "dasasdsdsad <DSSDS> dssdsadsfdddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddd \n <a href=www.example.com><b>Dot Net Perls</b></a> dshhdddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbjdsf <a href=\"www.example.com\"  deef=84><b>Dot Nsadfs</b></a> uuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuuu");

            foreach (LinkItem item in links)
            {
                System.Console.WriteLine(item.ToString());
            }
        }

        public static void MainTest()
        {
            test1();
        }
    }
}

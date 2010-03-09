using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

/**
 * This class is responsible for extracting and parsing html page, this module can
 * extract the links from the page and move them to canonized form
 */ 
namespace CrawlerNameSpace
{
    /**
     * This struct represents a link on the page
     */ 
    public struct LinkItem
    {
        public string Href;
        public string Text;

        public override string ToString()
        {
            return Href;
        }
    }

    class Extractor
    {
        public List<LinkItem> extractLinks(String url, String page)
        {
            List<LinkItem> list = new List<LinkItem>();

            // Find all matches in file.
            MatchCollection m1 = Regex.Matches(page, @"(<a.*?>.*?</a>)",
                RegexOptions.Singleline);

            // Loop over each match.
            foreach (Match m in m1)
            {
                string value = m.Groups[1].Value;
                LinkItem i = new LinkItem();

                // Get href attribute.
                Match m2 = Regex.Match(value, @"href=\""(.*?)\""",
                    RegexOptions.Singleline);
                if (m2.Success)
                {
                    i.Href = m2.Groups[1].Value;
                }

                // Remove inner tags from text.
                string t = Regex.Replace(value, @"\s*<.*?>\s*", "",
                    RegexOptions.Singleline);
                i.Text = t;

                list.Add(i);
            }
            return list;
        }

        public static void Test()
        {
            Extractor extractor = new Extractor();
            List<LinkItem> links = extractor.extractLinks("", "<a href=\"www.example.com\"><b>Dot Net Perls</b></a>");

            foreach (LinkItem item in links)
            {
                System.Console.WriteLine(item.ToString());
            }
        }
    }
}

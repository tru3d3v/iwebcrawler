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
    class Extractor
    {
        private static int SEMETRIC_LINE = 2;

        public List<LinkItem> extractLinks(String url, String page)
        {
            List<LinkItem> list = new List<LinkItem>();

            // Find all matches in file.
            MatchCollection reg = Regex.Matches(page, @"(<a.*?>.*?</a>)", 
                RegexOptions.Singleline);

            // Loop over each match.
            foreach (Match match in reg)
            {
                string tagValue = match.Groups[1].Value;
                LinkItem item = new LinkItem(url);
                item.setTag(tagValue);
                list.Add(item);
            }
            // gets the text near each link
            getText(list, page);

            return list;
        }

        private void getText(List<LinkItem> links, String page)
        {
            foreach (LinkItem item in links)
            {
                int index = page.IndexOf(item.getTag());

                if (index != -1)
                {
                    int lower = Math.Max(index - SEMETRIC_LINE, 0);
                    int higher = Math.Min(index + item.getTag().Length + SEMETRIC_LINE, page.Length - 1);

                    String subString = page.Substring(lower, higher - lower);
                    item.setText(subString);
                }
                else
                {
                    item.setText("");
                }
            }
        }
    }
}

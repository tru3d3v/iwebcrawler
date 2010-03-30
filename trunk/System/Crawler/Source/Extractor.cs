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
        private static String LINK_ATTR = "href";

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
                LinkItem item = new LinkItem();
                item.setParent(url);
                item.setTag(tagValue);
                list.Add(item);
            }
            // gets the text near each link
            getText(list, page);
            // gets the links
            getLinks(list);

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
                    item.setText(removeTags(subString));
                }
                else
                {
                    item.setText("");
                }
            }
        }

        private String removeTags(String content)
        {
            bool blockStream = false;
            String newContent = "";

            foreach (char current in content)
            {
                if (current != '<' && blockStream == false) newContent += current;

                if (current == '<') blockStream = true;
                else if (current == '>' && blockStream == true) blockStream = false;
            }

            return newContent;
        }

        private void getLinks(List<LinkItem> links)
        {
            foreach (LinkItem link in links)
            {
                String pageLink = getLink(link.getTag());
                if (isRelative(pageLink)) link.setLink(link.getParentUrl() + pageLink);
                else link.setLink(pageLink);
            }
        }

        private String getLink(String tag)
        {
            String lowerTag = tag.ToLower();
            String linkCut = "";

            int startIndex = tag.IndexOf(LINK_ATTR) + LINK_ATTR.Length;
            for (int i = startIndex; i < lowerTag.Length; i++)
            {
                if (lowerTag[i] != ' ' && lowerTag[i] != '\t' && lowerTag[i] != '\n' && lowerTag[i] != '=')
                {
                    startIndex = i;
                    break;
                }
            }

            for (int i = startIndex; i < lowerTag.Length; i++)
            {
                if (lowerTag[i] == ' ' || lowerTag[i] == '\t' || lowerTag[i] == '\n' || lowerTag[i] == '>') break;
                if (lowerTag[i] != '\"') linkCut += lowerTag[i];
            }

            return linkCut.Trim();
        }

        private bool isRelative(String link)
        {
            return false;
        }

    }
}

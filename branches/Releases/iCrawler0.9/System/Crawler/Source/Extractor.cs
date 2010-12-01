using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;

/**
 * This class is responsible for extracting and parsing html page, this module can
 * extract the links from the page and move them to canonized form
 */ 
namespace CrawlerNameSpace
{
    class Extractor
    {
        private static int SEMETRIC_LINE = 800;
        private static String LINK_ATTR = "href";

        /**
         * returns the link items from the given page
         */ 
        public List<LinkItem> extractLinks(String url, String page)
        {
            List<LinkItem> list = new List<LinkItem>();

            if (page == null)
                return list;
            /*** 1. start extractor ***/
            DateTime startTime = DateTime.Now;

            // Find all matches in file.
            MatchCollection reg = Regex.Matches(page, @"(<[aA][ \t\n].*?>.*?</[aA]>)", 
                RegexOptions.Singleline);

            // Loop over each match.
            foreach (Match match in reg)
            {
                string tagValue = match.Groups[1].Value;
                LinkItem item = new LinkItem();
                item.setParent(url);
                item.setTag(tagValue);
                if(tagValue.Contains(LINK_ATTR) == true)
                    list.Add(item);

                item.setIndex(match.Index);
            }

            /*** 2. getting all the link matches ***/
            DateTime matchTime = DateTime.Now;
            TimeSpan totalMatchTime = matchTime - startTime;

            // gets the text near each link
            getText(list, page);

            /*** 3. get the nearby text ***/
            DateTime getNearbyTime = DateTime.Now;
            TimeSpan totalNearbyTime = getNearbyTime - matchTime;

            // gets the links
            getLinks(list);

            /*** 4. get the links from the tags */
            DateTime getLinksTime = DateTime.Now;
            TimeSpan totalLinksTime = getLinksTime - getNearbyTime;

            //gets the anchor of each link
            getAnchors(list);

            /*** 5. get the anchors of the links ***/
            DateTime getAnchorsTime = DateTime.Now;
            TimeSpan totalAnchorsTime = getAnchorsTime - getLinksTime;

            return list;
        }

        /**
         * modifies the link-items to contain the nearby content foreach item, it assumes that
         * the link-items already has been assigned to tag
         */ 
        private void getText(List<LinkItem> links, String page)
        {
            foreach (LinkItem item in links)
            {
                int index = item.getIndex();//page.IndexOf(item.getTag());
                index = SEMETRIC_LINE + 1;
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

        /**
         * returns string which doesnot contain html tags
         */ 
        private String removeTags(String content)
        {
            bool blockStream = false;
            StringBuilder newContent = new StringBuilder("");

            foreach (char current in content)
            {
                if (current != '<' && blockStream == false) newContent.Append(current);

                if (current == '<')
                {
                    newContent.Append(' ');
                    blockStream = true;
                }
                else if (current == '>' && blockStream == true) blockStream = false;
            }

            return newContent.ToString();
        }

        /**
         * modifies the link-items to contain the links, it assumes that every link-item has been
         * already assigned to specific tag
         */ 
        private void getLinks(List<LinkItem> links)
        {
            foreach (LinkItem link in links)
            {
                String pageLink = getLink(link.getTag());
                if (isRelative(pageLink))
                {
                    if (pageLink.StartsWith("/"))
                    {
                        pageLink = pageLink.Substring(1);
                        string temp = "";
                        if (link.getParentUrl().ToLower().StartsWith("http://")) temp = link.getParentUrl().Substring(7);
                        string[] cuts = temp.Split('/');
                        if (cuts.Length == 1) temp = temp + '/';
                        else temp = cuts[0] + '/';
                        temp = "http://" + temp;
                        link.setLink(temp + pageLink);
                    }
                    else
                    {
                        link.setLink(link.getParentUrl() + pageLink);
                    }
                }
                else link.setLink(pageLink);
            }
        }

        /*
         * This method is a delagate method that returns true if the given stinrg is spaces or \t ot \n
         */
        private bool productnull(String product)
        {
            return product.Trim() == "";
        }
        /**
         * modifies the link-items to contain the anchors, it assumes that
         * the link-items already has been assigned to tag
         */
        private void getAnchors(List<LinkItem> links)
        {

            foreach(LinkItem item in links)
            {
                String tagsRemoved = removeTags(item.getTag());
                StringBuilder sb = new StringBuilder("");
                
                sb.Append(tagsRemoved.Trim());

                if (sb.ToString() == "")
                {
                    String[] seperators = { "</span>", "</a>", ">", "</Span>" };
                    String[] splitedTags = item.getTag().Split(seperators, StringSplitOptions.RemoveEmptyEntries);
                    List<String> splitedTagsList = new List<string>(splitedTags);
                    splitedTagsList.RemoveAll(productnull);
                    
                    if (splitedTagsList[splitedTagsList.Count - 1].TrimStart().StartsWith("<img"))
                    {
                        if (splitedTagsList[splitedTagsList.Count - 1].Contains("alt=\""))
                        {
                            String[] splitImg = splitedTagsList[splitedTagsList.Count - 1].Split(new String[] { "alt=\"" }, StringSplitOptions.RemoveEmptyEntries);
                            if (splitImg.Length >= 2)
                            {
                                string[] output = splitImg[1].Split(new char[] { '\"' }, StringSplitOptions.RemoveEmptyEntries);
                                if (output != null && output.Length > 0) sb.Append(output[0].Trim());
                            }
                        }
                    }
                }
                item.setAnchor(sb.ToString());
  
                /*
                StreamWriter sw = new
                StreamWriter("DataForExtractor" + System.Threading.Thread.CurrentThread.ManagedThreadId + ".txt", true);
                sw.WriteLine("***************TAG***************");
                sw.WriteLine(item.getTag());
                sw.WriteLine("The Splited Strings Are:");
                //foreach (String splited in splitedTagsList)
                //{
                //    sw.WriteLine(" [" + splited + "]");
                //}
                sw.WriteLine("The ANCHOR EXTRACTED IS:");
                sw.WriteLine(item.getAnchor());
                sw.WriteLine("***************ENDTAG***************");
                sw.Close();
                */
            }
        }
        /**
         * returns the link from the html-link attribute which given in the tag
         */
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
                if (lowerTag[i] != '\"' && lowerTag[i] != '\'') linkCut += tag[i];
            }

            return linkCut.Trim();
        }

        /**
         * returns whether the specified link is to be considered as relative link to the parent
         * page or not
         */ 
        private bool isRelative(String link)
        {
            String protocolFixed = link.Replace("://",":");
            string[] buffer = protocolFixed.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
            if (buffer.Length == 0) return true;

            string[] suffix = new string[] {".html", ".php", ".jsp", ".htm", ".asp", ".aspx"};

            if (link.StartsWith("/")) return true;
            if(buffer.Length == 1)
            {
                foreach(string end in suffix)
                {
                    if(buffer[0].EndsWith(end)) return true;
                }
            }

            return !Regex.IsMatch(buffer[0], @"\.[a-zA-Z]");
        }

    }
}

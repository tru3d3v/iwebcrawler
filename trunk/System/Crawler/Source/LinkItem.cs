using System;
using System.Collections.Generic;
using System.Text;

namespace CrawlerNameSpace
{
    /**
     * This class represents a link on the page, it will also contain all the information
     * needed to give this link a rank, it will contain some info about the link enviroment
     */
    public class LinkItem
    {
        // These attributes saves the link item status, it maintains the tag, link, 
        // parent-url and text.
        private String Parent, Tag, Link, Text;

        /**
         * Constructs a new LinkItem
         */ 
        public LinkItem()
        {
        }

        /**
         * sets the parent-url
         */ 
        public void setParent(String url)
        {
            String rootUrl = "";
            string[] buffer = url.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
            int depth = Math.Max(1, buffer.Length - 1);

            bool firstToken = true;
            for (int i = 0; i < depth; i++ )
            {
                if (firstToken == false) rootUrl += '/';
                rootUrl += buffer[i];
                firstToken = false;
            }

            if (rootUrl.EndsWith("/")) Parent = rootUrl;
            else Parent = rootUrl + '/';
        }

        /**
         * sets the link <a> tag
         */ 
        public void setTag(String tag)
        {
            Tag = tag;
        }

        /**
         * returns the link <a> tag
         */ 
        public String getTag()
        {
            return Tag;
        }

        /**
         * sets the link 'href' value
         */ 
        public void setLink(String link)
        {
            Link = link;
        }

        /**
         * returns the parent url
         */ 
        public String getParentUrl()
        {
            return Parent;
        }

        /**
         * returns the link 'href' value 
         */ 
        public String getLink()
        {
            return Link;
        }

        /**
         * sets the text which represent the link
         */
        public void setText(String text)
        {
            Text = text;
        }

        /**
         * returns the text which represnts the link
         */ 
        public String getText()
        {
            return Text;
        }

        /**
         * returns a string which represnets this
         */ 
        public override string ToString()
        {
            return "Url : " + Link + "\nTag : " + Tag + "\nText : \n" + Text;
        }
    }
}

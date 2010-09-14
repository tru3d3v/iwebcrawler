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
        private String Parent, Tag, Link, Text, domainUrl,Anchor;

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
            string rootUrl = "";
            string lowerUrl = url.ToLower();
            if (lowerUrl.StartsWith("http://")) url = url.Remove(0, 7);

            if (url.LastIndexOf('/') != -1) rootUrl = url.Substring(0, url.LastIndexOf('/'));
            else rootUrl = url;

            //Console.WriteLine(" [-] Index Of " + url.IndexOf('/'));
            //if (url.IndexOf('/') != -1)  Console.WriteLine(" [-] SubStr: " + url.Substring(0, url.IndexOf('/')));

            rootUrl = "http://" + rootUrl;
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

            string lowerLink = link.ToLower();
            if (lowerLink.StartsWith("http://")) link = link.Remove(0, 7);

            if (link.IndexOf('/') != -1) domainUrl = link.Substring(0, link.IndexOf('/')) + '/';
            else domainUrl = link + '/';
            domainUrl = "http://" + domainUrl;
        }

        /**
         * returns the domain of the inserted url
         */
        public String getDomainUrl()
        {
            return domainUrl;
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
         * sers the anchor of the link.
         */
        public void setAnchor(String anchor)
        {
            Anchor = anchor;
        }

        /**
         * returns the anchor of the url
         */
        public String getAnchor()
        {
            return Anchor;
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

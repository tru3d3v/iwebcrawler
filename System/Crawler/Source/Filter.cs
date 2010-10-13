using System;
using System.Collections.Generic;
using System.Text;
using CrawlerNameSpace.Utilities;
using System.Text.RegularExpressions;

namespace CrawlerNameSpace
{
    /**
     * This class is responsible for filtering the urls and canonize them to canonic
     * form;
     */ 
    class Filter
    {
        // this attribute saves the protocol prefix which should be assigned to every address
        private String prefix;
        // this attribute saves all the constrains options
        private Constraints constraints;
        // This variable contains all the urls in the page fetched,in order to check for repeating of same urls.
        private Dictionary<string, bool> urlsInPage = new Dictionary<string, bool>();

        /**
         * Constructs a new Filter class with the specified options and prefix
         */ 
        public Filter(String protocolPerfix, Constraints filterOptions)
        {
            prefix = protocolPerfix;
            constraints = filterOptions;
        }

        /**
         * This method gets a list of links and returns a filtered list which can be crawled
         * note that the returned link will be in canonic form also
         * 
         * returns a list of the filtered links which can be crawled
         */ 
        public List<String> filterLinks(List<String> links)
        {
            List<String> filtedLinks = new List<string>();
            
            foreach(String link in links)
            {
                String canonizedLink = canonize(link);

                if (urlsInPage.ContainsKey(canonizedLink))
                    continue;
                else
                {
                    urlsInPage.Add(canonizedLink, true);
                    if (canonizedLink.ToLower().StartsWith(prefix.ToLower()))
                        if (constraints.isUrlValid(canonizedLink) == true)
                            filtedLinks.Add(canonizedLink);
                }
            }
            return filtedLinks;
        }

        /**
         * returns the canonized form of the given link
         */ 
        public String canonize(String link)
        {
            String modifiedLink = (String)link.Clone();
            String[] prefixOfLink = modifiedLink.Split(":// ".Split(' '),StringSplitOptions.RemoveEmptyEntries);

            if (prefixOfLink.Length != 2)
            {
                modifiedLink = prefix + "://" + modifiedLink;
            }

            if (modifiedLink[modifiedLink.Length - 1] == '/')
                modifiedLink = modifiedLink.Remove(modifiedLink.Length - 1);

            return modifiedLink;
        }

        /*
         * This method resets the dictionary variable.
         * This method must be called whenever the page from which the urls are extracted is changed.
         */
        public void resetDictionary()
        {
            urlsInPage.Clear();
        }
    }
}

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
                if (canonizedLink.StartsWith(prefix))
                    if(constraints.isUrlValid(canonizedLink) == true && filtedLinks.Contains(canonizedLink) == false)
                        filtedLinks.Add(canonizedLink);
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

            if (prefixOfLink.Length!=2)
            {
                modifiedLink = prefix + "://" + modifiedLink;
            }

            return modifiedLink;
        }
    }
}

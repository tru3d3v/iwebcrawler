using System;
using System.Collections.Generic;
using System.Text;

namespace CrawlerNameSpace.Utilities.Tests
{
    public class TrieTest
    {
                    //Create trie
        private Trie<String> trie = new Trie<String>();

        public void Test1()
        {
            //Add some key-value pairs to the trie
            Console.WriteLine("Adding the following pair key values:");
            Console.WriteLine("James,112");
            Console.WriteLine("Jake,222");
            Console.WriteLine("Fred,326");
            Console.WriteLine("Adam,120");
            Console.WriteLine("Malik,300");
            Console.WriteLine("Noah,450");
            Console.WriteLine("adam,700");
            Console.WriteLine("mohammed,1000");

            trie.Put("James", "112");
            trie.Put("Jake", "222");
            trie.Put("Fred", "326");
            trie.Put("Adam", "120");
            trie.Put("Malik", "300");
            trie.Put("Noah", "450");
            trie.Put("adam", "700");
            trie.Put("mohammed", "1000");

            bool testPassed = true;
            //Search the trie
            trie.Matcher.NextMatch('J'); //Prefix thus far: "J"
            foreach (String value in trie.Matcher.GetPrefixMatches())
            {
                Console.WriteLine("Value of Word that starts with J is:" + value);
            }
            ; //[112, 222]

            Console.WriteLine("is exact match: " + trie.Matcher.IsExactMatch()); //false
            if (trie.Matcher.IsExactMatch())
                testPassed = false;
            if(!trie.Matcher.NextMatch('a'))
                testPassed = false;
            if (!trie.Matcher.NextMatch('m'))  //Prefix thus far: "Jam"
                testPassed = false;
            Console.WriteLine("Value of a word that has the prefix Jam: " + trie.Matcher.GetPrefixMatches()[0]);   //[112]
            trie.Matcher.NextMatch('e');
            trie.Matcher.NextMatch('s'); //Prefix thus far: "James"
            Console.WriteLine("is their an exact match for James: " + trie.Matcher.IsExactMatch()); //true
            if (!trie.Matcher.IsExactMatch())
                testPassed = false;
            Console.WriteLine("The exact match is : " + trie.Matcher.GetExactMatch()); //[112]
            if (trie.Matcher.GetExactMatch() != "112")
                testPassed = false;
            Console.WriteLine("Does \"Adam\" exists in the trie: " + trie.Matcher.NewMatch("Adam"));
            if (!trie.Matcher.NewMatch("Adam"))
                testPassed = false;
            Console.WriteLine("Does \"adam\" exists in the trie: " + trie.Matcher.NewMatch("adam"));
            if (!trie.Matcher.NewMatch("adam"))
                testPassed = false;
            Console.WriteLine("Does \"Noa\" exists in the trie: " + trie.Matcher.NewMatch("Noa"));
            if (trie.Matcher.NewMatch("Noa"))
                testPassed = false;
            Console.WriteLine("Does \"\" exists in the trie: " + trie.Matcher.NewMatch(""));
            if(trie.Matcher.NewMatch(""))
                testPassed=false;
            Console.WriteLine("Does \"adamh\" exists in the trie: " + trie.Matcher.NewMatch("adamh"));
            if(trie.Matcher.NewMatch("adamh"))
                testPassed=false;
            Console.WriteLine("Does \"mohammed\" exists in the trie: " + trie.Matcher.NewMatch("mohammed"));
            if(!trie.Matcher.NewMatch("mohammed"))
                testPassed=false;
            Console.WriteLine("Trying to remove the word \"James\" :");
            //Remove a string-value pair
            trie.Remove("James");
            Console.WriteLine("Remove Succeeded");
            if(trie.Matcher.NewMatch("James"))
                testPassed=false;

            if(testPassed)
                Console.WriteLine("Test PASSED");
            else
                Console.WriteLine("Test Failed");
        }
    }
}

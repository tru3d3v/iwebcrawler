﻿using System;
using System.Collections.Generic;
//using System.Linq;  
using System.Text;

namespace CrawlerNameSpace.Utilities  
 {
    /**
     * Search through a trie for all the strings which have a given prefix which will be entered character by character. 
     * Type of the value stored in the trie.
     */
     public class PrefixMatcher<V> : IPrefixMatcher<V> where V : class      
     {
       
       private TrieNode<V> root;                    
       private TrieNode<V> currMatch;            
       private string prefixMatched;            
   
       /**
        * Create a matcher, associating it to the trie to search in.
        * Root node of the trie which the matcher will search in.
        */
       public PrefixMatcher(TrieNode<V> root)          
       {
           this.root = root;              
           this.currMatch = root;          
       }            
       
       /**
        * Get the prefix entered so far.
        * Returns the The prefix.
        */
       public String GetPrefix()          
       {              
           return prefixMatched;          
       }            
         
       /**
        * Clear the currently entered prefix. 
        */
       public void ResetMatch()          
       {              
           currMatch = root;              
           prefixMatched = "";          
       }            
         
       /**
        * Remove the last character of the currently entered prefix. 
        */
       public void BackMatch()          
       {              
           if (currMatch != root)              
           {                  
               currMatch = currMatch.Parent;                  
               prefixMatched = prefixMatched.Substring(0, prefixMatched.Length - 1);              
           }          
       }            
         
       /**
        * Get the last character of the currently entered prefix.
        */
       public char LastMatch()          
       {              
           return currMatch.Key;          
       }            
         
       /**
        * Add another character to the end of the prefix if new prefix is actually a prefix to some strings in the trie.
        * If no strings have a matching prefix, character will not be added. 
        * The parameter "next": Next character in the prefix.
        * Returns:True if the new prefix was a prefix to some strings in the trie, false otherwise.
        */
       public bool NextMatch(char next)          
       {              
           if (currMatch.ContainsKey(next))              
           {                  
               currMatch = currMatch.GetChild(next);                  
               prefixMatched += next;                  
               return true;              
           }              
          return false;          
       }

       /**
        * Changes the prefix to a new string given to this method if it is a prefix to some strings in the trie, otherwise it 
        * resets the prefix.
        * newPrefix is a param to whom the new prefix will be changed.
        * This method returns true if the new prefix was a prefix to some strings in the trie, false otherwise.
        */
       public bool NewMatch(String newPrefix)
         {
             ResetMatch();
             char[] charsSplited = newPrefix.ToCharArray();

             foreach (char charecter in charsSplited)
             {
                 if (!(NextMatch(charecter)))
                 {
                     ResetMatch();
                     return false;
                 }
             }
             return true;
         }
                  
         /**
          * Get all the corresponding values of the keys which have a prefix corresponding to the currently entered prefix.
          * Returns: List of values.
          */
         public List<V> GetPrefixMatches()          
         {              
             return currMatch.PrefixMatches();          
         }
         
         /**
          * Check if the currently entered prefix is an existing string in the trie. 
          * Returns: True if the currently entered prefix is an existing string, false otherwise.
          */
         public bool IsExactMatch()          
         {              
             return currMatch.IsTerminater();          
         }
         
         /**
          * Get the value mapped by the currently entered prefix.
          * Returns: The value mapped by the currently entered prefix or null if current prefix does not map to any value.
          */
         public V GetExactMatch()          
         {              
             return IsExactMatch() ? currMatch.Value : null;          
         }        
     }  
 }  
using System;  
using System.Collections.Generic;  
//using System.Linq;  
using System.Text;

namespace CrawlerNameSpace.Utilities  
{    
    /**
     * Trie data structure which maps strings to generic values.
     * Type of values to map to. Must be referencable
     */
    public class Trie<V> where V:class  
    {    
        private TrieNode<V> root;    
        
        /**
         * Matcher object for matching prefixes of strings to the strings stored in this trie.
         */
        public IPrefixMatcher<V> Matcher { get; private set; }    
        
        /**
         * Create an empty trie with an empty root node.
         */
        public Trie()  
        {  
            this.root = new TrieNode<V>(null, '\0');  
            this.Matcher = new PrefixMatcher<V>(this.root);  
        }    
         
        /**
         * Put a new key value pair, overwriting the existing value if the given key is already in use.
         * Key to search for value by.
         * Value associated with key.
         */
        public void Put(string key, V value)  
        {  
            TrieNode<V> node = root;  
            foreach (char c in key)  
            {  
                node = node.AddChild(c);  
            }  
            node.Value = value;  
        }    
        
        /**
         * Remove the value that a key leads to and any redundant nodes which result from this action.  
         * Clears the current matching process.
         * Key of the value to remove.
         */
        public void Remove(string key)  
        {  
            TrieNode<V> node = root;  
            foreach (char c in key)  
            {  
                node = node.GetChild(c);  
            }  
            node.Value = null;    
            
            //Remove all ancestor nodes which don't lead to a value.  
            while (node != root && !node.IsTerminater() && node.NumChildren() == 0)  
            {  
                char prevKey = node.Key;  node = node.Parent;  node.RemoveChild(prevKey);  
            }    
            Matcher.ResetMatch();  
        }    
    }  
} 
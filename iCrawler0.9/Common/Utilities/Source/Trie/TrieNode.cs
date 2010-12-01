using System;  
using System.Collections.Generic;  
//using System.Linq;  
using System.Text;

namespace CrawlerNameSpace.Utilities  
{      
    /**
     * Node of the trie.
     * Stores a link to multiple children of type TrieNode each associated with a key of type Char.      
     * If the node terminates a string then it must also store a non-null value of type V.
     * the Paramater: Value associated with a character.
     */
    public class TrieNode <V> where V:class      
    {            
        /// <summary>          
        /// The value stored by this node. If not null then the node terminates a string.          
        /// </summary>
        /**
         * The value stored by this node. If not null then the node terminates a string.
         */
        public V Value { get; set; }            
        
        /**
         * Get the key which was associated with this node.
         */
        public Char Key { get; private set; }            
        
        /**
         * Get the parent of this node.
         */
        public TrieNode<V> Parent { get; private set; }            
        
        private Dictionary<Char, TrieNode<V>> children;            
        
        /**
         * Create an empty node with no children and null value.
         * The parameter parent: Parent node of this node.
         */
        public TrieNode(TrieNode<V> parent, Char key)          
        {              
            this.Key = key;              
            this.Value = null;              
            this.Parent = parent;              
            this.children = new Dictionary<Char, TrieNode<V>>();          
        }
            
        /**
         * Get a child of this node which is associated with a key.
         * The Paramater key: Key associated with the child of interest.
         * Returns: The child or null if no child is associated with the given key.
         */
        public TrieNode<V> GetChild(char key)          
        {              
            if (children.ContainsKey(key))              
            {                  
                return children[key];              
            }              
            return null;          
        }            
        
        /**
         * Check whether or not this node terminates a string and stores a value.
         * Returns: Whether node stores a value.
         */
        public bool IsTerminater()          
        {              
            return Value != null;          
        }            
        
        /**
         * Get the number of children this node has.
         * Returns: Number of children.
         */
        public int NumChildren()          
        {              
            return children.Count;          
        }            
        
        /**
         * Check whether or not this node has any children.
         * Returns: True if node does not have children, false otherwise.
         */
        public bool IsLeaf()          
        {              
            return NumChildren() == 0;          
        }            
        
        /**
         * Check whether or not one of the children of this node uses the given key.
         * The parameter "key": The key to check for.
         * Returns:True if a child with given key exists, false otherwise.
         */
        public bool ContainsKey(char key)          
        {              
            return children.ContainsKey(key);          
        }            
        
        /**
         * Add a child node associated with a key to this node and return the node.
         * The parameter "key": Key to associated with the child node.
         * If given key already exists then return the existing child node, else return the new child node.
         */
        public TrieNode<V> AddChild(char key)          
        {              
            if (children.ContainsKey(key))              
            {                  
                return children[key];              
            }              
            else              
            {                  
                TrieNode<V> newChild = new TrieNode<V>(this, key);                  
                children.Add(key, newChild);                  
                return newChild;              
            }          
        }            
        
        /**
         * Remove the child of a node associated with a key along with all its descendents.
         * The parameter "key": The key associated with the child to remove.
         */
        public void RemoveChild(char key)          
        {              
            children.Remove(key);          
        }            
        
        /**
         * Get a list of values contained in this node and all its descendants.
         * Returns A List of values.
         */
        public List<V> PrefixMatches()          
        {              
            if (IsLeaf())              
            {                  
                if (IsTerminater())                  
                {                      
                    return new List<V>(new V[] { Value });                  
                }                  
                else                  
                {                      
                    return new List<V>();                  
                }              
            }              
            else              
            {                  
                List<V> values = new List<V>();                  
                foreach (TrieNode<V> node in children.Values)                  
                {                      
                    values.AddRange(node.PrefixMatches());                  
                }                    
                if (IsTerminater())                  
                {                      
                    values.Add(Value);                  
                }                                    
                return values;              
            }          
        }        
    }  
} 
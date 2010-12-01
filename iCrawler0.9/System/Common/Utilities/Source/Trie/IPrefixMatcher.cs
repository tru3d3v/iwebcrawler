using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;

namespace CrawlerNameSpace.Utilities
{ 
    
    public interface IPrefixMatcher<V> where V : class 
    { 
        String GetPrefix(); 
        void ResetMatch(); 
        void BackMatch(); 
        char LastMatch(); 
        bool NextMatch(char next);
        bool NewMatch(String newPrefix);
        List<V> GetPrefixMatches(); 
        bool IsExactMatch(); 
        V GetExactMatch();      
    } 
}
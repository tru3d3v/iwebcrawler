using System;
using System.Collections.Generic;
using System.Text;

namespace CrawlerNameSpace.Utilities
{
     public class Long  
     {  
         long value = 0;  
   
         public Long(long value)  
         {  
             this.value = value;  
         }  
   
         public static implicit operator Long(long value)  
         {  
             return new Long(value);  
         }  
   
         public static implicit operator long(Long Long)  
         {  
             return Long.value;  
         }  
   
         public static long operator +(Long one, Long two)  
         {  
             return one.value + two.value;  
         }  
   
         public static Long operator +(long one, Long two)  
         {  
             return new Long(one + two);  
         }  
   
         public static long operator -(Long one, Long two)  
         {  
             return one.value - two.value;  
         }  
   
         public static Long operator -(long one, Long two)  
         {  
             return new Long(one - two);  
         }  
     }  
}

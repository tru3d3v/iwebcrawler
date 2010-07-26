using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using CrawlerNameSpace.Utilities;


namespace CrawlerNameSpace.StorageSystem
{
    /**
     * This class is responsible for mainting all the requests to the database. This class
     * serves as database proxy so it can do some caching from the database. it also handles 
     * all the connections with the database, so every other system needs information stored
     * in the database should request it via this proxy system.
     */
    class StorageSystem
    {
        /*
        public static void Main(String[] args)
        {
            /*
            StorageSystem ss = new StorageSystem();
            //List<TaskStatus> l = ss.getWorkDetails("13dba25a-3401-4766-b00a-fcea8a69cefc",QueryOption.IdleTasks);
            //System.Console.WriteLine(QueryOption.IdleTasks);
            List<String> keywords = new List<string>();
            keywords.Add("woo"); keywords.Add("moo"); keywords.Add("foo");
            Category t1 = new Category("", null, "Temp1", keywords, 10);
            Category t2 = new Category("", null, "Temp1->2", keywords, 10);
            List<Category> ll = new List<Category>();
            ll.Add(t1); ll.Add(t2);
            */
            //ss.setCategories("13dba25a-3401-4766-b00a-fcea8a69cefc",ll);
            /*
            foreach(Category cat in ll)
            {
                //String df;
                //df.Trim();
                System.Console.WriteLine(cat);
            }
            
        }*/
    }
}


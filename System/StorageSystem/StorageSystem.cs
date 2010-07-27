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
        public static void Main(String[] args)
        {
            StorageSystemTest.MainTest();
        }
    }
}


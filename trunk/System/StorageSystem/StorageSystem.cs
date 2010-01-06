using System;
using System.Collections.Generic;
using System.Text;

/**
 * This class is responsible for mainting all the requests to the database. This class
 * serves as database proxy so it can do some caching from the database. it also handles 
 * all the connections with the database, so every other system needs information stored
 * in the database should request it via this proxy system.
 */ 
class StorageSystem
{
    static void Main(string[] args)
    {
        System.Console.WriteLine("Proxy is running ... ");
        while (true) ;
    }
}


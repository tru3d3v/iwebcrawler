using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using CrawlerNameSpace.Utilities;

namespace CrawlerNameSpace.StorageSystem
{
    public class SettingsStorageTest
    {
        //This method tests the getRestrictions method
        public void Test1()
        {
            SettingsStorageImp st = new SettingsStorageImp();

            Console.WriteLine("[] Trying to get the constraints of the task : \"e4a55914-0847-4d70-be22-d2ecac77cdfa\"");
            Constraints constraints = st.getRestrictions("e4a55914-0847-4d70-be22-d2ecac77cdfa");
            Console.WriteLine("The constraints of the task are: ");
            Console.WriteLine("AllowedDepth          : " + constraints.getAllowedDepth());
            Console.WriteLine("RestrictionList       : ");
            foreach (String restrict in constraints.getRestrictionList())
            {
                Console.Write(restrict + " ");
            }
            Console.WriteLine("");
            Console.WriteLine("CrawlList             : ");
            foreach (String crawl in constraints.getCrawlList())
            {
                Console.Write(crawl + " " );
            }
            Console.WriteLine("");
            Console.WriteLine("Allow parametrezation : " + constraints.isParametrizationAllowed());
            Console.WriteLine("=============================================================");
            if (constraints==null)
                Console.WriteLine("Test Not Found !!");
            Console.WriteLine(" - PASSED -");
            Console.WriteLine("=============================================================");
        }

        //This method tests the setRestrictions method 
        public void Test2()
        {
            SettingsStorageImp st = new SettingsStorageImp();
            Console.WriteLine("[] Trying to set constraints to the task : \"b80dcfdd-d8c4-4176-aef1-6b76add16fb3\"");
            Constraints constraints = new Constraints(13, false, "org ru uk ","com co.il ac.il gov.il");

            st.setRestrictions("b80dcfdd-d8c4-4176-aef1-6b76add16fb3", constraints);
            Console.WriteLine(" - PASSED -");
            Console.WriteLine("=============================================================");
        }

        //This method tests the getSeedList method
        public void Test3()
        {
            SettingsStorageImp st = new SettingsStorageImp();
            Console.WriteLine("[] Trying to get the Seeds of the task : \"b80dcfdd-d8c4-4176-aef1-6b76add16fb3\"");

            List<String> seeds = st.getSeedList("b80dcfdd-d8c4-4176-aef1-6b76add16fb3");

            Console.WriteLine("The seeds of the task are: ");
            foreach (String seed in seeds)
            {
                Console.WriteLine(seed.Trim() );
            }
            Console.WriteLine("");
            Console.WriteLine(" - PASSED -");
 
        }

        //This method tests the setSeedList method
        public void Test4()
        {
            SettingsStorageImp st = new SettingsStorageImp();
            Console.WriteLine("[] Trying to set the following seeds to the task : e4a55914-0847-4d70-be22-d2ecac77cdfa");
            Console.WriteLine("The seeds that it trying to add are :");
            Console.WriteLine("http://www.msdn.co.il http://www.nana.co.il http://www.facebook.com");

            List<String> seeds = new List<string>();
            seeds.Add("http://www.msdn.co.il");
            seeds.Add("http://www.nana.co.il");
            seeds.Add("http://www.facebook.com");

            st.setSeedList("e4a55914-0847-4d70-be22-d2ecac77cdfa", seeds);
            Console.WriteLine(" - PASSED -");
        }

        //This method tests all the methods when the data searched does not exist in the data base
        public void Test5()
        {
            SettingsStorageImp st = new SettingsStorageImp();
            //testing the method getRestrictions 
            Console.WriteLine("[] Trying to get the constraints of a task that does not exist");
            Constraints constraints = st.getRestrictions("e4a55914-0847-4d70-be22-d2ecac7c9dfa");

            if (constraints==null)
                Console.WriteLine(" - PASSED -");

            //testing the method getSeedList
            Console.WriteLine("[] Trying to get the seeds of a task that does not exist");
            List<String> seeds = st.getSeedList("e4a55914-0847-4d70-be22-d2ecac7c9dfa");

            if (seeds.Count == 0)
                Console.WriteLine(" - PASSED -");
        }
    }
}

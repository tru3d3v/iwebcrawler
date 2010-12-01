using System;
using System.Collections.Generic;
using System.Text;

namespace CrawlerNameSpace.Utilities.Tests
{
    public class RecordTest
    {
        /**
         * This is a private record variable that holds an instance of the
         * class record,and it will be the one that this class will use for
         * testing
         */
        private Record testRecord = new Record("012", "testrecord");

        /*
         * Testing if getTaskID and getRecordName methods are working.
         */
        public void test1()
        {
            Console.WriteLine("Creating an instance of record(testRecord) with ID=012,name=testrecord");
            Console.WriteLine("Testing if correct ID and name of testrecord is returned:");
            Console.WriteLine("The ID is " + testRecord.getTaskID() + " and the name is " + testRecord.getRecordName());
            if (testRecord.getTaskID().Equals("012"))
            {
                Console.WriteLine("getTskID PASSED");
            }
            else 
            {
                Console.WriteLine("getTaskID FAILED");
            }
            if (testRecord.getRecordName().Equals("testrecord"))
            {
                Console.WriteLine("getRecordName PASSED");
            }
            else
            {
                Console.WriteLine("getRecordName FAILED");
            }
        }

        /*
         * Testing if setProperty and getProperty methods are working.
         */
        public void test2()
        {
            Console.WriteLine("Adding new property using the setProperty method");
            Console.WriteLine("Adding property:key=age,value=15");
            testRecord.setProperty("age", "15");
            Console.WriteLine("Obtaining the same property using getProperty with key=age");
            Console.WriteLine("The return value of getProperty(age) is " + testRecord.getProperty("age"));
            Console.WriteLine("Trying to obtain a property with key=usage(it should return null)");
            Console.WriteLine("The return value of getProperty(usage) is : " + testRecord.getProperty("usage"));
            if ((testRecord.getProperty("age").Equals("15")) && (testRecord.getProperty("usage")==null))
            {
                Console.WriteLine("setProperty and getProperty methods PASSED");
            }
            else
            {
                Console.WriteLine("setProperty and getProperty methods FAILED");
            }
        }

        /**
         * Testing if getKeysSet method works.
         */
        public void test3()
        {
            Console.WriteLine("Creating new record named testrecord");
            Record testRecord = new Record("012", "testrecord");
            Console.WriteLine("Adding the following 3 new properties to the record using setProperty:");
            Console.WriteLine("1.key=address,value=technion");
            testRecord.setProperty("address", "technion");
            Console.WriteLine("1.key=street,value=125");
            testRecord.setProperty("street", "125");
            Console.WriteLine("1.key=town,value=haifa");
            testRecord.setProperty("town", "haifa");
            Console.WriteLine("Using the getKeySet method to obtain all the keys in the record");
            String[] returnlist=testRecord.getKeysSet().ToArray();
            Console.WriteLine("First key is: " + returnlist[0]);
            Console.WriteLine("Second key is: " + returnlist[1]);
            Console.WriteLine("Third key is: " + returnlist[2]);
            String[] compatelist ={ "address", "street", "town" };
            if ((returnlist[0] == compatelist[0]) && (returnlist[1] == compatelist[1]) && (returnlist[2] == compatelist[2]))
            {
                Console.WriteLine("getKeysSet method PASSED");
            }
            else
            {
                Console.WriteLine("getKeysSet method FAILED");
            }
        }

        /**
         * Testing generateRecordString() and restoreRecordFromString(String recStr) methods.
         */
        public void test4()
        {
            Console.WriteLine("Creating new record named testrecord");
            Record testRecord = new Record("012", "testrecord");
            Console.WriteLine("Adding the following 3 new properties to the record using setProperty:");
            Console.WriteLine("1.key=address,value=technion");
            testRecord.setProperty("address", "technion");
            Console.WriteLine("1.key=street,value=125");
            testRecord.setProperty("street", "125");
            Console.WriteLine("1.key=town,value=haifa");
            testRecord.setProperty("town", "haifa");
            Console.WriteLine("Using the generateRecordString() method:");
            String returnStirng=testRecord.generateRecordString();
            Console.WriteLine("The retuned string is: " + returnStirng);
            Console.WriteLine("Deleting the content of testrecord and refilling it with the");
            Console.WriteLine("restoreRecordFromString(String recStr) method using the");
            Console.WriteLine("Sring: " + returnStirng);
            Console.WriteLine("The content of testRecord after calling the method restoreRecordFromString is: ");
            List<String> returnlist = testRecord.getKeysSet();
            foreach (String key in returnlist)
            {
                Console.WriteLine("Key= " + key + " Value= " + testRecord.getProperty(key));
            }
        }

        /*
         * This method is to test the clone() method.
         */
        public void test5()
        {
            Console.WriteLine("Creating new record named testrecord");
            Record testRecord = new Record("012", "testrecord");
            Console.WriteLine("Adding the following 3 new properties to the record using setProperty:");
            Console.WriteLine("1.key=address,value=technion");
            testRecord.setProperty("address", "technion");
            Console.WriteLine("1.key=street,value=125");
            testRecord.setProperty("street", "125");
            Console.WriteLine("1.key=town,value=haifa");
            testRecord.setProperty("town", "haifa");
            Console.WriteLine("Using the Clone method to copy testrecord to new instance:");
            Record copied=(Record)testRecord.Clone();
            Console.WriteLine("The content of the copied instance is :");
            List<String> returnlist = copied.getKeysSet();
            foreach (String key in returnlist)
            {
                Console.WriteLine("Key= " + key + " Value= " + testRecord.getProperty(key));
            }
        }

        /**
         * This method is the main test that gathers all the tests.
         */
        public static void MainTest()
        {
            RecordTest testingRecord = new RecordTest();
            testingRecord.test1();
            testingRecord.test2();
            testingRecord.test3();
            testingRecord.test4();
            testingRecord.test5();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace CrawlerNameSpace.Utilities.Tests
{
    public class ResultTest
    {
        public static void test1()
        {
            Result res = new Result("0000-92", "www.alexa.com", "000-52", 54, 78);
            
            if (res.getResultID() != "0000-92")
                System.Console.WriteLine("Error @ Result.getResultID: Out <{1}> ,Expected <{2}>", res.getResultID(), "0000-92");
            else System.Console.WriteLine("Success @ Result.getResultID");

            if (res.getUrl() != "www.alexa.com")
                System.Console.WriteLine("Error @ Result.getUrl: Out <{1}> ,Expected <{2}>", res.getUrl(), "www.alexa.com");
            else System.Console.WriteLine("Success @ Result.getUrl");

            if (res.getCategoryID() != "000-52")
                System.Console.WriteLine("Error @ Result.getCategoryID: Out <{1}> ,Expected <{2}>", res.getUrl(), "000-52");
            else System.Console.WriteLine("Success @ Result.getCategoryID");

            if (res.getRank() != 54)
                System.Console.WriteLine("Error @ Result.getRank: Out <{1}> ,Expected <{2}>", res.getRank(), 54);
            else System.Console.WriteLine("Success @ Result.getRank");

            if (res.getTrustMeter() != 78)
                System.Console.WriteLine("Error @ Result.getTrustMeter: Out <{1}> ,Expected <{2}>", res.getTrustMeter(), 78);
            else System.Console.WriteLine("Success @ Result.getTrustMeter");
        }

        /**
         * This method is the main test that gathers all the tests.
         */
        public static void MainTest()
        {
            ResultTest.test1();
        }
    }
}

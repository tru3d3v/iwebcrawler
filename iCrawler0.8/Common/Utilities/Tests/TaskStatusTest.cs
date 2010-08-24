using System;
using System.Collections.Generic;
using System.Text;

namespace CrawlerNameSpace.Utilities.Tests
{
    public class TaskStatusTest
    {
        public static void test1()
        {
            TaskStatus task = new TaskStatus("T-001");

            if (task.getTaskID() != "T-001")
                System.Console.WriteLine("Error @ TaskStatus.getTaskID: Out <{1}> ,Expected <{2}>", task.getTaskID(), "T-001");
            else System.Console.WriteLine("Success @ TaskStatus.getTaskID");

            task.setTaskName("T-002");
            if (task.getTaskName() != "T-002")
                System.Console.WriteLine("Error @ TaskStatus.getTaskName: Out <{1}> ,Expected <{2}>", task.getTaskName(), "T-002");
            else System.Console.WriteLine("Success @ TaskStatus.getTaskName");

            task.setTaskStatus(Status.Waiting);
            if (task.getTaskStatus() != Status.Waiting)
                System.Console.WriteLine("Error @ TaskStatus.getTaskStatus: Out <{1}> ,Expected <{2}>", task.getTaskStatus(), Status.Waiting);
            else System.Console.WriteLine("Success @ TaskStatus.getTaskStatus");

            task.setTaskElapsedTime(634);
            if (task.getTaskElapsedTime() != 634)
                System.Console.WriteLine("Error @ TaskStatus.getTaskElapsedTime: Out <{1}> ,Expected <{2}>", task.getTaskElapsedTime(), 634);
            else System.Console.WriteLine("Success @ TaskStatus.getTaskElapsedTime");
        }

        /**
         * This method is the main test that gathers all the tests.
         */
        public static void MainTest()
        {
            test1();
        }
    }
}

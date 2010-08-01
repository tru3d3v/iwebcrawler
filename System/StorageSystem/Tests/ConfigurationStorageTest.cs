using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using CrawlerNameSpace.Utilities;

namespace CrawlerNameSpace.StorageSystem
{
   public class ConfigurationStorageTest
    {
        public void Test1()
        {
            ConfigurationStorageImp ss = new ConfigurationStorageImp();
            List<TaskStatus> l = ss.getWorkDetails("a7b717e0-4fad-4293-a621-026b6f05713d", QueryOption.AllTasks);
            Console.WriteLine("[] All Tasks saved for the specified user ...");
            foreach (TaskStatus task in l)
            {
                Console.WriteLine("TaskId          : " + task.getTaskID());
                Console.WriteLine("TaskName        : " + task.getTaskName());
                Console.WriteLine("TaskStatus      : " + task.getTaskStatus());
                Console.WriteLine("TaskElapsedTime : " + task.getTaskElapsedTime());
                Console.WriteLine("=============================================================");
            }
            if (l.Count == 0) Console.WriteLine("Test Not Found !!");
            Console.WriteLine(" - PASSED -");
            Console.WriteLine("=============================================================");
            //checking active tasks
            l = ss.getWorkDetails("a7b717e0-4fad-4293-a621-026b6f05713d", QueryOption.ActiveTasks);
            Console.WriteLine("[] Active Tasks saved for the specified user ...");
            foreach (TaskStatus task in l)
            {
                Console.WriteLine("TaskId          : " + task.getTaskID());
                Console.WriteLine("TaskName        : " + task.getTaskName());
                Console.WriteLine("TaskStatus      : " + task.getTaskStatus());
                Console.WriteLine("TaskElapsedTime : " + task.getTaskElapsedTime());
                Console.WriteLine("=============================================================");
            }
            if (l.Count == 0) Console.WriteLine("Test Not Found !!");
            Console.WriteLine(" - PASSED -");
            Console.WriteLine("=============================================================");
            //checking waiting tasks
            l = ss.getWorkDetails("a7b717e0-4fad-4293-a621-026b6f05713d", QueryOption.WaitingTasks);
            Console.WriteLine("[] Waiting Tasks saved for the specified user ...");
            foreach (TaskStatus task in l)
            {
                Console.WriteLine("TaskId          : " + task.getTaskID());
                Console.WriteLine("TaskName        : " + task.getTaskName());
                Console.WriteLine("TaskStatus      : " + task.getTaskStatus());
                Console.WriteLine("TaskElapsedTime : " + task.getTaskElapsedTime());
                Console.WriteLine("=============================================================");
            }
            if (l.Count == 0) Console.WriteLine("Test Not Found !!");
            Console.WriteLine(" - PASSED -");
            Console.WriteLine("=============================================================");

            l = ss.getWorkDetails("a7b717e0-4fad-4293-a621-026b6f05713d", QueryOption.IdleTasks);
            Console.WriteLine("[] Idle Tasks saved for the specified user ...");
            foreach (TaskStatus task in l)
            {
                Console.WriteLine("TaskId          : " + task.getTaskID());
                Console.WriteLine("TaskName        : " + task.getTaskName());
                Console.WriteLine("TaskStatus      : " + task.getTaskStatus());
                Console.WriteLine("TaskElapsedTime : " + task.getTaskElapsedTime());
                Console.WriteLine("=============================================================");
            }
            if (l.Count == 0) Console.WriteLine("Test Not Found !!");
            Console.WriteLine(" - PASSED -");
        }

        public void Test2()
        {
            Console.WriteLine("[] Trying to add new task with the given user ...");
            ConfigurationStorageImp storage = new ConfigurationStorageImp();
            String taskid = storage.createWorkResources("dd9eb297-9881-4a31-a1b5-d77ccdc0aefa","Task8");
            Console.WriteLine("The new ID of the new task is : ");
            Console.WriteLine(taskid);
            Console.WriteLine(" - PASSED -");
        }

        public void Test3()
        {
            Console.WriteLine("[] Trying to change an existing task ...");
            ConfigurationStorageImp ss = new ConfigurationStorageImp();
            TaskStatus status = new TaskStatus("92667983-3cb9-4cb7-8b5b-5febb5db9341");
            status.setTaskElapsedTime(39);
            status.setTaskName("snoop");
            status.setTaskStatus(Status.Waiting);
            ss.changeWorkDetails(status);
            Console.WriteLine(" - PASSED -");
        }

        public void Test4()
        {
            Console.WriteLine("[] Trying to remove task...");
            ConfigurationStorageImp ss = new ConfigurationStorageImp();
            ss.releaseWorkResources("e4a55914-0847-4d70-be22-d2ecac77cdfa");
            Console.WriteLine(" - PASSED -");
        }

       //Testing if all the methods works when data hasn't been found.
        public void Test5()
        {
            //testing the method:getWorkDetails
            ConfigurationStorageImp ss = new ConfigurationStorageImp();
            Console.WriteLine("[] Trying to get detailts of a user that does not exist");
            List <TaskStatus> l = ss.getWorkDetails("a7b717e0-4fad-3593-a621-026b6f05713d", QueryOption.IdleTasks);
            Console.WriteLine("[] Idle Tasks saved for the specified user ...");
            foreach (TaskStatus task in l)
            {
                Console.WriteLine("TaskId          : " + task.getTaskID());
                Console.WriteLine("TaskName        : " + task.getTaskName());
                Console.WriteLine("TaskStatus      : " + task.getTaskStatus());
                Console.WriteLine("TaskElapsedTime : " + task.getTaskElapsedTime());
                Console.WriteLine("=============================================================");
            }
            if (l.Count == 0) Console.WriteLine("Test Not Found !!");
            Console.WriteLine(" - PASSED -");

            //testing the releaseWorkResources method
            Console.WriteLine("[] Trying to remove task that does not exist...");

            ss = new ConfigurationStorageImp();
            ss.releaseWorkResources("e4a55914-0827-4d70-be22-d2ecac77cdfa");
            Console.WriteLine(" - PASSED -");

        }

        public static void MainTest()
        {
            ConfigurationStorageTest testStorage = new ConfigurationStorageTest();
            testStorage.Test1();
            testStorage.Test2();
        }
    }
}

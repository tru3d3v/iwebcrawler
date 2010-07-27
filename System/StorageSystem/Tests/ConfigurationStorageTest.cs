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
            Console.WriteLine("[] Tasks saved for the specified user ...");
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
            TaskStatus status = new TaskStatus("3012e088-1519-4a78-9986-89683a7901a3");
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
            ss.releaseWorkResources("3012e088-1519-4a78-9986-89683a7901a3");
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

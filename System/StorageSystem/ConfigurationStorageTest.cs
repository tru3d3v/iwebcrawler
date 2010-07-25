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
            foreach (TaskStatus task in l)
            {
                Console.WriteLine("TaskId          : " + task.getTaskID());
                Console.WriteLine("TaskName        : " + task.getTaskName());
                Console.WriteLine("TaskStatus      : " + task.getTaskStatus());
                Console.WriteLine("TaskElapsedTime : " + task.getTaskElapsedTime());
                Console.WriteLine("=============================================================");
            }
        }

        public void Test2()
        {
            ConfigurationStorageImp storage = new ConfigurationStorageImp();
            String taskid = storage.createWorkResources("dd9eb297-9881-4a31-a1b5-d77ccdc0aefa","Task8");
            Console.WriteLine("The new ID of the new task is : ");
            Console.WriteLine(taskid);
        }

        public void Test3()
        {
            ConfigurationStorageImp ss = new ConfigurationStorageImp();
            TaskStatus status = new TaskStatus("164f1f4f-a331-495d-a59f-f6f71e670966");
            status.setTaskElapsedTime(39);
            status.setTaskName("snoop");
            status.setTaskStatus(Status.Waiting);
            ss.changeWorkDetails(status);
        }

        public void Test4()
        {
            ConfigurationStorageImp ss = new ConfigurationStorageImp();
            ss.releaseWorkResources("164f1f4f-a331-495d-a59f-f6f71e670966");
        }

        public static void MainTest()
        {
            ConfigurationStorageTest testStorage = new ConfigurationStorageTest();
            testStorage.Test1();
            testStorage.Test2();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

/**
 * This NameSpace offers some utitlity classes which will be used on all over the crawler
 * project and the other projects, This namespace contains classes which represent:
 * Task, Category, Constraints, Result, Record;
 */
namespace CrawlerNameSpace.Utilities
{
    // This enumuration is used to specify which task you want to retrieve from the saved
    // tasks, so you may specify 'idle', 'waiting', 'active', 'all'
    enum QueryOption { IdleTasks, WaitingTasks, ActiveTasks, AllTasks };

    // This enumuration represents the status of the task
    enum Status { Idle, Waiting, Active };

    /**
     * This class represents a task which have an id and name, it also contain some metadata
     * of the predefined task.
     */ 
    public class TaskStatus
    {
        // taskID represents the taskID which is a unique identifier for the task
        // taskName represents the name of the task
        private string taskID, taskName;
        // taskStatus represents the task status which can be : idle, active, waiting
        private Status taskStatus;
        // couter for the task which represent how many counts has been passed while this
        // task has been working
        private long elapsedTime;


        public void Try()
        {
            System.Console.WriteLine("Hello World");
        }
    }
}

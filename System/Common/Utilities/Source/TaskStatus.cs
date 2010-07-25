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
    public enum QueryOption { IdleTasks, WaitingTasks, ActiveTasks, AllTasks };

    // This enumuration represents the status of the task
    public enum Status { Idle, Waiting, Active };

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

        /**
         * constructs a new task with the specified identity
         */ 
        public TaskStatus(string identity)
        {
            taskID = identity;
        }

        /**
         * This method returns the identity of the represented task
         */ 
        public string getTaskID()
        {
            return taskID;
        }

        /**
         * returns the name of the represented task
         */ 
        public string getTaskName()
        {
            return taskName;
        }

        /**
         * returns the status of the represented task
         */ 
        public Status getTaskStatus()
        {
            return taskStatus;
        }

        /**
         * returns the elapsed time of the represneted task
         */ 
        public long getTaskElapsedTime()
        {
            return elapsedTime;
        }

        /**
         * sets the task name to the specified name
         */ 
        public void setTaskName(string name)
        {
            taskName = name;
        }

        /**
         * sets the task status to the specified status
         */ 
        public void setTaskStatus(Status status)
        {
            taskStatus = status;
        }

        /**
         * sets the task elapsed tiem to the specified value
         */ 
        public void setTaskElapsedTime(long time)
        {
            elapsedTime = time;
        }

        /**
         * static method that converts string, that represents a status, to  Status enum
         * In case that the given string is incorrect then the value "Idle" is returned. 
         */
        public static Status convertToStatusObj(String statusStr)
        {
            Status statusObj;
            statusStr = statusStr.ToLower(System.Globalization.CultureInfo.InvariantCulture);
            statusStr = statusStr.TrimEnd(' ');
            switch (statusStr)
            {
                case "idle":
                    statusObj = Status.Idle;
                    break;
                case "waiting":
                    statusObj = Status.Waiting;
                    break;
                case "active":
                    statusObj = Status.Active;
                    break;
                default:
                    statusObj= Status.Idle;
                    break;
            }
            return statusObj;
        }

        /**
         * static method that converts status, from enum to string.
         */
        public static String convertToStatusString(Status status)
        {
            String myStatus=null;
            switch (status)
            {
                case Status.Active :
                    myStatus = "active";
                    break;
                case Status.Idle :
                    myStatus = "idle";
                    break;
                case Status.Waiting :
                    myStatus = "waiting";
                    break;
            }
            return myStatus;
        }
    }
}

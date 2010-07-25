using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using CrawlerNameSpace.Utilities;

namespace CrawlerNameSpace.StorageSystem
{
    public class ConfigurationStorageImp : ConfigurationStorage
    {
        /*
       * This function gets UserID and QueryOption and returns the TaskID,TaskName,
       * Status,ElapsedTime of all the tasks that have the given UserID and that they are
       * in the state of the given QueryOpiton.
       */
        public List<TaskStatus> getWorkDetails(String userID, QueryOption option)
        {
            // 1. Instantiate the connection
            SqlConnection conn = new SqlConnection(SettingsReader.getConnectionString());

            SqlDataReader rdr = null;

            List<TaskStatus> taskDetailsList = new List<TaskStatus>();

            try
            {
                // 2. Open the connection
                conn.Open();

                SqlCommand cmd;

                // 3. Pass the connection to a command object

                String statusString = "";

                switch (option)
                {
                    case QueryOption.ActiveTasks:
                        statusString = "Active";
                        break;
                    case QueryOption.IdleTasks:
                        statusString = "Idle";
                        break;
                    case QueryOption.WaitingTasks:
                        statusString = "Waiting";
                        break;
                    default:
                        statusString = "";
                        break;
                }

                if (option == QueryOption.AllTasks)
                {
                    cmd = new SqlCommand("SELECT TaskID,TaskName,Status,ElapsedTime from Task WHERE UserID=\'" + userID + "\'", conn);
                }
                else
                {
                    cmd = new SqlCommand("SELECT TaskID,TaskName,Status,ElapsedTime from Task" +
                            " WHERE UserID=\'" + userID + "\' AND Status=\'" + statusString + "\'", conn);
                }

                // get query results
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    TaskStatus taskDetails = new TaskStatus(rdr["TaskID"].ToString());
                    taskDetails.setTaskElapsedTime((long)rdr["ElapsedTime"]);
                    taskDetails.setTaskName((String)rdr["TaskName"]);
                    Status statusOfTask = TaskStatus.convertToStatusObj((String)rdr["Status"]);
                    taskDetails.setTaskStatus(statusOfTask);
                    taskDetailsList.Add(taskDetails);
                }
            }
            finally
            {
                // close the reader
                if (rdr != null)
                {
                    rdr.Close();
                }

                // 5. Close the connection
                if (conn != null)
                {
                    conn.Close();
                }
            }
            return taskDetailsList;
        }

        public String createWorkResources(String userID, String taskName)
        {
            SqlConnection conn = new SqlConnection(SettingsReader.getConnectionString());
            String taskid = null;
            SqlDataReader rdr = null;
            try
            {
                conn.Open();
                //insert new row to Task 

                String cmdtxt = "SELECT TaskName FROM Task WHERE UserID = \'" + userID + "\'";

                SqlCommand cmd = new SqlCommand(cmdtxt, conn);

                //Execute command
                rdr = cmd.ExecuteReader();

                //check if the inserted userid has a task named taskName
                while (rdr.Read())
                {
                    Console.WriteLine(rdr["TaskName"]);
                    string nameExtacted = (string)rdr["TaskName"];
                    nameExtacted = nameExtacted.TrimEnd(' ');
                    if (nameExtacted == taskName)
                    {
                        throw new Exception("TaskName for the user allready exists");
                    }
                }

                if (rdr != null)
                {
                    rdr.Close();
                }

                //if the taskName does not exist in the table for the inserted userid 
                //then insert it into the table.
                cmdtxt = "INSERT INTO Task (UserID,TaskName) VALUES (\'" + userID + "\',\'" + taskName + "\')";

                cmd.CommandText = cmdtxt;

                cmd.ExecuteNonQuery();

                //return the taskID of the new row created 
                cmdtxt = "SELECT TaskID FROM Task WHERE UserID=\'" + userID + "\' AND TaskName=\'" + taskName + "\'";

                cmd.CommandText = cmdtxt;

                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                    taskid = rdr["TaskID"].ToString();

            }
            finally
            {
                if (rdr != null)
                {
                    rdr.Close();
                }

                if (conn != null)
                {
                    conn.Close();
                }
            }
            return taskid;
        }

        public void releaseWorkResources(String userID)
        {

        }

        public void changeWorkDetails(TaskStatus status)
        {

        }
    }
}

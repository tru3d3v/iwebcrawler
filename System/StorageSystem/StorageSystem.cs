using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using CrawlerNameSpace.Utilities;


namespace CrawlerNameSpace.StorageSystem
{
    /**
     * This class is responsible for mainting all the requests to the database. This class
     * serves as database proxy so it can do some caching from the database. it also handles 
     * all the connections with the database, so every other system needs information stored
     * in the database should request it via this proxy system.
     */
    class StorageSystem : ConfigurationStorage
    {

        public List<TaskStatus> getWorkDetails(String userID, QueryOption option)
        {
            // 1. Instantiate the connection
            SqlConnection conn = new SqlConnection(
                @"Data Source=.\SQLEXPRESS;AttachDbFilename=C:\Users\netproject\Documents\CrawlerDB.mdf;" + 
                       "Integrated Security=True;Connect Timeout=30;User Instance=True");

            SqlDataReader rdr = null;

            List<TaskStatus> taskDetailsList = new List<TaskStatus>();

            try
            {
                // 2. Open the connection
                conn.Open();

                SqlCommand cmd;

                // 3. Pass the connection to a command object
                if (option == QueryOption.AllTasks)
                {
                    cmd = new SqlCommand("SELECT TaskID,TaskName,Status,ElapsedTime from Task WHERE UserID=" + userID, conn);
                }
                else
                {
                    cmd = new SqlCommand("SELECT TaskID,TaskName,Status,ElapsedTime from Task" + 
                            " WHERE UserID=\'" + userID + "\' AND Status=\'Idle\'" , conn);
                }

                // get query results
                rdr = cmd.ExecuteReader();
                
                while (rdr.Read())
                {
                    TaskStatus taskDetails = new TaskStatus(rdr["TaskID"].ToString());
                    taskDetails.setTaskElapsedTime((long)rdr["ElapsedTime"]);
                    taskDetails.setTaskName((String)rdr["TaskName"]);
                    taskDetails.setTaskStatus((Status)rdr["Status"]);
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

        public String createWorkResources(String userID)
        {
            return null;
        }

        public void releaseWorkResources(String userID)
        {
 
        }

        public void changeWorkDetails(TaskStatus status)
        {
 
        }

        public static void Main(String[] args)
        {
            StorageSystem ss = new StorageSystem();
            List<TaskStatus> l = ss.getWorkDetails("13dba25a-3401-4766-b00a-fcea8a69cefc",QueryOption.IdleTasks);
            System.Console.WriteLine(QueryOption.IdleTasks);
        }
    }
}


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

                String statusString="";

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

        public String createWorkResources(String userID,String taskName)
        {
            SqlConnection conn = new SqlConnection(SettingsReader.getConnectionString());
            String taskid=null;
            SqlDataReader rdr = null;
            try
            {
                conn.Open();
                //insert new row to Task 

                String cmdtxt = "SELECT TaskName FROM Task WHERE UserID = \'" + userID +"\'";

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
                   taskid= rdr["TaskID"].ToString();

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

        // Start of Catergories Section

        /**
         * returns the category list of the specified task
         */
        public List<Category> getCategories(String taskId)
        {
            List<Category> categoryList = new List<Category>();
            SqlConnection conn = null;
            SqlDataReader rdr  = null;

            try
            {
                conn = new SqlConnection(SettingsReader.getConnectionString());
                
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT CategoryID,CategoryName,Keywords,ParentCategory,ConfidenceLevel" +
                    " FROM Category WHERE TaskID=\'" + taskId + "\'", conn);

                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    int confidenceLevel = Convert.ToInt32(rdr["ConfidenceLevel"]);
                    List<String> keywords = new List<string>(((String)rdr["Keywords"]).Split(';'));
                    Category category = new Category(rdr["CategoryID"].ToString(), rdr["ParentCategory"].ToString(), 
                        rdr["CategoryName"].ToString().Trim(), keywords, confidenceLevel);

                    categoryList.Add(category);
                }
            }
            catch (Exception e)
            {
                System.Console.WriteLine("Exception Caught: " + e.Message);
            }
            finally
            {
                if (rdr != null) rdr.Close();
                if (conn != null) conn.Close();
            }
            return categoryList;
        }

        /**
         * resets the predifined categories in the specified task
         * it will return the number of rows which has been removed due to this reset
         */
        public int resetCategories(String taskId)
        {
            SqlConnection conn = null;
            int rowsRemoved = 0;

            try
            {
                conn = new SqlConnection(SettingsReader.getConnectionString());

                conn.Open();
                SqlCommand cmd = new SqlCommand("DELETE FROM Category WHERE TaskID=\'" + taskId + "\'", conn);

                rowsRemoved = cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                System.Console.WriteLine("Exception Caught: " + e.Message);
            }
            finally
            {
                if (conn != null) conn.Close();
            }
            return rowsRemoved;
        }

        /**
         * sets the categories for the specified task
         */
        public void setCategories(String taskId, List<Category> categoryList)
        {
            SqlConnection conn = null;

            try
            {
                conn = new SqlConnection(SettingsReader.getConnectionString());

                conn.Open();
                foreach(Category category in categoryList)
                {
                    String keywords = "";
                    bool flag = true;
                    foreach(string token in category.getKeywordList())
                    {
                        if (flag == true)
                        {
                            keywords = token;
                            flag = false;
                        }
                        else keywords = keywords + ";" + token;
                    }

                    SqlCommand cmd = null;
                    if(category.getParentName() != null)
                    {
                        cmd = new SqlCommand("INSERT INTO Category (TaskID,CategoryName,Keywords,ParentCategory,ConfidenceLevel)"
                            + " Values (\'" + taskId + "\',\'" + category.getCatrgoryName() + "\',\'" + keywords + "\',\'"  
                            + category.getParentName() + "\'," + category.getConfidenceLevel().ToString() + ")", conn);
                    }
                    else
                    {
                        cmd = new SqlCommand("INSERT INTO Category (TaskID,CategoryName,Keywords,ConfidenceLevel)"
                            + " Values (\'" + taskId + "\',\'" + category.getCatrgoryName() + "\',\'" + keywords + "\',"  
                            + category.getConfidenceLevel().ToString() + ")", conn);
                    }
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                System.Console.WriteLine("Exception Caught: " + e.Message);
            }
            finally
            {
                if (conn != null) conn.Close();
            }
        }
        /*
        public static void Main(String[] args)
        {
            StorageSystem ss = new StorageSystem();
            //List<TaskStatus> l = ss.getWorkDetails("13dba25a-3401-4766-b00a-fcea8a69cefc",QueryOption.IdleTasks);
            //System.Console.WriteLine(QueryOption.IdleTasks);
            List<String> keywords = new List<string>();
            keywords.Add("woo"); keywords.Add("moo"); keywords.Add("foo");
            Category t1 = new Category("", null, "Temp1", keywords, 10);
            Category t2 = new Category("", null, "Temp1->2", keywords, 10);
            List<Category> ll = new List<Category>();
            ll.Add(t1); ll.Add(t2);

            ss.setCategories("13dba25a-3401-4766-b00a-fcea8a69cefc",ll);
            /*
            foreach(Category cat in ll)
            {
                //String df;
                //df.Trim();
                System.Console.WriteLine(cat);
            }
            */
       
    }
}


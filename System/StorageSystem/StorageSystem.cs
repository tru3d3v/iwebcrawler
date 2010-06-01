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
}


using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using CrawlerNameSpace.Utilities;

namespace CrawlerNameSpace.StorageSystem
{
    class SettingsStorageImp : SettingsStorage
    {
        /**
         * returns the saved constrains for the specified task
         */
        public Constraints getRestrictions(String taskId)
        {
            Constraints constrains;
            SqlConnection conn = null;
            SqlDataReader rdr = null;

            try
            {
                string restrict = "", crawl = "";
                int linkDepth = 1;
                bool parametersAllowed = false;
                conn = new SqlConnection(SettingsReader.getConnectionString());

                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT LinkDepth,AllowUrlParam" +
                    " FROM Task WHERE TaskID=\'" + taskId + "\'", conn);

                rdr = cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    if (rdr.Read())
                    {
                        linkDepth = Convert.ToInt32(rdr["LinkDepth"]);
                        int allowParameters = Convert.ToInt32(rdr["AllowUrlParam"]);
                        parametersAllowed = (allowParameters != 0);
                    }
                    else throw new Exception();
                }

                cmd = new SqlCommand("SELECT Value" +
                    " FROM TaskProperties WHERE TaskID=\'" + taskId + "\' AND Property=\'RESTRICT\'", conn);

                if (rdr != null) rdr.Close();
                rdr = cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        restrict = restrict + rdr["Value"] + ' ';
                    }
                    if (restrict.Length != 0) restrict = restrict.TrimEnd(new char[] { ' ' });
                }

                cmd = new SqlCommand("SELECT Value" +
                    " FROM TaskProperties WHERE TaskID=\'" + taskId + "\' AND Property=\'ALLOW\'", conn);
                if (rdr != null) rdr.Close();
                rdr = cmd.ExecuteReader();
                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        crawl = crawl + rdr["Value"] + ' ';
                    }
                    if (crawl.Length != 0) crawl = crawl.TrimEnd(new char[] { ' ' });
                }

                constrains = new Constraints((uint)linkDepth, parametersAllowed, restrict, crawl);
            }
            catch (Exception e)
            {
                System.Console.WriteLine("Exception Caught: " + e.Message);
                constrains = null;
            }
            finally
            {
                if (rdr != null) rdr.Close();
                if (conn != null) conn.Close();
            }
            return constrains;
        }

        /**
         * removes the predifined property fields in the specified task
         * it will return the number of rows which has been removed due to this remove
         */
        public int removeProperty(String taskId, String property)
        {
            SqlConnection conn = null;
            int rowsRemoved = 0;

            try
            {
                conn = new SqlConnection(SettingsReader.getConnectionString());

                conn.Open();
                SqlCommand cmd = new SqlCommand("DELETE FROM TaskProperties WHERE TaskID=\'" +
                    taskId + "\' AND Property=\'" + property + "\'", conn);

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
         * sets a new restrictions for the specified task
         */ 
        public void setRestrictions(String taskId, Constraints constrains)
        {
            SqlConnection conn = null;
            SqlDataReader rdr = null;

            try
            {
                conn = new SqlConnection(SettingsReader.getConnectionString());

                conn.Open();
                SqlCommand cmd = new SqlCommand("UPDATE Task" +
                    " SET LinkDepth=\'" + constrains.getAllowedDepth() + "\', AllowUrlParam=\'" + 
                    constrains.isParametrizationAllowed() + "\' WHERE TaskID=\'" + taskId + "\'", conn);
                cmd.ExecuteNonQuery();

                removeProperty(taskId, "ALLOW");
                foreach(String crawl in constrains.getCrawlList())
                {
                    cmd = new SqlCommand("INSERT INTO TaskProperties (TaskID,Property,Value)"
                            + " Values (\'" + taskId + "\',\'" + "ALLOW" + "\',\'" + crawl + "\')", conn);

                    cmd.ExecuteNonQuery();
                }

                removeProperty(taskId, "RESTRICT");
                foreach (String crawl in constrains.getRestrictionList())
                {
                    cmd = new SqlCommand("INSERT INTO TaskProperties (TaskID,Property,Value)"
                            + " Values (\'" + taskId + "\',\'" + "RESTRICT" + "\',\'" + crawl + "\')", conn);

                    cmd.ExecuteNonQuery();
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
        }

        /**
         * returns the seeds list for the specified task
         */
        public List<String> getSeedList(String taskId)
        {
            List<String> seedsList = new List<string>();
            SqlConnection conn = null;
            SqlDataReader rdr = null;

            try
            {
                conn = new SqlConnection(SettingsReader.getConnectionString());

                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT Value" +
                    " FROM TaskProperties WHERE TaskID=\'" + taskId + "\' AND Property=\'SEED\'", conn);
                rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    seedsList.Add(rdr["Value"].ToString());
                }
            }
            catch (Exception e)
            {
                System.Console.WriteLine("Exception Caught: " + e.Message);
                seedsList = null;
            }
            finally
            {
                if (rdr != null) rdr.Close();
                if (conn != null) conn.Close();
            }
            return seedsList;
        }

        /**
         * sets the seeds for the specified task
         */
        public void setSeedList(String taskId, List<String> seeds)
        {
            SqlConnection conn = null;

            try
            {
                removeProperty(taskId, "SEED");
                conn = new SqlConnection(SettingsReader.getConnectionString());

                conn.Open();
                foreach (String seed in seeds)
                {
                    SqlCommand cmd = new SqlCommand("INSERT INTO TaskProperties (TaskID,Property,Value)"
                            + " Values (\'" + taskId + "\',\'" + "SEED" + "\',\'" + seed + "\')", conn);

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

    }
}

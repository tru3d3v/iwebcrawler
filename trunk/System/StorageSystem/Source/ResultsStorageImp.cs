using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using CrawlerNameSpace.Utilities;

namespace CrawlerNameSpace.StorageSystem
{
    public class ResultsStorageImp : ResultsStorage
    {
        //this is a private method that compares between two result object according to their rank.
        //this method is used as delegate function in the sort of list.
        private static int CompareResultByRank(Result res1, Result res2)
        {
            if (res1 == null)
            {
                if (res2 == null)
                    return 0;
                else
                    return -1;
            }
            else
                if (res2 == null)
                    return 1;
                else
                {
                    if (res1.getRank() < res2.getRank())
                        return -1;
                    else
                        if (res1.getRank() == res2.getRank())
                            return 0;
                        else
                            return 1;
                }
        }

        //this is a private method that compares between two result object according to their trust meter.
        //this method is used as delegate function in the sort of list.
        private static int CompareResultByTrustMeter(Result res1, Result res2)
        {
            if (res1 == null)
            {
                if (res2 == null)
                    return 0;
                else
                    return -1;
            }
            else
                if (res2 == null)
                    return 1;
                else
                {
                    if (res1.getTrustMeter() < res2.getTrustMeter())
                        return -1;
                    else
                        if (res1.getTrustMeter() == res2.getTrustMeter())
                            return 0;
                        else
                            return 1;
                }
        }

        /**
         * This function gets results of a task,  the results will be represented as list of Result object:
         * categoryId, rank, trustMeter.
         */
        public List<Result> getURLResults(String taskId, String url)
        {
            // 1. Instantiate the connection
            SqlConnection conn = new SqlConnection(SettingsReader.getConnectionString());
            SqlDataReader rdr = null;
            List<Result> resultsCrawled = new List<Result>();
            
            try
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("SELECT ResultID,Url,CategoryID,rank,TrustMeter" + 
                                " FROM Results " + 
                                "WHERE TaskID=\'" + taskId + "\' AND Url = \'" + url + "\'", conn);
                rdr = cmd.ExecuteReader();

                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        int rank = Convert.ToInt32(rdr["rank"].ToString().Trim());
                        int trustMeter = Convert.ToInt32(rdr["TrustMeter"].ToString().Trim());

                        Result resultItem = new Result(rdr["ResultID"].ToString(), rdr["Url"].ToString().Trim(),
                                            rdr["CategoryID"].ToString(), rank, trustMeter);

                        resultsCrawled.Add(resultItem);

                    }
                }
                else
                {
                    resultsCrawled = null;
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
            
            return resultsCrawled;
        }

        /**
         * This function returns the number of the urls which already has been crawled that belongs to the 
         * given taskId.
         */
        public ulong getTotalURLs(String taskId)
        {
            SqlConnection conn = new SqlConnection(SettingsReader.getConnectionString());

            SqlDataReader rdr = null;

            ulong totalUrls = 0;

            try
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("SELECT COUNT(TaskID) AS TotalUrls FROM Results " + 
                                        "WHERE TaskID = \'" + taskId +"\'", conn);
                rdr = cmd.ExecuteReader();

                rdr.Read();
                totalUrls = Convert.ToUInt32(rdr["TotalUrls"].ToString().Trim());
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

            return totalUrls;
        }

         /**
          * This function returns a list of urls which suits a specific category, every url will be described 
          * as object which contains data about: url, categoryId, rank, trustMeter.
          */
         public List<Result> getURLsFromCategory(String taskId, String categoryId, Order order, int from, int to)
         {
             SqlConnection conn = new SqlConnection(SettingsReader.getConnectionString());

             SqlDataReader rdr = null;

             List<Result> resultUrls = new List<Result>();
             
             //here we save all the id's of the categories that reside in the tree of categories.
             //we add categories as we pass over a certain level of the tree.
             //so it will look like kind of a queue.
             List<String> currentCategories = new List<string>();

             currentCategories.Add(categoryId);

             try
             {
                 conn.Open();

                     //go through all the categories at the current level of the tree and extract all the 
                     //urls that belong to that categories.

                     while (currentCategories.Count != 0)
                     {
                         SqlCommand cmd = new SqlCommand("SELECT ResultID,Url,rank,TrustMeter From Results WHERE " +
                                            "TaskID = \'" + taskId + "\' AND CategoryID = \'" + 
                                            currentCategories[0] + "\'", conn);

                         rdr = cmd.ExecuteReader();

                         while (rdr.Read())
                         {
                             int rank = Convert.ToInt32(rdr["rank"].ToString().Trim());
                             int trustMeter = Convert.ToInt32(rdr["TrustMeter"].ToString().Trim());
                             Result resultItem = new Result(rdr["ResultID"].ToString().Trim(), rdr["Url"].ToString().Trim(),
                                                  currentCategories[0], rank, trustMeter);

                             resultUrls.Add(resultItem);

                         }
                         if (rdr != null) rdr.Close();

                         SqlCommand cmnd = new SqlCommand("SELECT CategoryID From Category WHERE ParentCategory = \'" +
                                            currentCategories[0] + "\'",conn);
                         

                         rdr = cmnd.ExecuteReader();

                         //add all the selected categories(sons of the current category being searched) to currentCategories.
                         while (rdr.Read())
                         {
                             currentCategories.Add(rdr["CategoryID"].ToString().Trim());
                         }
                         currentCategories.RemoveAt(0);
                         if (rdr != null) rdr.Close();
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

             return sortListOfResults(resultUrls,order,from,to);
         }

         /**
          * This function replaces the URL result to the new one(and it's fathers).
          * NOTE:the new data stored in the data base is the data of the given newResult
          * so make sure that all the needed data there is valid.
          */
         public void replaceURLResult(String taskId,Result oldResult, Result newResult)
         { 
             SqlConnection conn = new SqlConnection(SettingsReader.getConnectionString());

             try
             {
                 conn.Open();
                 SqlCommand cmd = new SqlCommand("DELETE FROM Results WHERE Url = \'" + oldResult.getUrl() + 
                                   "\' AND TaskID = \'" + taskId + "\'" , conn);

                 cmd.ExecuteNonQuery();

                 addURLResult(taskId, newResult);
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

         /**
          * This function removes one entry of the URL result, which specified in the given argument.
          * NOTE:this function removes one entry according to the resultID of the given result.
          */
         public void removeURLResult(Result result)
         {
             SqlConnection conn = new SqlConnection(SettingsReader.getConnectionString());

             try
             {
                 conn.Open();

                 SqlCommand cmd = new SqlCommand("DELETE FROM Results WHERE ResultID = \'" + result.getResultID() + "\'", conn);

                 cmd.ExecuteNonQuery();

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

         /**
          * This function adds the URL result to the given categories (and it's fathers).
          */
         public void addURLResult(String taskId, Result result)
         {
             SqlConnection conn = new SqlConnection(SettingsReader.getConnectionString());

             SqlDataReader rdr = null;

             String categoryID = result.getCategoryID();

             try
             {
                 conn.Open();

                 //this is the ID of the main category(webpage).
                 String webpageID = getMainCategoryID();

                 while (categoryID != webpageID)
                 {
                     SqlCommand cmd = new SqlCommand("INSERT INTO Results (TaskID,Url,CategoryID,rank,TrustMeter) " +
                                        "Values(\'" + taskId + "\',\'" + result.getUrl() + "\',\'" +
                                        categoryID + "\',\'" + result.getRank() + "\',\'" +
                                        result.getTrustMeter() + "\')", conn);

                     cmd.ExecuteNonQuery();

                     cmd = new SqlCommand("SELECT ParentCategory From Category WHERE CategoryID = \'" + categoryID + "\'");

                     rdr = cmd.ExecuteReader();

                     categoryID = rdr["ParentCategory"].ToString();
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
         * This method is an assistant method that finds the ID of the category "WebPage".
         */
         public String getMainCategoryID()
         {
             SqlConnection conn = new SqlConnection(SettingsReader.getConnectionString());
                
             String catID = null;

             SqlDataReader rdr = null;

             try
             {
                 conn.Open();

                 SqlCommand cmd = new SqlCommand("SELECT CategoryID FROM Category WHERE CategoryName = 'WebPage'", conn);

                 rdr = cmd.ExecuteReader();

                 rdr.Read();

                 catID = rdr["CategoryID"].ToString();
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

             return catID;
         }

        /**
         * This private method receives list of Results and sorts them according to the given  Order order,
         * int from, int to .
         */
         private List<Result> sortListOfResults(List<Result> resultsList,Order order,int from,int to)
         {
             List<Result> sortedResults = new List<Result>(resultsList);
             
             switch (order)
             {
                 case Order.AscendingTrust :
                     sortedResults.Sort(CompareResultByTrustMeter);
                     break;
                 case Order.DescendingTrust :
                     sortedResults.Sort(CompareResultByTrustMeter);
                     sortedResults.Reverse();
                     break;
                 case Order.AscendingRank :
                     sortedResults.Sort(CompareResultByRank);
                     break;
                 case Order.DescendingRank :
                     sortedResults.Sort(CompareResultByRank);
                     sortedResults.Reverse();
                     break;
                 default:
                     break;
             }
             if (to>sortedResults.Count)
                if(from<0)
                    return sortedResults.GetRange(0, (sortedResults.Count-from));
                else
                    return sortedResults.GetRange(from, (sortedResults.Count - from));
             else
                 if (from < 0)
                     return sortedResults.GetRange(0, (to-from));
                 else
                     return sortedResults.GetRange(from, (to - from));

         }
    }
}

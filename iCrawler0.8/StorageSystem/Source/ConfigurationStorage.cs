using System;
using System.Collections.Generic;
using System.Text;
using CrawlerNameSpace.Utilities;

namespace CrawlerNameSpace.StorageSystem
{
    /**
     * This interface defines all the methods that are related to configuring the 
     * crawler, and that the Storage System should impement.
     */
    interface ConfigurationStorage
    {
        /**
         * This method will be called in order to check some properties of the allocated tasks, 
         * so one can insert a query asking for all existing tasks status or the active one etc
         */
        List<TaskStatus> getWorkDetails(String userID, QueryOption option);

        /**
         * This method is responsible for creating all the resources for new task and 
         * initialize them with default values.
         */
        String createWorkResources(String userID,String taskName);

        /**
         * This function is responsible for releasing the task
         * resources from the storage system, and deleting all the resources.
         */
        void releaseWorkResources(String taskID);

        /**
         * This method is responsible for changing the status of a given task, 
         * It may ask for pause or activate the given task, change the name of the 
         * task or increment the elapsed time counter.
         */
        void changeWorkDetails(TaskStatus status);
    }
}

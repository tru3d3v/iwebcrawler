using System;
using System.Collections.Generic;
using System.Text;
using CrawlerNameSpace.Utilities;

namespace CrawlerNameSpace.StorageSystem
{
    /**
     * This interface contains the methods which determine the configured settings
     * for the crawler behavior
     */ 
    interface SettingsStorage
    {
        /**
         * This function is responsible for getting all the restrictions which was 
         * attached to the given task.
         */
        Constraints getRestrictions(String taskId);

        /**
         * This function is used in order to save all the restrictions created by the
         * user in the storage system. These restrictions will be activated in the next
         * time the specified task resumes it's work.
         */
        void setRestrictions(String taskId, Constraints constrains);

        /**
         * This function gets the seeds list from the storage for the specified task. 
         * This list can be used for start the crawling for the first time or for 
         * re-crawl progress.
         */
        List<String> getSeedList(String taskId);

        /**
         * This function sets the seeds list for a given task. It recreates the seeds 
         * list (not appending).
         */
        void setSeedList(String taskId, List<String> seeds);

        /**
         * sets the specified property in the database with the new specified value
         */
        void setProperty(String taskId, String property, String value);

        /**
         * returns the property value; null in case property not found
         */
        String getProperty(String taskId, String property);
    }
}

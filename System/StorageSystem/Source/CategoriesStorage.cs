using System;
using System.Collections.Generic;
using System.Text;
using CrawlerNameSpace.Utilities;

namespace CrawlerNameSpace.StorageSystem
{
    /**
     * This interface defines the functionality used in order to save or to get the 
     * defined categories for specific task
     */ 
    interface CategoriesStorage
    {
        /**
         * returns the category list of the specified task
         */
        List<Category> getCategories(String taskId);

        /**
         * this method sets the categories for the task id to the specified list
         * NOTE: this operation will reset pervious configuration
         */ 
        void setCategories(String taskId, List<Category> categoryList);

        /**
         * This method sets the given categoryID(of the parent) to be a parent
         * of the given other categoryID(of the son).
         */
        void setParentToSon(String parentID, String sonID);
    }
}

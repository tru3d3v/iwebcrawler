using System;
using System.Collections.Generic;
using System.Text;

namespace CrawlerNameSpace.Utilities
{
    /**
     * This interface defines the methods that every regeneratable record should have,
     * regeneratable record can turn itself to string and vice versa .
     */
    interface RegeneratableRecord : ICloneable
    {
        /**
         * This method returns the TaskID to whom the record belongs.
         */
        String getTaskID();

        /**
         * This method returns the Name of the record
         */
        String getRecordName();

        /**
         * This method returns the record as a string when every word in the record is
         * seperated with space.
         */
        String generateRecordString();

        /**
         * This method transforms the given string(which must be at the correct form,
         * meaning that every word in it should be separated)into a record.
         */
        void restoreRecordFromString(String recStr);

    }
}

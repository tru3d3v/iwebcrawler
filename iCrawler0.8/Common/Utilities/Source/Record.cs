using System;
using System.Collections.Generic;
using System.Text;

namespace CrawlerNameSpace.Utilities
{
    /**
     * This class implements the regeneratable record interface.
     */
    
    public class Record : RegeneratableRecord
    {
        private Dictionary<String, String> propertiesMap;
        private String taskID;
        private String recordName;


        /**
         * Constructor:
         */
        public Record(String taskid, String recordname)
        {
            propertiesMap=new Dictionary<string,string>();
            taskID = taskid;
            recordName = recordname;
        }

        /**
         * This method sets the property given as an argument
         * in the properties map,and if it exists it changes it.
         */
        public void setProperty(String key,String value)
        {
            if (propertiesMap.ContainsKey(key))
            {
                propertiesMap[key] = value;
            }
            else
            {
                propertiesMap.Add(key, value);    
            }
        }

        /**
         * This method returns the property of the given key if it exists in the 
         * properties map otherwise it returns null.
         */
        public String getProperty(String key)
        {
            if (propertiesMap.ContainsKey(key))
            {
                return (String)propertiesMap[key].Clone();
            }
            else
            {
                return null;
            }
        }

        /**
         * This method returns the whole set of the keys of the properties map.
         */
        public List<String> getKeysSet()
        {
            List<String> list_keys = new List<string>(propertiesMap.Keys);
            return list_keys;
        }

        /**
         * This method returns the taskID of the task that this record belongs to.
         */
        public String getTaskID()
        {
            return (String)taskID.Clone();
        }

        /**
         * This method returns the name of the record.
         */
        public String getRecordName()
        {
            return (String)recordName.Clone();
        }

        /**
         * This method returns the record as a string when every pair of key,value 
         * in the record are 
         * seperated with "|".
         */
        public String generateRecordString()
        {
            String record="|";
            foreach (KeyValuePair<String,String> pair in propertiesMap)
            {
                record = record + pair.Key + " " + pair.Value + "|";
            }
            return record;
        }

        /**
         * This method transforms the given string(which must be at the correct form,
         * meaning that every word in it should be separated)into a record.
         * It also clears the variable propertiesMap.
         */
        public void restoreRecordFromString(String recStr)
        {
            String[] pairs = recStr.Split('|');
            propertiesMap.Clear();
            foreach (string pair in pairs)
            {
                String[] partsofpair = pair.Split(' ');
                propertiesMap.Add(partsofpair[0], partsofpair[1]);
            }
        }

        /*
         * This method overrides the defualt clone method.
         */
        public object Clone()
        {
            Record copiedRecord = new Record((String)this.taskID.Clone(),(String) this.recordName.Clone());
            Dictionary<String, String> propertiesMapCopy = new Dictionary<string, string>();

            foreach (KeyValuePair<String, String> pair in propertiesMap)
            {
                propertiesMapCopy.Add((String)pair.Key.Clone(),(String) pair.Value.Clone());
            }
            copiedRecord.propertiesMap = propertiesMapCopy;
            return copiedRecord;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace CrawlerNameSpace.Utilities
{
    public class WorkDetails
    {
        static private operationMode_t _operationMode = operationMode_t.Auto;

        static private String _TaskID=null;

        //get operation mode
        public static operationMode_t getOperationMode()
        {
            return _operationMode;
        }

        //sets a new operation mode
        public static void setOperationMode(operationMode_t mode)
        {
            _operationMode = mode;
        }

        //sets a new task id
        public static void setTaskId(String taskid)
        {
            _TaskID = new String(taskid.ToCharArray());
        }

        //gets the task id
        public static String getTaskId()
        {
            return _TaskID;
        }
    }
}

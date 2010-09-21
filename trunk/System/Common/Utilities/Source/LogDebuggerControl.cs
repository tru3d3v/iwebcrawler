using System;
using System.Collections.Generic;
using System.Text;

namespace CrawlerNameSpace.Utilities
{
    /**
     * This class controls wether to write log file to debug or not.
     * This class wil be a singletone class.
     */
    public class LogDebuggerControl
    {
        //reference to the singleton
        static private LogDebuggerControl _logDebugger = null;
        public bool debugCategorization;
        public bool debugRanker;
        public bool debugCategorizationInRanker;

        private LogDebuggerControl()
        {
            debugCategorization = true;
            debugRanker = true;
            debugCategorizationInRanker = false;
        }

        public static LogDebuggerControl getInstance()
        {
            if (_logDebugger == null)
                _logDebugger = new LogDebuggerControl();

            return _logDebugger;
        }
    }
}

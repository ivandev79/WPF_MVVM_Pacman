using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logs
{
    /// <summary>
    ///  Write to file full information about game progress or any exceptions
    /// </summary>
    public class Log
    {
        private DateTime _timeStamp;

        /// <summary>
        /// Date when log was added
        /// </summary>
        public DateTime Time
        {
            get { return _timeStamp; }
            private set { _timeStamp = value; }
        }

        private string _methodName;

        /// <summary>
        /// Application from what method  log was added
        /// </summary>
        public string MethodName
        {
            get { return _methodName; }
            private set { _methodName = value; }
        }

        private string _className;

        /// <summary>
        /// Type
        /// </summary>
        public string ClassName
        {
            get { return _className; }
            set { _className = value; }
        }

        private string _logMessage;

        /// <summary>
        /// Custom log message
        /// </summary>
        public string LogMessage
        {
            get { return _logMessage; }
            private set { _logMessage = value; }
        }


        private Exception _exception;

        /// <summary>
        /// Appplication exception
        /// </summary>
        public Exception Exception
        {
            get { return _exception; }
            set { _exception = value; }
        }


        /// <summary>
        /// Create a log
        /// </summary>
        /// <param name="logMessage">Custom message</param>
        /// <param name="ex">Exception</param>
        public Log(string methodName, string className,string logMessage,Exception ex = null)
        {
            Time = DateTime.Now;
            MethodName = methodName;
            ClassName = className;
            LogMessage = logMessage;
            Exception = ex;
        }

      
        public override string ToString()
        {
            if (Exception==null)
            {
                return $"{Time} Class : {ClassName}; Method : {MethodName} \n {LogMessage} \n";
            }
            return $"{Time} Class : {ClassName}; Method : {MethodName} \n {LogMessage} \n {Exception.HResult} : {Exception.Message??""}\n";
        }
    }
}

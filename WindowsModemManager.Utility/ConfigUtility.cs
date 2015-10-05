using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace WindowsModemManager.Utility
{
    public static class ConfigUtility
    {
        public static string ModemPostUrl
        {
            get
            {
                 return System.Configuration.ConfigurationManager.AppSettings["ModemPostUrl"];
            }
        }

        public static string AuthorizationKey
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["AuthorizationKey"];
            }
        }

        public static string GoogleUrl
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["GoogleUrl"];
            }
        }

        public static string FacebookUrl
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["FacebookUrl"];
            }
        }

        public static string YahooUrl
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["YahooUrl"];
            }
        }

        public static string TwitterUrl
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["TwitterUrl"];
            }
        }

        public static bool IsShoutDownOnFail
        {
            get
            {   var IsShutDown = System.Configuration.ConfigurationManager.AppSettings["IsShoutDownOnFail"] ;
                return !string.IsNullOrWhiteSpace(IsShutDown) && IsShutDown.Equals("1");
            }
        }

        public static int IdleWaitTimeMinute
        {
            get
            {
                var WaitTime = System.Configuration.ConfigurationManager.AppSettings["IdleWaitTimeMinute"];
                int IdleWaitTimeMinute;
                Int32.TryParse(WaitTime, out IdleWaitTimeMinute);
                return IdleWaitTimeMinute != 0 ? IdleWaitTimeMinute : 5;
            }
        }

        public static int NetFailureRetryIntervalMinute
        {
            get
            {
                var WaitTime = System.Configuration.ConfigurationManager.AppSettings["NetFailureRetryIntervalMinute"];
                int NetFailureRetryInterval;
                Int32.TryParse(WaitTime, out NetFailureRetryInterval);
                return NetFailureRetryInterval != 0 ? NetFailureRetryInterval : 5;
            }
        }

        public static int FailureCountForShutDown
        {
            get
            {
                var FailureCountForShutDown = System.Configuration.ConfigurationManager.AppSettings["FailureCountForShutDown"];
                int Count;
                Int32.TryParse(FailureCountForShutDown, out Count);
                return Count != 0 ? Count : 15;
            }
        }
        
        public static string ErrorLogPath
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["ErrorLogPath"];
            }
        }
        
        public static string TraceLogPath
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["TraceLogPath"];
            }
        }

        public static bool IsTracingEnabled
        {
            get
            {
                var IsTracingEnabled = System.Configuration.ConfigurationManager.AppSettings["IsTracingEnabled"];
                return !string.IsNullOrWhiteSpace(IsTracingEnabled) && IsTracingEnabled.Equals("1");
            }
        }

    }
}

using System;
using System.Collections.Generic;
using System.Threading;
using WindowsModemManager.Utility;
using System.Diagnostics;

namespace WindowsModemManager.ConsoleTest
{
    public class ModemManagerProgram
    {
        private static System.Timers.Timer timerOnFail;
        private static System.Timers.Timer timerOnSuccess;
        private static int CounterForShutDown;
        private static bool IsNetAvailableLastTime;
        
        public ModemManagerProgram()
        {
            timerOnFail = new System.Timers.Timer(TimeSpan.FromMinutes(ConfigUtility.NetFailureRetryIntervalMinute).TotalMilliseconds);
            timerOnSuccess = new System.Timers.Timer(TimeSpan.FromMinutes(ConfigUtility.IdleWaitTimeMinute).TotalMilliseconds);
            timerOnFail.Elapsed += ElapsedEventOnFailedTimer;
            timerOnSuccess.Elapsed += ElapsedEventOnSuccessTimer;
            timerOnFail.Start();
            timerOnSuccess.Start();
            CounterForShutDown = 0;
            IsNetAvailableLastTime = false;
        }

        ~ModemManagerProgram()
        {
            timerOnFail.Stop();
            timerOnSuccess.Stop();
        }

       
        static void Main(string[] args)
        {

            ErrorAndLogUtility.EventLoger.WriteEntry("Manual Try", EventLogEntryType.Information);
            //CheckAndRestartModemOnNoInternet();
            System.Diagnostics.EventLog.Delete("WindowsModemManager");
            System.Diagnostics.EventLog.Delete("ModemManager");

        }

        private static void CheckAndRestartModemOnNoInternet()
        {
            try
            {
                if (!WebRequestHelper.IsNetworkAvailable())
                {
                    ErrorAndLogUtility.TraceError(" Network Not Available.." + DateTime.Now.ToString());
                    ErrorAndLogUtility.EventLoger.WriteEntry("Network Not Available..", EventLogEntryType.Warning);
                    IsNetAvailableLastTime = false;
                    return;
                }

                var IsGoogleConnected = WebRequestHelper.SendJSONGetRequest(ConfigUtility.GoogleUrl);
                var IsFacebookActive = WebRequestHelper.SendJSONGetRequest(ConfigUtility.FacebookUrl);

                if (IsGoogleConnected && IsFacebookActive)
                {
                    ErrorAndLogUtility.TraceError("All Ok Sleeping for 10 :" + DateTime.Now.ToString());
                    IsNetAvailableLastTime = true;
                    //Thread.Sleep(TimeSpan.FromMinutes(10));
                    ErrorAndLogUtility.EventLoger.WriteEntry("All Ok..", EventLogEntryType.Information);

                }
                else
                {
                    CounterForShutDown++;
                    IsNetAvailableLastTime = false;
                    ErrorAndLogUtility.TraceError(CounterForShutDown + " Initiating restart.." + DateTime.Now.ToString());
                    ErrorAndLogUtility.EventLoger.WriteEntry(CounterForShutDown + " Initiating restart.." + DateTime.Now.ToString(), EventLogEntryType.Error);

                    var Keys = new Dictionary<string, string>();
                    Keys.Add("Authorization", ConfigUtility.AuthorizationKey);
                    var ModemRestart = WebRequestHelper.SendFormPostRequest(ConfigUtility.ModemPostUrl, Keys);

                    if (ModemRestart.StatusCode == System.Net.HttpStatusCode.OK)
                        Console.WriteLine("Modem Restarted successfully" + DateTime.Now.ToString());

                }
                //Console.ReadKey();
                Console.WriteLine("Modem restarted");
            }
            catch (Exception Ex)
            {
                ErrorAndLogUtility.EventLoger.WriteEntry(Ex.Message + Ex.StackTrace + Ex.Source, EventLogEntryType.Error);
            }
        }

        private static void ElapsedEventOnFailedTimer(Object source, System.Timers.ElapsedEventArgs e)
        {
            if (!IsNetAvailableLastTime)
            {
                Console.WriteLine("The Elapsed event was raised at {0}", e.SignalTime);
                ErrorAndLogUtility.TraceError("Inside Failed Timer");
                ErrorAndLogUtility.EventLoger.WriteEntry("Inside Failed Timer");
                CheckAndRestartModemOnNoInternet();
            }
        }

        private static void ElapsedEventOnSuccessTimer(Object source, System.Timers.ElapsedEventArgs e)
        {
            if (IsNetAvailableLastTime)
            {
                Console.WriteLine("The Elapsed event was raised at {0}", e.SignalTime);
                ErrorAndLogUtility.TraceError("Inside Success Timer");
                ErrorAndLogUtility.EventLoger.WriteEntry("Inside Success Timer");
                CheckAndRestartModemOnNoInternet();
            }
        }
    }
}

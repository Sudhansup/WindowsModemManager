using System;
using System.Collections.Generic;
using System.Threading;
using WindowsModemManager.Utility;

namespace WindowsModemManager.ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var IsGoogleConnected  = WebRequestHelper.SendJSONGetRequest(ConfigUtility.GoogleUrl);
            var IsFacebookActive = WebRequestHelper.SendJSONGetRequest(ConfigUtility.FacebookUrl);

            if (IsGoogleConnected && IsFacebookActive)
            {
                ErrorAndLogUtility.TraceError("All Ok Sleeping for 10 :" + DateTime.Now.ToString());
                //Thread.Sleep(TimeSpan.FromMinutes(10));
                
            }else
            {
                ErrorAndLogUtility.TraceError(" Initiating restart.." + DateTime.Now.ToString()); 

                var Keys = new Dictionary<string, string>();
                Keys.Add("Authorization", ConfigUtility.AuthorizationKey);
                var ModemRestart = WebRequestHelper.SendFormPostRequest(ConfigUtility.ModemPostUrl, Keys);

                if(ModemRestart.StatusCode == System.Net.HttpStatusCode.OK)
                    Console.WriteLine("Modem Restarted successfully" + DateTime.Now.ToString());
                
            }
            //Console.ReadKey();
            Console.WriteLine("Modem restarted");
        }
    }
}

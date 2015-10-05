using System.ServiceProcess;
using WindowsModemManager.ConsoleTest;
using WindowsModemManager.Utility;
using System.Diagnostics;
using System;

namespace WindowsModemManager
{
    public partial class ModemManager : ServiceBase
    {
               
        public ModemManager()
        {
             //this.ServiceName = "ModemManagerService1";
             //this.EventLog.Log = "ModemManagerServiceLog";

            // These Flags set whether or not to handle that specific
            ////  type of event. Set to true if you need it, false otherwise.
            //this.CanHandlePowerEvent = true;
            //this.CanHandleSessionChangeEvent = true;
            //this.CanPauseAndContinue = true;
            //this.CanShutdown = true;
            //this.CanStop = true;
                      
                     

            InitializeComponent();
           
                       
        }

        protected override void OnStart(string[] args)
        {
            ErrorAndLogUtility.EventLoger.WriteEntry("Window Service Startmethod called");

#if DEBUG
           

            System.Diagnostics.Debugger.Launch();
            System.Diagnostics.Debugger.Break();
#endif
            ModemManagerProgram objModemManagerProgram = new ModemManagerProgram();
        }

        protected override void OnStop()
        {
            ErrorAndLogUtility.EventLoger.WriteEntry("Window Service Stop method called");
        }

        /// <summary>
        /// OnShutdown(): Called when the System is shutting down
        /// - Put code here when you need special handling
        ///   of code that deals with a system shutdown, such
        ///   as saving special data before shutdown.
        /// </summary>
        protected override void OnShutdown()
        {
            base.OnShutdown();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsModemManager.Utility;
using System.IO;


namespace WindowsModemManager.Utility
{
    public static class ErrorAndLogUtility
    {
        private const string ErrorLogFileName = "Error";
        private const string TraceLogFileName = "Log";

        public static void WriteError(Exception Ex)
        {
            try
            {
                StringBuilder Sb = new StringBuilder();
                Sb.AppendLine("Errror Message :" + Ex.Message);
                Sb.AppendLine("Errror Source :" + Ex.Source);
                Sb.AppendLine("Errror StackTrace :" + Ex.StackTrace);
                Sb.AppendLine("Errror Date :" + DateTime.Now.ToString());

                WriteInFile(Sb.ToString(), ConfigUtility.ErrorLogPath, ErrorLogFileName + DateTime.Now.ToShortDateString().Replace('/', '_') + ".txt");
            }
            catch (Exception)
            {
                
            }
        }

        public static void TraceError(string Content)
        {
            try
            {
               if(ConfigUtility.IsTracingEnabled)
               {
                   WriteInFile(Content, ConfigUtility.TraceLogPath, TraceLogFileName + DateTime.Now.ToShortDateString().Replace('/', '_') + ".txt");
               }

                
            }
            catch (Exception)
            {

                throw;
            }
        }

        private static void WriteInFile(string Content, string DirectoryPath, string FileName)
        {
            try
            {
                DirectoryPath.CreateDirectoryIfNotExists();
                System.IO.StreamWriter ObjWriter;
                ObjWriter = new System.IO.StreamWriter(Path.Combine(DirectoryPath, FileName), true, ASCIIEncoding.ASCII);
                ObjWriter.WriteLine("=========================================");
                ObjWriter.WriteLine(Content);
                ObjWriter.Close();

            }
            catch (Exception)
            {
                
                throw;
            }
        }

    }
}

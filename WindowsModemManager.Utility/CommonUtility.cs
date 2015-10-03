using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace WindowsModemManager.Utility
{
    public static  class CommonUtility
    {
        public static void CreateDirectoryIfNotExists(this string DirectoryPath)
        {
            try
            {
                if(!Directory.Exists(DirectoryPath))
                {
                    Directory.CreateDirectory(DirectoryPath);
                }
            }
            catch (Exception)
            {
                
                throw;
            }
        }
    }
}

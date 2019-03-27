using System;
using System.IO;

namespace ProntuarioApp.Api.Helper
{
    public static class FileLogger
    {
        public static void LogToFile(string message) {
            // begn log
            string strLogFile = "C:\\inetpub\\wwwroot\\v1\\pubkeylog.txt";
            
            string strLogMessage = string.Format("[{0}] [{1}]", DateTime.Now, message);

            using (var s = new FileStream(strLogFile, !System.IO.File.Exists(strLogFile) ? FileMode.Create : FileMode.Append))
            using (var swLog = new StreamWriter(s))
            {
                swLog.WriteLine(strLogMessage);
                swLog.WriteLine();
            }
            // end log
        }
    }
}
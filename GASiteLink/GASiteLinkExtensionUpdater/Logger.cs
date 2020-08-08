using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GASiteLinkExtensionUpdater
{
    public class Logger
    {
        public static void Log(LogType logType, string message)
        {
            Console.WriteLine(logType.ToString()+":"+message);
        }
        
        public enum LogType
        {
            INFO,WARNING, ERROR,EXCEPTION
        }

    }
}

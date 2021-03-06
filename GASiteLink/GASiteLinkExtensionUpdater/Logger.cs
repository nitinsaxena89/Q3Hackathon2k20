﻿using System;
using System.Configuration;
using System.IO;

namespace GASiteLinkExtensionUpdater
{
    public class Logger
    {
        public static void Log(LogType logType, string message)
        {
            try
            {
                string logFilePath = ConfigurationManager.AppSettings["LogFilePath"];
                if (logFilePath.Trim().Length == 0)
                {
                    logFilePath = "Log.txt";
                }
                using (StreamWriter sw = File.AppendText(logFilePath))
                {
                    string logMessage = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.fff") + ": [" + logType + "] " + message;
                    sw.WriteLine(logMessage);
                    Console.WriteLine(logMessage);
                }
            }catch(Exception ex)
            {
                //Validate File Path
            }

        }

        public enum LogType
        {
            INFO, WARNING, ERROR, EXCEPTION
        }

    }
}

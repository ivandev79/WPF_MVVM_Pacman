using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logs
{
    public  class Logger
    {
        /// <summary>
        /// <param name="WriteWithAdding"> Each added log will write to file.</param>
        /// </summary>
        public static bool WriteWithAdding { get; set; } = false;
        /// <summary>
        /// <param name="LogFilePath"> File path where logs saving.</param>
        /// </summary>
        public static string LogFilePath { get; private set; } = GetDefaultLogFilePath();
        /// <summary>
        /// List of  logs
        /// </summary>
        private static List<Log> Logs { get; set; } = new List<Log>();
     
        /// <summary>
        /// <param name="Frequency"> Each X added log will save all to file</param>
        /// </summary>
        public static int Frequency { get; set; } = 100;
        /// <summary>
        /// Add and write log to file
        /// </summary>
        /// <param name="log">log information</param>
        public static void Add(Log log)
        {
            Logs.Add(log);
            if (WriteWithAdding)
            {
                WriteFileInfo(log);
            }
            else
            {
                if (Logs.Count%Frequency == 0)
                {
                    WriteFileInfo(log);
                    Logs.Clear();
                }
            }
        }

        private static void WriteFileInfo(Log log)
        {
            if (File.Exists(LogFilePath))
            {
                StreamWriter sw = File.AppendText(LogFilePath);

                foreach (var it in Logs)
                {
                    sw.WriteLine(it.ToString());
                }
                sw.Close();
            }
            else
	        {
                using (StreamWriter sw = File.CreateText(LogFilePath))
                {
                    foreach (var it in Logs)
                    {
                        sw.WriteLine(it.ToString());
                    }
                }
            }
        }

        private static string GetDefaultLogFilePath()
        {
            var dir = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName.ToString();
            CreateIfMissing(Path.GetFullPath($@"{dir}/Files/Logs"));
            string tempPath = Path.GetFullPath($@"{dir}/Files/Logs") +$"\\log_{DateTime.Now.ToString("yyyy-MM-dd")}.txt";

            return tempPath;
        }
        /// <summary>
        /// Set Default Log File Path for save
        /// </summary>
        public static void SetDefaultLogFilePath()
        {
            LogFilePath = GetDefaultLogFilePath();
        }

        private static void CreateIfMissing(string path)
        {
            bool folderExists = Directory.Exists(path);
            if (!folderExists)
                Directory.CreateDirectory(path);
        }
    }
}

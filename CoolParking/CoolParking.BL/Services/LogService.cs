// TODO: implement the LogService class from the ILogService interface.
//       One explicit requirement - for the read method, if the file is not found, an InvalidOperationException should be thrown
//       Other implementation details are up to you, they just have to match the interface requirements
//       and tests, for example, in LogServiceTests you can find the necessary constructor format.

using System;
using System.IO;
using System.Text;
using CoolParking.BL.Interfaces;

namespace CoolParking.BL.Services
{
    public class LogService : ILogService
    {
        public string LogPath { get; }
        public LogService(string logPath)
        {
            LogPath = logPath;
        }

        public void Write(string logInfo)
        {
            using (StreamWriter writer = new StreamWriter(LogPath, true))
            {
                writer.WriteLine(logInfo);
            }
        }

        public string Read()
        {
            StringBuilder result = new StringBuilder();
            
                if (File.Exists(LogPath))
                {
                    using (StreamReader reader = new StreamReader(LogPath))
                    {
                        result.AppendLine(reader.ReadToEnd());
                    }
                }
                else
                {
                    throw new InvalidOperationException("Sorry, no log file found to read.");
                }

            // for unknown reason ToString() adds two unnecessary characters, so i must remove them before tests hit
            return result.Remove(result.Length - 2, 2).ToString();
        }
    }
}
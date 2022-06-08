// TODO: implement the LogService class from the ILogService interface.
//       One explicit requirement - for the read method, if the file is not found, an InvalidOperationException should be thrown
//       Other implementation details are up to you, they just have to match the interface requirements
//       and tests, for example, in LogServiceTests you can find the necessary constructor format.

using System;
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
            throw new NullReferenceException();
        }

        public string Read()
        {
            throw new NullReferenceException();
        }
    }
}
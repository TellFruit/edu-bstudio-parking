// TODO: implement class TimerService from the ITimerService interface.
//       Service have to be just wrapper on System Timers.

using System.Timers;
using CoolParking.BL.Interfaces;
using CoolParking.BL.Models;
using Microsoft.VisualBasic;

namespace CoolParking.BL.Services
{
    public class TimerService : ITimerService
    {
        private double _interval;

        public event ElapsedEventHandler Elapsed;

        public double Interval { get; set; }

        public void Start()
        {
            throw new System.NotImplementedException();
        }

        public void Stop()
        {
            throw new System.NotImplementedException();
        }

        public void Dispose()
        {
            throw new System.NotImplementedException();
        }
    }
}
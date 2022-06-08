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
        private readonly Timer _timer;

        public event ElapsedEventHandler Elapsed { 
            add => _timer.Elapsed += value;
            remove => _timer.Elapsed -= value;
        }

        public TimerService()
        {
            _timer = new Timer();
        }

        // think that interval is described in milliseconds, but input is always in seconds (see Settings class)
        public double Interval
        {
            get => _timer.Interval;
            set => _timer.Interval = value * 1000;
        }

        public void Start()
        {
            _timer.Start();
        }

        public void Stop()
        {
            _timer.Stop();
        }

        public void Dispose()
        {
            _timer.Dispose();
        }
    }
}
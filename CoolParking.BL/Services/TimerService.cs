﻿// TODO: implement class TimerService from the ITimerService interface.
//       Service have to be just wrapper on System Timers.

using System.Timers;
using CoolParking.BL.Interfaces;
using CoolParking.BL.Models;
using Microsoft.VisualBasic;

namespace CoolParking.BL.Services
{
    public class TimerService : ITimerService
    {
        // think that interval is described in milliseconds
        private double _interval;
        private readonly Timer _timer;

        public event ElapsedEventHandler Elapsed;

        public TimerService()
        {
            _timer = new Timer();
        }

        public double Interval
        {
            get => _interval;
            set => _interval = value * 1000;
        }

        public void Start()
        {
            _timer.Interval = Interval;

            if (Elapsed != null)
                _timer.Elapsed += Elapsed;

            _timer.Start();
        }

        public void Stop()
        {
            _timer.Stop();
        }

        public void Dispose()
        {
            throw new System.NotImplementedException();
        }
    }
}
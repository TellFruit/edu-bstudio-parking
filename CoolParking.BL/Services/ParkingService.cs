﻿// TODO: implement the ParkingService class from the IParkingService interface.
//       For try to add a vehicle on full parking InvalidOperationException should be thrown.
//       For try to remove vehicle with a negative balance (debt) InvalidOperationException should be thrown.
//       Other validation rules and constructor format went from tests.
//       Other implementation details are up to you, they just have to match the interface requirements
//       and tests, for example, in ParkingServiceTests you can find the necessary constructor format and validation rules.

using System;
using System.Collections.ObjectModel;
using CoolParking.BL.Interfaces;
using CoolParking.BL.Models;

namespace CoolParking.BL.Services
{
    public class ParkingService : IParkingService
    {
        // additional services instances
        private readonly ITimerService _withdrawTimer;
        private readonly ITimerService _loggerTimer;
        private readonly ILogService _loggerService;

        // locally used variables
        private readonly Parking _parking;

        public ParkingService(ITimerService transactionTimer, ITimerService loggerTimer, ILogService loggerService)
        {
            _withdrawTimer = transactionTimer;
            _loggerTimer = loggerTimer;
            _loggerService = loggerService;
            
            _parking = new Parking();
        }

        public void Dispose()
        {
            throw new System.NotImplementedException();
        }

        public decimal GetBalance()
        {
            return _parking.Balance;
        }

        public int GetCapacity()
        {
            return Settings.ParkingPlaces;
        }

        public int GetFreePlaces()
        {
            return Settings.ParkingPlaces - _parking.Vehicles.Count;
        }

        public ReadOnlyCollection<Vehicle> GetVehicles()
        {
            return _parking.Vehicles as ReadOnlyCollection<Vehicle>;
        }

        public void AddVehicle(Vehicle vehicle)
        {
            try
            {
                if (GetFreePlaces() > 0)
                {
                    _parking.Vehicles.Add(vehicle);
                }
                else
                {
                    throw new InvalidOperationException("Sorry, no free places left");
                }
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void RemoveVehicle(string vehicleId)
        {
            throw new System.NotImplementedException();
        }

        public void TopUpVehicle(string vehicleId, decimal sum)
        {
            throw new System.NotImplementedException();
        }

        public TransactionInfo[] GetLastParkingTransactions()
        {
            throw new System.NotImplementedException();
        }

        public string ReadFromLog()
        {
            throw new System.NotImplementedException();
        }
    }
}
// TODO: implement the ParkingService class from the IParkingService interface.
//       For try to add a vehicle on full parking InvalidOperationException should be thrown.
//       For try to remove vehicle with a negative balance (debt) InvalidOperationException should be thrown.
//       Other validation rules and constructor format went from tests.
//       Other implementation details are up to you, they just have to match the interface requirements
//       and tests, for example, in ParkingServiceTests you can find the necessary constructor format and validation rules.

using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Timers;
using CoolParking.BL.Interfaces;
using CoolParking.BL.Models;
using CoolParking.BL.Validation;

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

            _withdrawTimer.Interval = Settings.TransactionSecondsPeriod;
            _withdrawTimer.Elapsed += OnWithdrawMoment;
            _loggerTimer.Interval = Settings.LoggingSecondsPeriod;
            _withdrawTimer.Elapsed += OnLogMoment;

            _withdrawTimer.Start();
            _loggerTimer.Start();
        }

        public void Dispose()
        {
            _withdrawTimer.Stop();
            _loggerTimer.Stop();

            _withdrawTimer.Dispose();
            _loggerTimer.Dispose();

            _parking.RecentTransactions.Clear();
            _parking.Vehicles.Clear();
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
                // input validation
                if (CheckVehicleForCtorFail(vehicle))
                    throw new ArgumentException("Try put vehicle again or return later.");
                if (CheckVehicleForDuplicate(vehicle, _parking))
                    throw new ArgumentException("Vehicle with this Id is already parked");

                // parking logic validation
                if (GetFreePlaces() > 0)
                    _parking.Vehicles.Add(vehicle);
                else
                    throw new InvalidOperationException("Sorry, no free places left.");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void RemoveVehicle(string vehicleId)
        {
            try
            {
                Vehicle vehicle = _parking.Vehicles.FirstOrDefault(v => v.Id == vehicleId);

                // if no car found FirstOrDefault will return null
                if (vehicle != null)
                {
                    if (vehicle.Balance >= 0)
                    {
                        _parking.Vehicles.Remove(vehicle);
                    }
                    else
                    {
                        throw new InvalidOperationException("Please, pay your dept first.");
                    }
                }
                else
                {
                    throw new NullReferenceException("Sorry, no such car is registered.");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void TopUpVehicle(string vehicleId, decimal sum)
        {
            try
            {
                if (CommonValidation.CheckBalancePush(sum))
                    throw new ArgumentException("You cannot add negative or zero numbers to vehicle balance.");
                    
                Vehicle vehicle = _parking.Vehicles.FirstOrDefault(v => v.Id == vehicleId);

                if (vehicle != null)
                {
                    vehicle.Balance += sum;
                }
                else
                {
                    throw new NullReferenceException("Sorry, no such car is registered.");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public TransactionInfo[] GetLastParkingTransactions()
        {
            return _parking.RecentTransactions.ToArray();
        }

        public string ReadFromLog()
        {
            return _loggerService.Read();
        }

        private void OnWithdrawMoment(object source, ElapsedEventArgs e)
        {
            foreach (var vehicle in GetVehicles())
            {
                decimal sum = AssessTransactionSum(vehicle);

                vehicle.Balance -= sum;
                _parking.Balance += sum;
                
                _parking.RecentTransactions.Add(new TransactionInfo(vehicle.Id, DateTime.Now, sum));
            }
        }

        private void OnLogMoment(object source, ElapsedEventArgs e)
        {
            foreach (var transaction in GetLastParkingTransactions())
            {
                _loggerService.Write($"{transaction.VehicleId}: {transaction.OperationDate} {transaction.Sum}");
            }

            _parking.RecentTransactions.Clear();
        }
        
        private static decimal AssessTransactionSum(Vehicle vehicle)
        {
            decimal sum;

            if (vehicle.Balance >= vehicle.Balance - vehicle.TariffPrice)
                sum = vehicle.TariffPrice;
            else
                sum = vehicle.Balance + (vehicle.TariffPrice - vehicle.Balance) * Settings.FeeCoefficient;

            if (vehicle.Balance < 0)
                sum = vehicle.TariffPrice * Settings.FeeCoefficient;

            return sum;
        }

        private static bool CheckVehicleForDuplicate(Vehicle vehicle, Parking parking)
        {
            Vehicle duplicate = parking.Vehicles.FirstOrDefault(v => v.Id == vehicle.Id);

            if (duplicate != null)
                return true;

            return false;
        }

        private static bool CheckVehicleForCtorFail(Vehicle vehicle)
        {
            // the thing is vehicle validation is done inside Vehicle class
            // so there we only need to check id for emptiness
            // empty == fail 
            if (vehicle.Id == "")
                return true;

            return false;
        }
    }
}
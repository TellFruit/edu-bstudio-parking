using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using CoolParking.BL.Interfaces;
using CoolParking.BL.Models;
using CoolParking.BL.Services;

namespace CoolParking.UI.Console
{
    enum Actions
    {
        Exit ,
        GetBalance,
        GetRecentBalance, 
        AddVehicle,
        TopUpVehicle,
        RemoveVehicle,
        GetFreePlaces,
        GetRecentTransactions,
        GetVehiclesInParking,
        ReadLog,

    }
    internal class ConsoleControl
    {
        private readonly ApiAccessConsole _apiAccess;
        public ConsoleControl(ApiAccessConsole apiAccess)
        {
            _apiAccess = apiAccess;
        }

        public void LaunchProgram()
        {
            while (true)
            {
                ShowActionList();

                try
                {
                    Actions action = (Actions) Int32.Parse(System.Console.ReadLine() ??
                                                           throw new InvalidOperationException("Non integer input"));
                    System.Console.WriteLine("-------------------------");


                    switch (action)
                    {
                        case Actions.GetBalance:
                            System.Console.WriteLine("Balance: " + _apiAccess.GetBalance().Result);
                            break;
                        case Actions.GetRecentBalance:
                            //System.Console.WriteLine("Recent income: " + _parkingService.GetRecentBalance());
                            break;
                        case Actions.AddVehicle:
                        {
                            System.Console.WriteLine("Enter vehicle id: ");
                            string id = System.Console.ReadLine() ?? throw new InvalidOperationException("Invalid");
                           
                            System.Console.WriteLine("Enter vehicle type (PassengerCar - 1; Truck - 2; Bus - 3; Motorcycle -4): ");
                            VehicleType type = (VehicleType)Int32.Parse(System.Console.ReadLine() ??
                                                                    throw new InvalidOperationException("Non integer input"));
                                
                            System.Console.WriteLine("Enter vehicle initial balance (higher than 0): ");
                            decimal balance = Decimal.Parse(System.Console.ReadLine() ?? throw new InvalidOperationException("Non decimal input"));

                            HttpStatusCode statusCode = _apiAccess.CreateVehicle(id, type, balance).Result;
                            if ( statusCode == HttpStatusCode.Created)
                                System.Console.WriteLine("Vehicle added.");
                            else
                                System.Console.WriteLine("Error: " + statusCode);
                        }
                            break;
                        case Actions.TopUpVehicle:
                        {
                            System.Console.WriteLine("Enter vehicle id you want to delete: ");
                            string id = System.Console.ReadLine() ?? throw new InvalidOperationException("Invalid");

                            System.Console.WriteLine("Enter vehicle initial balance (higher than 0): ");
                            decimal topup = Decimal.Parse(System.Console.ReadLine() ?? throw new InvalidOperationException("Non decimal input"));

                            //_parkingService.TopUpVehicle(id, topup);
                        }
                            break;
                        case Actions.RemoveVehicle:
                        {
                            System.Console.WriteLine("Enter vehicle id you want to delete: ");
                            string id = System.Console.ReadLine();

                           // _parkingService.RemoveVehicle(id);

                        }
                            break;
                        case Actions.GetFreePlaces:
                        {
                           // System.Console.WriteLine("Total free places: " + _parkingService.GetFreePlaces());
                        }
                            break;
                        case Actions.GetRecentTransactions:
                        {
                            /*foreach (var lastParkingTransaction in _parkingService.GetLastParkingTransactions())
                            {
                                System.Console.WriteLine($"Time: {lastParkingTransaction.OperationDate.Hour}:{lastParkingTransaction.OperationDate.Minute}; Date: {lastParkingTransaction.OperationDate.Day}.{lastParkingTransaction.OperationDate.Month}.{lastParkingTransaction.OperationDate.Year} Vehicle Id = {lastParkingTransaction.VehicleId}; Sum = {lastParkingTransaction.Sum}");
                            }*/
                        }
                            break;
                        case Actions.GetVehiclesInParking:
                        {
                            /*foreach (var vehicle in _parkingService.GetVehicles())
                            {
                                System.Console.WriteLine($"ID - {vehicle.Id}; Type - {vehicle.VehicleType}; Balance: {vehicle.Balance}");
                            }*/
                        }
                            break;
                        case Actions.ReadLog:
                        {/*
                            System.Console.WriteLine("All recorded transactions: ");
                            System.Console.WriteLine(_parkingService.ReadFromLog());*/
                        }
                            break;
                        case Actions.Exit:
                            //_parkingService.Dispose();
                            System.Environment.Exit(0);
                            break;
                    }
                }
                catch (Exception e)
                {
                    System.Console.WriteLine();
                    System.Console.WriteLine(e.Message);
                }
            }
        }

        private void ShowActionList()
        {
            System.Console.Write(@"
Action indexes:
GetBalance - 1
GetRecentBalance - 2
AddVehicle - 3
TopUpVehicle - 4
RemoveVehicle - 5
GetFreePlaces - 6
GetRecentTransactions - 7
GetVehiclesInParking - 8
ReadLog - 9

Exit - 0

");
        }
    }
}

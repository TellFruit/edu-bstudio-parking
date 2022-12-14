using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        GetCapacity,
        GetRecentTransactions,
        GetVehiclesInParking,
        GetVehicleById,
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
                            System.Console.WriteLine("Balance: " + _apiAccess.GetRecentBalance().Result);
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
                            System.Console.WriteLine("Enter vehicle id of your machine: ");
                            string id = System.Console.ReadLine() ?? throw new InvalidOperationException("Invalid");

                            System.Console.WriteLine("Enter required money (higher than 0): ");
                            decimal topup = Decimal.Parse(System.Console.ReadLine() ?? throw new InvalidOperationException("Non decimal input"));

                            HttpStatusCode statusCode = _apiAccess.TopUpVehicle(id, topup).Result;

                            if (statusCode == HttpStatusCode.OK)
                                System.Console.WriteLine("Increased balance of that vehicle!");
                            else
                                System.Console.WriteLine("Error: " + statusCode);
                        }
                            break;
                        case Actions.RemoveVehicle:
                        {
                            System.Console.WriteLine("Enter vehicle id you want to delete: ");
                            string id = System.Console.ReadLine() ?? throw new InvalidOperationException("Invalid"); ;

                            HttpStatusCode statusCode = _apiAccess.DeleteVehicle(id).Result;
                            
                            if (statusCode == HttpStatusCode.NoContent)
                                System.Console.WriteLine("Vehicle removed.");
                            else
                                System.Console.WriteLine("Error: " + statusCode);
                        }
                            break;
                        case Actions.GetFreePlaces:
                        {
                           System.Console.WriteLine("Total free places: " + _apiAccess.GetFreePlaces().Result);
                        }
                            break;
                        case Actions.GetCapacity:
                        {
                            System.Console.WriteLine("Parking capacity: " + _apiAccess.GetCapacity().Result);
                        }
                            break;
                        case Actions.GetRecentTransactions:
                        {
                            ReadOnlyCollection<TransactionInfo>? transactionInfos = _apiAccess.GetLastTransactions().Result;

                            if (transactionInfos != null)
                                foreach (var transaction in transactionInfos)
                                {
                                    System.Console.WriteLine(
                                        $"Time: {transaction.OperationDate.Hour}:{transaction.OperationDate.Minute}; Date: {transaction.OperationDate.Day}.{transaction.OperationDate.Month}.{transaction.OperationDate.Year} Vehicle Id = {transaction.VehicleId}; Sum = {transaction.Sum}");
                                }
                            else
                                System.Console.WriteLine("No transactions found.");
                        }
                            break;
                        case Actions.GetVehiclesInParking:
                        {
                            ReadOnlyCollection<Vehicle>? vehicles = _apiAccess.GetVehicles().Result;

                            if (vehicles != null)
                                foreach (var vehicle in vehicles)
                                {
                                    System.Console.WriteLine(
                                        $"ID - {vehicle.Id}; Type - {vehicle.VehicleType}; Balance: {vehicle.Balance}");
                                }
                            else
                                System.Console.WriteLine("No vehicles in the parking");
                        }
                            break;
                        case Actions.GetVehicleById:
                        {
                            System.Console.WriteLine("Enter vehicle id of your machine: ");
                            string id = System.Console.ReadLine() ?? throw new InvalidOperationException("Invalid");
                            
                            var result = _apiAccess.GetVehicleById(id).Result;

                            System.Console.WriteLine(
                                result.Item1 != null
                                    ? $"Found:\nID - {result.Item1.Id}; Type - {result.Item1.VehicleType}; Balance: {result.Item1.Balance}"
                                    : $"Error: {result.Item2}");
                        }
                            break;
                        case Actions.ReadLog:
                        {
                            var result = _apiAccess.ReadFromLog().Result;

                            System.Console.WriteLine(
                                result.Item1 != null
                                    ? result.Item1
                                    : $"Error: {result.Item2}");
                        }
                            break;
                        case Actions.Exit:
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
            System.Console.WriteLine();
            System.Console.WriteLine("Action indexes:");
            System.Console.WriteLine("GetBalance - 1");
            System.Console.WriteLine("GetRecentBalance - 2");
            System.Console.WriteLine("AddVehicle - 3");
            System.Console.WriteLine("TopUpVehicle - 4");
            System.Console.WriteLine("RemoveVehicle - 5");
            System.Console.WriteLine("GetFreePlaces - 6");
            System.Console.WriteLine("GetCapacity - 7");
            System.Console.WriteLine("GetRecentTransactions - 8");
            System.Console.WriteLine("GetVehiclesInParking - 9");
            System.Console.WriteLine("GetVehicleById - 10");
            System.Console.WriteLine("ReadLog - 11");
            System.Console.WriteLine();
            System.Console.WriteLine("Exit - 0");
            System.Console.WriteLine();
        }
    }
}

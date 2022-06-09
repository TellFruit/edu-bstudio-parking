using System;
using System.Collections.Generic;
using System.Linq;
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
        AddVehicle
    }
    internal class ConsoleControl
    {
        private readonly ParkingService _parkingService;
        private readonly ITimerService _withdrawTimer;
        private readonly ITimerService _logTimer;
        private readonly ILogService _logService;

        public ConsoleControl()
        {
            _withdrawTimer = new TimerService();
            _logTimer = new TimerService();
            _logService = new LogService(Settings.LogPath);

            _parkingService = new ParkingService(_withdrawTimer, _logTimer, _logService);
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
                    switch (action)
                    {
                        case Actions.GetBalance:
                            _parkingService.GetBalance();
                            break;
                        case Actions.AddVehicle:
                        {
                            System.Console.WriteLine("Enter vehicle id: ");
                            string id = System.Console.ReadLine();
                            
                            System.Console.WriteLine("Enter vehicle id: ");
                            VehicleType type = (VehicleType)Int32.Parse(System.Console.ReadLine() ??
                                                                    throw new InvalidOperationException("Non integer input"));
                                
                            System.Console.WriteLine("Enter vehicle id: ");
                            decimal balance = Decimal.Parse(System.Console.ReadLine() ?? throw new InvalidOperationException("Non decimal input"));

                            _parkingService.AddVehicle(new Vehicle(id, type, balance));
                        }
                            break;
                        case Actions.Exit:
                            System.Environment.Exit(0);
                            break;

                    }
                }
                catch (Exception e)
                {
                    System.Console.WriteLine(e.Message);
                }
            }
        }

        private void ShowActionList()
        {
            System.Console.WriteLine();
        }
    }
}

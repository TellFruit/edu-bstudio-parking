// TODO: implement class Vehicle.
//       Properties: Id (string), VehicleType (VehicleType), Balance (decimal). DONE
//       The format of the identifier is explained in the description of the home task.
//       Id and VehicleType should not be able for changing. DONE
//       The Balance should be able to change only in the CoolParking.BL project. DONE
//       The type of constructor is shown in the tests and the constructor should have a validation, which also is clear from the tests.
//       Static method GenerateRandomRegistrationPlateNumber should return a randomly generated unique identifier.

using System;
using System.IO;

namespace CoolParking.BL.Models
{
    public class Vehicle
    {
        public string Id { get; }
        public VehicleType VehicleType { get; }
        public decimal TariffModifier { get; }
        public decimal Balance { get; internal set; }

        public Vehicle(string id, VehicleType vehicleType, decimal balance)
        {
            Id = id;
            VehicleType = vehicleType;
            Balance = balance;

            TariffModifier = AssignTariff(VehicleType);
        }

        public static decimal AssignTariff(VehicleType vehicleType)
        {
            switch(vehicleType)
            {
                case VehicleType.PassengerCar:
                    return Settings.PassengerCarTariff;
                case VehicleType.Truck:
                    return Settings.TruckTariff;
                case VehicleType.Bus:
                    return Settings.BusTariff;
                case VehicleType.Motorcycle:
                    return Settings.MotorcycleTariff;
            }

            return 0;
        }
    }
}
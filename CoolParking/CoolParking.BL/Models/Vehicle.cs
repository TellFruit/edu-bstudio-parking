// TODO: implement class Vehicle.
//       Properties: Id (string), VehicleType (VehicleType), Balance (decimal). DONE
//       The format of the identifier is explained in the description of the home task.
//       Id and VehicleType should not be able for changing. DONE
//       The Balance should be able to change only in the CoolParking.BL project. DONE
//       The type of constructor is shown in the tests and the constructor should have a validation, which also is clear from the tests.
//       Static method GenerateRandomRegistrationPlateNumber should return a randomly generated unique identifier.

using System;
using System.Text;
using CoolParking.BL.Validation;
using static System.Char;

namespace CoolParking.BL.Models
{
    public class Vehicle
    {
        public string Id { get; }
        public VehicleType VehicleType { get; }
        public decimal TariffPrice { get; }
        public decimal Balance { get; internal set; }

        public Vehicle(string id, VehicleType vehicleType, decimal balance)
        {
            if (CheckValidation(id, balance, vehicleType))
            {
                Id = id;
                VehicleType = vehicleType;
                Balance = balance;


                TariffPrice = AssignTariff(VehicleType);
            }
        }

        public static decimal AssignTariff(VehicleType vehicleType)
        {
            switch (vehicleType)
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

        private static bool CheckValidation(string id, decimal balance, VehicleType type)
        {
            if (CheckForIdFailure(id))
                throw new ArgumentException("Sorry, incorrect id format.");
            if (CommonValidation.CheckBalancePush(balance))
                throw new ArgumentException("Sorry, incorrect balance value.");
            if (CheckVehicleTypeFailure(type))
                throw new ArgumentException("Sorry, incorrect vehicleType value.");

            // if no error, validation is passed
            return true;
        }

        public static bool CheckVehicleTypeFailure(VehicleType vehicleType)
        {
            if (vehicleType < VehicleType.PassengerCar || vehicleType > VehicleType.Motorcycle)
                return true;

            return false;
        }

        public static bool CheckForIdFailure(string id)
        {
            // the most basic assumption
            if (string.IsNullOrEmpty(id))
                return true;

            string[] parts = id.Split("-");

            // string with incorrect sections will give incorrect split result
            if (parts.Length != 3)
                return true;

            if (parts[0].Length != 2 || parts[2].Length != 2)
                return true;

            if (parts[1].Length != 4)
                return true;

            // examine all the parts of id
            for (int i = 0; i < parts.Length; i++)
            {
                // validation is done char by char
                char[] chars = parts[i].ToCharArray();

                for (int j = 0; j < chars.Length; j++)
                {
                    // if we examine syllables parts of id
                    if (i == 0 || i == 2)
                    {
                        if (IsLetter(chars[j]))
                        {
                            if (IsUpper(chars[j]))
                                continue;

                            return true;
                        }

                        return true;
                    }

                    // this section is reached only when i = 1 due to restrictions
                    if (IsDigit(chars[j]))
                        continue;

                    // again, if all is false - the id is invalid
                    return true;
                }
            }

            // if id survived all the checks, it was correct!
            return false;
        }

        public static string GenerateRandomRegistrationPlateNumber()
        {
            StringBuilder id = new StringBuilder();

            Random rnd = new Random();
            // three because it consists of three parts
            for (int i = 0; i < 3; i++)
            {

                if (i == 0 || i == 2)
                {
                    for (int j = 0; j < 2; j++)
                    {
                        int ascii_index = rnd.Next(65, 91); //ASCII character codes 65-90
                        char myRandomUpperCase = Convert.ToChar(ascii_index);

                        id.Append(myRandomUpperCase);
                    }
                }
                else if (i == 1)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        int myRandomDigit = rnd.Next(1, 10); 

                        id.Append(myRandomDigit);
                    }
                }

                if (i < 2)
                {
                    id.Append("-");
                }
            }

            return id.ToString();
        }
    }
}
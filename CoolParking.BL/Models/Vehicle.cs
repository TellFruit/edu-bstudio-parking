// TODO: implement class Vehicle.
//       Properties: Id (string), VehicleType (VehicleType), Balance (decimal). DONE
//       The format of the identifier is explained in the description of the home task.
//       Id and VehicleType should not be able for changing. DONE
//       The Balance should be able to change only in the CoolParking.BL project. DONE
//       The type of constructor is shown in the tests and the constructor should have a validation, which also is clear from the tests.
//       Static method GenerateRandomRegistrationPlateNumber should return a randomly generated unique identifier.

using System;
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
            if (CheckValidation(id, balance))
            {
                Id = id;
                VehicleType = vehicleType;
                Balance = balance;


                TariffPrice = AssignTariff(VehicleType);
            }
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

        private static bool CheckValidation(string id, decimal balance)
        {
            try
            {
                if (CheckForIdFailure(id))
                    throw new ArgumentException("Sorry, incorrect id format.");
                if (CommonValidation.CheckBalancePush(balance))
                    throw new ArgumentException("Sorry, incorrect balance value.");

                // if no error, validation is passed
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        private static bool CheckForIdFailure(string id)
        {
            String[] parts = id.Split("-");

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
                char[] chars = parts[0].ToCharArray();

                for (int j = 0; j < chars.Length; j++)
                {
                    // if we examine syllables parts of id
                    if (i == 0 || i == 2)
                    {
                        if (IsLetter(chars[i]))
                        {
                            if (IsUpper(chars[i]))
                                continue;

                            return true;
                        }
                        
                        return true;
                    }

                    // this section is reached only when i = 1 due to restrictions
                    if (IsDigit(chars[i]))
                        continue;

                    // again, if all is false - the id is invalid
                    return true;
                }
            }

            // if id survived all the checks, it was correct!
            return false;
        }
    }
}
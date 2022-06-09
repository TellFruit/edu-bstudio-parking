
using CoolParking.BL.Models;
using CoolParking.UI.Console;

namespace CoolParking.Ui.Console
{
    class Program
    {
        public static void Main()
        {
            System.Console.WriteLine(Vehicle.GenerateRandomRegistrationPlateNumber());
            //new ConsoleControl().LaunchProgram();
        }
    }
}


// TODO: implement class Settings.
//       Implementation details are up to you, they just have to meet the requirements of the home task.

using System.IO;
using System.Reflection;

namespace CoolParking.BL.Models
{
    public static class Settings
    {
        // parking base settings
        public static int InitParkingBalance = 0;
        public static int ParkingPlaces = 10;
        
        // time periods elapsing
        public static int TransactionSecondsPeriod = 5;
        public static int LoggingSecondsPeriod = 60;

        // Tariff prices based on vehicle type
        public static decimal PassengerCarTariff = 2M;
        public static decimal TruckTariff = 5M;
        public static decimal BusTariff = 3.5M;
        public static decimal MotorcycleTariff = 1M;

        // fee coefficient
        public static decimal FeeCoefficient = 2.5M;

        // path to access or create log file
        public static string LogPath = $@"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\Transactions.log";
        public static string BaseApiAddress = "https://localhost:7179/api";
    }
}
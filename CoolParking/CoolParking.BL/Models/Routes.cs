using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoolParking.BL.Models
{
    public static class Routes
    {
        public static string BaseApiAddress = "https://localhost:7179/api";


        public static string ParkingBalance = "/parking/balance";

        public static string ParkingRecentBalance = "/parking/recentBalance";

        public static string ParkingCapacity = "/parking/capacity";

        public static string ParkingFreePlaces = "/parking/freePlaces";


        public static string Vehicles = "/vehicles";


        public static string TransactionsLast = "/transactions/last";

        public static string TransactionsAll = "/transactions/all";

        public static string TransactionsTopUp = "/transactions/topUpVehicle";
    }
}

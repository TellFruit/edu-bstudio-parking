// TODO: implement class Parking.
//       Implementation details are up to you, they just have to meet the requirements 
//       of the home task and be consistent with other classes and tests.
using System.Collections.Generic;

namespace CoolParking.BL.Models
{
    public class Parking
    {
        public decimal Balance { get; internal set; }
        public decimal RecentIncome { get; internal set; }
        public IList<Vehicle> Vehicles { get; }
        public IList<TransactionInfo> RecentTransactions { get; }

        public Parking()
        {
            Vehicles = new List<Vehicle>();
            RecentTransactions = new List<TransactionInfo>();
        }
    }
}
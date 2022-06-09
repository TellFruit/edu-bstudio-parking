// TODO: implement struct TransactionInfo.
//       Necessarily implement the Sum property (decimal) - is used in tests.
//       Other implementation details are up to you, they just have to meet the requirements of the homework.

using System;

namespace CoolParking.BL.Models
{
    public class TransactionInfo
    {
        public DateTime OperationDate { get; }
        public string VehicleId { get; }
        public decimal Sum { get; }
        public TransactionInfo(string vehicleId, DateTime operationDate, decimal sum)
        {
            VehicleId = vehicleId;
            OperationDate = operationDate;
            Sum = sum;
        }

    }
}
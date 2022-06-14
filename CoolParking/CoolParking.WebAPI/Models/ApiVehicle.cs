using System.ComponentModel.DataAnnotations;
using CoolParking.BL.Models;

namespace CoolParking.WebAPI.Models
{
    public class ApiVehicle
    {
        public string Id { get; }
        public VehicleType VehicleType { get; }
        public decimal Balance { get; internal set; }
    }
}

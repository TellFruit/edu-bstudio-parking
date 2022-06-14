using System.ComponentModel.DataAnnotations;
using CoolParking.BL.Models;

namespace CoolParking.WebAPI.Models
{
    public class ApiVehicle
    {
        public string Id { get; set; }
        public VehicleType VehicleType { get; set; }
        public decimal Balance { get; set; }
    }
}

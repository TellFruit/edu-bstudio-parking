using System.ComponentModel.DataAnnotations;
using CoolParking.BL.Models;

namespace CoolParking.WebAPI.Models
{
    public class ApiVehicle
    {
        [Required]
        public string Id { get; }
        [Required]
        public VehicleType VehicleType { get; }
        [Required]
        public decimal Balance { get; internal set; }
    }
}

using CoolParking.BL.Interfaces;
using CoolParking.BL.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoolParking.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParkingController : ControllerBase
    {
        private readonly IParkingService _parking;
        public ParkingController(IParkingService parking)
        {
            _parking = parking;
        }

        [HttpGet("balance")]
        public IActionResult GetBalance() => Ok(_parking.GetBalance());

        [HttpGet("capacity")]
        public IActionResult GetCapacity() => Ok(_parking.GetCapacity());

    }
}

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

        /*
         * GET api/parking/balance
           
           Response:
           If request is handled successfully
           Status Code: 200 OK
           Body schema: decimal
           Body example: 10.5
         */

        [HttpGet("balance")]
        public IActionResult GetBalance() => Ok(_parking.GetBalance());

        [HttpGet("recentBalance")]
        public IActionResult GetRecentBalance() => Ok(_parking.GetRecentBalance());

        /*
         * GET api/parking/capacity
           
           Response:
           If request is handled successfully
           Status Code: 200 OK
           Body schema: int
           Body example: 10
         */

        [HttpGet("capacity")]
        public IActionResult GetCapacity() => Ok(_parking.GetCapacity());

        /*
         * GET api/parking/freePlaces
           
           Response:
           If request is handled successfully
           Status Code: 200 OK
           Body schema: int
           Body example: 9
         */

        [HttpGet("freePlaces")]
        public IActionResult GetFreePlaces () => Ok(_parking.GetFreePlaces());

    }
}

using CoolParking.BL.Interfaces;
using CoolParking.WebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoolParking.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehiclesController : ControllerBase
    {
        private readonly IParkingService _parking;
        public VehiclesController(IParkingService parking)
        {
            _parking = parking;
        }
    }
}

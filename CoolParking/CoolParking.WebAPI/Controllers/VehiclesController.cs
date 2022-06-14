using System.Net;
using CoolParking.BL.Interfaces;
using CoolParking.BL.Models;
using CoolParking.WebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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


        /*
         * POST api/vehicles
           
           Request:
           Body schema: { “id”: string, “vehicleType”: int, “balance”: decimal }
           Body example: { “id”: “LJ-4812-GL”, “vehicleType”: 2, “balance”: 100 }
           Response:
           If body is invalid - Status Code: 400 Bad Request
           If request is handled successfully
           Status Code: 201 Created
           Body schema: { “id”: string, “vehicleType”: int, "balance": decimal }
           Body example: { “id”: “LJ-4812-GL”, “vehicleType”: 2, "balance": 100 }
         */

        [HttpPost]
        public IActionResult Post(ApiVehicle apiVehicle)
        {
            try
            {
                if (!string.IsNullOrEmpty(apiVehicle.Id))
                {
                    Vehicle vehicle = new Vehicle(apiVehicle.Id, apiVehicle.VehicleType, apiVehicle.Balance);
                    
                    _parking.AddVehicle(vehicle);

                    string json = JsonConvert.SerializeObject(vehicle);

                    return Created(Url.Link("DefaultApi", new {id = vehicle.Id}) ?? string.Empty, json);
                }
                
                throw new InvalidOperationException("Inconsistent json body. Required field: string Id, VehicleType VehicleType, decimal Balance");
                
            }
            // no exception specification because every of them described inside of the classes
            // lead to the same status code response 
            catch (Exception e)
            {
                return BadRequest(new ApiError((int)HttpStatusCode.BadRequest, e.Message));
            }
        }
    }
}

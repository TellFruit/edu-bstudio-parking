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
         * GET api/vehicles
           
           Response:
           If request is handled successfully
           Status Code: 200 OK
           Body schema: [{ “id”: string, “vehicleType”: int, "balance": decimal }]
           Body example: [{ “id”: “GP-5263-GC”, “vehicleType”: 2, "balance": 196.5 }]
         */

        [HttpGet]
        public IActionResult Get() => Ok(_parking.GetVehicles());

        /*
         * GET api/vehicles/id (id - vehicle id of format “AA-0001-AA”)
           
           Response:
           If id is invalid - Status Code: 400 Bad Request
           If vehicle not found - Status Code: 404 Not Found
           If request is handled successfully
           Status Code: 200 OK
           Body schema: { “id”: string, “vehicleType”: int, "balance": decimal }
           Body example: { “id”: “GP-5263-GC”, “vehicleType”: 2, "balance": 196.5 }
         */

        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            try
            {
                if (Vehicle.CheckForIdFailure(id))
                    throw new ArgumentException("Sorry, incorrect id format.");

                Vehicle vehicle = _parking.GetVehicles().First(x => x.Id == id);

                return Ok(vehicle);
            }
            // this - for invalid id handling
            catch (ArgumentException e)
            {
                return BadRequest(new ApiError((int)HttpStatusCode.BadRequest, e.Message));
            }
            // that - for no vehicles found handling
            catch (InvalidOperationException e)
            {
                return NotFound(new ApiError((int)HttpStatusCode.NotFound, e.Message));
            }
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

        [HttpPost("post")]
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

        /*
         * DELETE api/vehicles/id (id - vehicle id of format “AA-0001-AA”)
           
           Response:
           If id is invalid - Status Code: 400 Bad Request
           If vehicle not found - Status Code: 404 Not Found
           If request is handled successfully
           Status Code: 204 No Content
         */

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            try
            {
                if (Vehicle.CheckForIdFailure(id))
                    throw new ArgumentException("Sorry, incorrect id format.");

                Vehicle vehicle = _parking.GetVehicles().First(x => x.Id == id);

                _parking.RemoveVehicle(vehicle.Id);

                return NoContent();
            }
            // this - for invalid id handling
            catch (ArgumentException e)
            {
                return BadRequest(new ApiError((int) HttpStatusCode.BadRequest, e.Message));
            }
            // that - for no vehicles found handling
            catch (InvalidOperationException e)
            {
                return NotFound(new ApiError((int)HttpStatusCode.NotFound, e.Message));
            }
        }
    }
}

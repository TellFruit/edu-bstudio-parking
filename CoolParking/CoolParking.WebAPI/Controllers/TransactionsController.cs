using System.Net;
using CoolParking.BL.Interfaces;
using CoolParking.WebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoolParking.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly IParkingService _parking;

        public TransactionsController(IParkingService parking)
        {
            _parking = parking;
        }

        /*
         * GET api/transactions/last
           
           Response:
           If request is handled successfully
           Status Code: 200 OK
           Body schema: [{ “vehicleId”: string, “sum”: decimal, "transactionDate": DateTime }]
           Body example: [{ “vehicleId”: "DG-3024-UB", “sum”: 3.5, "transactionDate": "2020-05-10T11:36:20.6395402+03:00"}]
         */

        [HttpGet("last")]
        public IActionResult LastTransactions() => Ok(_parking.GetLastParkingTransactions());

        /*
         * GET api/transactions/all (тільки транзакції з лог файлу)
           
           Response:
           If log file not found - Status Code: 404 Not Found
           If request is handled successfully
           Status Code: 200 OK
           Body schema: string
           Body example: “5/10/2020 11:21:20 AM: 3.50 money withdrawn from vehicle with Id='GP-5263-GC'.\n
                        5/10/2020 11:21:25 AM: 3.50 money withdrawn from vehicle with Id='GP-5263-GC'.”
         */

        [HttpGet("all")]
        public IActionResult GetLogTransactions()
        {
            try
            {
                string log = _parking.ReadFromLog();

                return Ok(log);
            }
            catch (InvalidOperationException e)
            {
                return NotFound(new ApiError((int) HttpStatusCode.NotFound, e.Message));
            }
        }
    }
}

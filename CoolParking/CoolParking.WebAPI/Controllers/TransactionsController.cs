using CoolParking.BL.Interfaces;
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
    }
}

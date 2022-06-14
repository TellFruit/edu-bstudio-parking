using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using CoolParking.BL.Models;
using Newtonsoft.Json;

namespace CoolParking.UI.Console
{
    internal class ApiAccessConsole
    {
        private readonly HttpClient _client;

        public ApiAccessConsole(HttpClient client, string baseAddress)
        {
            _client = client;
        }

        #region ParkingControllerRequests

        public async Task<decimal> GetBalance()
        {
            var response = await _client.GetAsync(Routes.BaseApiAddress + Routes.ParkingBalance);
            var balance = await response.Content.ReadAsStringAsync();
            var reader = new JsonTextReader(new StringReader(balance));
            reader.FloatParseHandling = FloatParseHandling.Decimal;
            Task<decimal?> result = reader.ReadAsDecimalAsync();

            return result.Result ?? -1;
        }

        public async Task<decimal> GetRecentBalance()
        {
            var response = await _client.GetAsync(Routes.BaseApiAddress + Routes.ParkingRecentBalance);
            var balance = await response.Content.ReadAsStringAsync();
            var reader = new JsonTextReader(new StringReader(balance));
            reader.FloatParseHandling = FloatParseHandling.Decimal;
            Task<decimal?> result = reader.ReadAsDecimalAsync();

            return result.Result ?? -1;
        }

        public async Task<int> GetCapacity()
        {
            var response = await _client.GetAsync(Routes.BaseApiAddress + Routes.ParkingCapacity);
            var capacity = await response.Content.ReadAsStringAsync();
            var reader = new JsonTextReader(new StringReader(capacity));

            Task<int?> result = reader.ReadAsInt32Async();

            return result.Result ?? -1;
        }

        public async Task<int> GetFreePlaces()
        {
            var response = await _client.GetAsync(Routes.BaseApiAddress + Routes.ParkingFreePlaces);
            var freePlaces = await response.Content.ReadAsStringAsync();
            var reader = new JsonTextReader(new StringReader(freePlaces));

            Task<int?> result = reader.ReadAsInt32Async();

            return result.Result ?? -1;
        }

        #endregion

        #region VehiclesControllerRequests

        public async Task<ReadOnlyCollection<Vehicle>?> GetVehicles()
        {
            var response = await _client.GetAsync(Routes.BaseApiAddress + Routes.Vehicles);

            var vehicles = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<ReadOnlyCollection<Vehicle>>(vehicles);

            return result;
        }

        public async Task<Tuple<Vehicle?, HttpStatusCode>> GetVehicleById(string id)
        {
            var response = await _client.GetAsync(Routes.BaseApiAddress + Routes.Vehicles + $"/{id}");

            var vehicle = await response.Content.ReadAsStringAsync();

            Vehicle? result = null;

            if (response.StatusCode == HttpStatusCode.OK)
                result = JsonConvert.DeserializeObject<Vehicle>(vehicle);

            return new Tuple<Vehicle?, HttpStatusCode>(result, response.StatusCode);
        }

        public async Task<HttpStatusCode> CreateVehicle(string id, VehicleType vehicleType, decimal balance)
        {
            var json = JsonConvert.SerializeObject(new {id, vehicleType, balance});
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync(Routes.BaseApiAddress + Routes.Vehicles, data);

            var result = response.StatusCode;

            return result;
        }

        public async Task<HttpStatusCode> DeleteVehicle(string id)
        {
            var json = JsonConvert.SerializeObject(id);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.DeleteAsync(Routes.BaseApiAddress + Routes.Vehicles + $"/{id}");

            var result = response.StatusCode;

            return result;
        }

        #endregion
        
        #region TransactionsControllerRequests

        public async Task<ReadOnlyCollection<TransactionInfo>?> GetLastTransactions()
        {
            var response = await _client.GetAsync(Routes.BaseApiAddress + Routes.TransactionsLast);

            var transactions = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<ReadOnlyCollection<TransactionInfo>>(transactions);

            return result;
        }

        public async Task<Tuple<string, HttpStatusCode>> ReadFromLog()
        {
            var response = await _client.GetAsync(Routes.BaseApiAddress + Routes.TransactionsAll);

            string transactions = "";

            if (response.StatusCode == HttpStatusCode.OK)
                transactions = await response.Content.ReadAsStringAsync();

            return new Tuple<string, HttpStatusCode>(transactions, response.StatusCode);
        }

        public async Task<HttpStatusCode> TopUpVehicle(string id, decimal sum)
        {
            var json = JsonConvert.SerializeObject(new {id, sum});
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PutAsync(Routes.BaseApiAddress + Routes.TransactionsTopUp, data);

            var result = response.StatusCode;

            return result;
        }

        #endregion
    }
}
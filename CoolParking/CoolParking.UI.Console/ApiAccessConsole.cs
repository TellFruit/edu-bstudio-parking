﻿using System;
using System.Collections.Generic;
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

        public async Task<decimal> GetBalance()
        {
            var response = await _client.GetAsync(Settings.BaseApiAddress + "/parking" + "/balance");
            var balance = await response.Content.ReadAsStringAsync();
            var reader = new JsonTextReader(new StringReader(balance));
            reader.FloatParseHandling = FloatParseHandling.Decimal;
            Task<decimal?> result = reader.ReadAsDecimalAsync();

            return result.Result ?? -1;
        }

        public async Task<int> GetCapacity()
        {
            var response = await _client.GetAsync(Settings.BaseApiAddress + "/parking" + "/capacity");
            var capacity = await response.Content.ReadAsStringAsync();
            var reader = new JsonTextReader(new StringReader(capacity));

            Task<int?> result = reader.ReadAsInt32Async();

            return result.Result ?? -1;
        }

        public async Task<int> GetFreePlaces()
        {
            var response = await _client.GetAsync(Settings.BaseApiAddress + "/parking" + "/freePlaces");
            var freePlaces = await response.Content.ReadAsStringAsync();
            var reader = new JsonTextReader(new StringReader(freePlaces));

            Task<int?> result = reader.ReadAsInt32Async();

            return result.Result ?? -1;
        }

        public async Task<HttpStatusCode> CreateVehicle(Vehicle vehicle)
        {
            var json = JsonConvert.SerializeObject(vehicle);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync(Settings.BaseApiAddress + "/Vehicles/post", data);

            var result = response.StatusCode;

            return result;
        }

        public async Task<HttpStatusCode> TopUpVehicle(string id, decimal sum)
        {
            var json = JsonConvert.SerializeObject(new { id = id, sum = sum });
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PutAsync(Settings.BaseApiAddress + "/transactions" + "/topUpVehicle", data);

            var result = response.StatusCode;

            return result;
        }
    }
}
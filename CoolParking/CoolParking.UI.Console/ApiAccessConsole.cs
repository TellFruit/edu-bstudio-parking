using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CoolParking.UI.Console
{
    internal class ApiAccessConsole
    {
        private readonly HttpClient _client;

        public ApiAccessConsole(HttpClient client, string baseAddress)
        {
            _client = client;
            _client.BaseAddress = new Uri(baseAddress);
        }

        public async Task<decimal> GetBalance()
        {
            var response = await _client.GetAsync("/parking" + "/balance");
            var balance = await response.Content.ReadAsStringAsync();
            var reader = new JsonTextReader(new StringReader(balance));
            reader.FloatParseHandling = FloatParseHandling.Decimal;
            Task<decimal?> result = reader.ReadAsDecimalAsync();

            return result.Result ?? -1;
        }
    }
}
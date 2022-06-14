using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoolParking.UI.Console
{
    internal class ApiAccessConsole
    {
        private HttpClient _client;

        public ApiAccessConsole(HttpClient client, string baseAddress)
        {
            _client = client;
            _client.BaseAddress = new Uri(baseAddress);
        }
    }
}
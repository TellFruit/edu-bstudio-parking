using Newtonsoft.Json;

namespace CoolParking.WebAPI.Models
{
    public class ApiError
    {
        public int StatusCode { get; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Message { get; private set; }

        public ApiError(int statusCode, string message)
        { 
            StatusCode = statusCode;
            Message = message;
        }
    }
}

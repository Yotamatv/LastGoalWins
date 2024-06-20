using LastGoalWinsServer.Models.MatchModel;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Net.Http;
using System.Text.Json;
using LastGoalWinsServer.Models;
using LastGoalWinsServer.Models.General;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using JsonException = Newtonsoft.Json.JsonException;

namespace LastGoalWinsServer.Services
{
    public class ApiService<T>
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public ApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<string> MakeApiRequest(string RequestUri)
        {
            try
            {
                var client = _httpClientFactory.CreateClient("rapidapi");
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri(RequestUri)
                };
                client.DefaultRequestHeaders.Add("X-RapidAPI-Host", "api-football-v1.p.rapidapi.com");

                using (var response = await client.SendAsync(request))
                {
                    response.EnsureSuccessStatusCode();
                    var contentStream = await response.Content.ReadAsStringAsync();
                    
                    return contentStream;
                }
            }
            catch (HttpRequestException ex)
            {
                // Handle HTTP request exceptions (e.g., network issues, invalid URL, etc.)
                Console.WriteLine($"HTTP request error: {ex.Message}");
                return null; 
            }
            catch (Exception ex)
            {
                // Handle other exceptions (e.g., unexpected errors)
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null; 
            }
        }
        public ResponseModel<T> ConvertToJson(string data)
        {
            try
            {
                // Configure JsonSerializerSettings for case-insensitive property names
                JsonSerializerSettings settings = new JsonSerializerSettings
                {
                    ContractResolver = new DefaultContractResolver
                    {
                        NamingStrategy = new CamelCaseNamingStrategy()
                    },
                };

                // Deserialize the JSON string into a ResponseModel<T> object using Newtonsoft.Json
                Console.WriteLine(data);
                ResponseModel<T>? serializedResult = JsonConvert.DeserializeObject<ResponseModel<T>>(data, settings);
                return serializedResult;
            }
            catch (JsonException jsonEx)
            {
                // Handle JSON-specific exceptions
                Console.WriteLine($"JSON Deserialization Exception: {jsonEx.Message}");
                if (jsonEx.InnerException != null)
                {
                    Console.WriteLine($"Inner Exception: {jsonEx.InnerException.Message}");
                }
                return new ResponseModel<T>(new List<string> { $"Deserialization failed: {jsonEx.Message}" });
            }
            catch (Exception ex)
            {
                // Handle all other exceptions
                Console.WriteLine($"General Exception: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
                }
                return new ResponseModel<T>(new List<string> { $"Error: {ex.Message}" });
            }
        }
        public async Task< ResponseModel<T>> UseAPI(string RequestUri)
        {
            string response = await MakeApiRequest(RequestUri);//Makes get request to the API, returns it as string
            var ParsedResponse = ConvertToJson(response);//Converts the response into the ResponseModel class with JSON
            return ParsedResponse;
        }
    }
}

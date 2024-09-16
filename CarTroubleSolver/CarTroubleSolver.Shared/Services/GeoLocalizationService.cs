using CarTroubleSolver.Shared.Models;
using CarTroubleSolver.Shared.Services.Interface;
using Newtonsoft.Json.Linq;
using System.Globalization;

namespace CarTroubleSolver.Shared.Services
{
    public class GeoLocalizationService : IGeoLocalizationService
    {
        private readonly HttpClient _httpClient;
        public GeoLocalizationService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<(decimal Latitude, decimal Longitude)> GetCurrentGeoLocalization(StreetDto address, CancellationToken cancellationToken)
        {
            var encodedAddress = Uri.EscapeDataString(address.StreetToString());

            var url = $"https://nominatim.openstreetmap.org/search?q={encodedAddress}&format=json&limit=1";

            var requestMessage = new HttpRequestMessage(HttpMethod.Get, url);
            requestMessage.Headers.Add("User-Agent", "CarTroubleSolverApp/1.0");

            var response = await _httpClient.SendAsync(requestMessage, cancellationToken);

            response.EnsureSuccessStatusCode();

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var jsonArray = JArray.Parse(jsonResponse);

            if (jsonArray.Count > 0)
            {
                var location = jsonArray[0];
                decimal latitude = (decimal)location["lat"];
                decimal longitude = (decimal)location["lon"];
                return (latitude, longitude);
            }

            throw new Exception("There is no provided adress");
        }

        public async Task<StreetDto> GetLocalizationDetails(decimal latitude, decimal longitude, CancellationToken cancellationToken)
        {
            var url = $"https://nominatim.openstreetmap.org/reverse.php?lat={latitude.ToString("F6", CultureInfo.InvariantCulture)}&lon={longitude.ToString("F6", CultureInfo.InvariantCulture)}&zoom=18&format=jsonv2";

            var requestMessage = new HttpRequestMessage(HttpMethod.Get, url);
            requestMessage.Headers.Add("User-Agent", "CarTroubleSolverApp/1.0");

            var response = await _httpClient.SendAsync(requestMessage, cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"Request failed with status code {response.StatusCode}: {errorContent}");
            }

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var location = JObject.Parse(jsonResponse);

            var address = location["address"];

            if (address != null)
            {
                var streetDto = new StreetDto
                {
                    StreetName = (string)address["road"] ?? "",
                    StreetNumber = (string)address["house_number"] ?? "",
                    PostalCode = (string)address["postcode"] ?? "",
                    City = (string)address["city"] ?? "",
                    Country = (string)address["country"] ?? "",
                    Province = (string)address["state"] ?? ""
                };

                return streetDto;
            }

            throw new Exception("No address details found for the given coordinates.");
        }
    }
}

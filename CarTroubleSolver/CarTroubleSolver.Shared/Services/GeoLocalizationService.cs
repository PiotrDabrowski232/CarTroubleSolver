using CarTroubleSolver.Shared.Models;
using CarTroubleSolver.Shared.Services.Interface;
using Newtonsoft.Json.Linq;

namespace CarTroubleSolver.Shared.Services
{
    public class GeoLocalizationService : IGeoLocalizationService
    {
        private readonly HttpClient _httpClient;
        public GeoLocalizationService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<(double Latitude, double Longitude)> GetCurrentGeoLocalization(StreetDto address, CancellationToken cancellationToken)
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
                double latitude = (double)location["lat"];
                double longitude = (double)location["lon"];
                return (latitude, longitude);
            }

            throw new Exception("There is no provided adress");
        }

    }
}

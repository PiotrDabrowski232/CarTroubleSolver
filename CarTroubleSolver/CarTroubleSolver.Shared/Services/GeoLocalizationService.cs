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
            // Kodowanie adresu dla URL
            var encodedAddress = Uri.EscapeDataString(address.StreetToString());

            // Tworzymy pełny URL do OpenStreetMap Nominatim API
            var url = $"https://nominatim.openstreetmap.org/search?q={encodedAddress}&format=json&limit=1";

            // Dodanie nagłówka User-Agent do żądania HTTP
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, url);
            requestMessage.Headers.Add("User-Agent", "CarTroubleSolverApp/1.0");

            // Wysyłamy zapytanie HTTP do API
            var response = await _httpClient.SendAsync(requestMessage, cancellationToken);

            // Sprawdzamy, czy odpowiedź zakończyła się sukcesem
            response.EnsureSuccessStatusCode();

            // Odczytujemy odpowiedź jako string i parsujemy do JSON-a
            var jsonResponse = await response.Content.ReadAsStringAsync();
            var jsonArray = JArray.Parse(jsonResponse);

            // Sprawdzamy, czy znaleziono jakiekolwiek wyniki
            if (jsonArray.Count > 0)
            {
                var location = jsonArray[0];
                double latitude = (double)location["lat"];
                double longitude = (double)location["lon"];
                return (latitude, longitude);
            }

            // W przypadku braku wyników, rzucamy wyjątek
            throw new Exception("Nie można znaleźć współrzędnych dla podanego adresu.");
        }

    }
}

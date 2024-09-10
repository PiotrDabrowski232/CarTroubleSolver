using CarTroubleSolver.Shared.Models;

namespace CarTroubleSolver.Shared.Services.Interface
{
    public interface IGeoLocalizationService
    {
        public Task<(double Latitude, double Longitude)> GetCurrentGeoLocalization(StreetDto address, CancellationToken cancellationToken);
    }
}

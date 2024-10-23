using CarTroubleSolver.Shared.Models.ExtraModels;

namespace CarTroubleSolver.Shared.Services.Interface
{
    public interface IGeoLocalizationService
    {
        public Task<(decimal Latitude, decimal Longitude)> GetCurrentGeoLocalization(StreetDto address, CancellationToken cancellationToken);
        public Task<StreetDto> GetLocalizationDetails(double? latitude, double? longitude, CancellationToken cancellationToken);
    }
}

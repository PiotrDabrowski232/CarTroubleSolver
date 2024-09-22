using CarTroubleSolver.Logic.Dto.Car;
using CarTroubleSolver.Logic.Dto.File;

namespace CarTroubleSolver.Logic.Services.Interfaces
{
    public interface IFileService
    {
        Task<string> SaveFileAsync(CarPhotoDto image);
        void DeleteFile(string fileNameWithExtension);
        Task UpdateFilePath(long currentVin, string currentPath);
        FileDto Download(long vin);
    }
}

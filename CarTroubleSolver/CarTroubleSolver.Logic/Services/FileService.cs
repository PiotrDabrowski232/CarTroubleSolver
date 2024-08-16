using CarTroubleSolver.Data.Repositories.Interfaces;
using CarTroubleSolver.Logic.Consts;
using CarTroubleSolver.Logic.Dto.Car;
using CarTroubleSolver.Logic.Dto.File;
using CarTroubleSolver.Logic.Services.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace CarTroubleSolver.Logic.Services
{
    public class FileService(IWebHostEnvironment _environment, ICarRepository _carRepository) : IFileService
    {
        public async Task<string> SaveFileAsync(CarPhotoDto image)
        {
            if (image.Photo == null)
            {
                throw new ArgumentNullException(nameof(image.Photo));
            }

            var contentPath = _environment.ContentRootPath;
            var path = Path.Combine(contentPath, "wwwroot\\Images\\Car\\");

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            var ext = Path.GetExtension(image.Photo.FileName);
            if (!AllowedFormats.Formats.Contains(ext))
            {
                throw new ArgumentException($"Only {string.Join(",", AllowedFormats.Formats)} are allowed.");
            }

            var fileName = $"{image.Vin}{ext}";
            var fileNameWithPath = Path.Combine(path, fileName);
            using var stream = new FileStream(fileNameWithPath, FileMode.Create);
            await image.Photo.CopyToAsync(stream);
            await _carRepository.UpdateImagePath(long.Parse(image.Vin), fileNameWithPath);
            return fileName;
        }


        public void DeleteFile(string fileNameWithExtension)
        {
            if (string.IsNullOrEmpty(fileNameWithExtension))
            {
                throw new ArgumentNullException(nameof(fileNameWithExtension));
            }
            var contentPath = _environment.ContentRootPath;
            var path = Path.Combine(contentPath, $"Uploads", fileNameWithExtension);

            if (!File.Exists(path))
            {
                throw new FileNotFoundException($"Invalid file path");
            }
            File.Delete(path);
        }

        public async Task UpdateFilePath(long CurrentVin, string CurrentPath)
        {
            if (File.Exists(CurrentPath))
            {
                string newFileName = $"{CurrentVin}{Path.GetExtension(CurrentPath)}";
                string newPath = Path.Combine(Path.GetDirectoryName(CurrentPath), newFileName);

                File.Copy(CurrentPath, newPath);

                File.Delete(CurrentPath);

                await _carRepository.UpdateImagePath(CurrentVin, newPath);

                await Task.CompletedTask;
            }
        }

        public FileDto Download(long vin)
        {
            var filePath = _carRepository.GetAll().FirstOrDefault(x => x.VIN == vin)?.ImagePath;

            if (string.IsNullOrEmpty(filePath) || !File.Exists(filePath))
            {
                throw new NotSupportedException("File not found.");
            }

            var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            return new FileDto(fileStream, $"image/{Path.GetExtension(filePath).ToLowerInvariant().Trim('.')}", Path.GetFileName(filePath));
        }
    }
}


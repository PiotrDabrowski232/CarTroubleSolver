using Microsoft.AspNetCore.Http;

namespace CarTroubleSolver.Logic.Dto.Car
{
    public class CarPhotoDto
    {
        public string Vin { get; set; }
        public IFormFile Photo { get; set; }
    }
}

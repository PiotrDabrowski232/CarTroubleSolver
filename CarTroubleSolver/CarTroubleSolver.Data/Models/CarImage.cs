using Microsoft.AspNetCore.Http;

namespace CarTroubleSolver.Data.Models
{
    public class CarImage
    {
        public string Vin { get; set; }
        public IFormFile Image { get; set; }
    }
}

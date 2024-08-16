using CarTroubleSolver.Logic.Dto.Car;
using CarTroubleSolver.Logic.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace CarTroubleSolver.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class FileController(IFileService _fileService) : ControllerBase
    {

        [HttpPost]
        [Route("/AddFile")]
        public async Task<IActionResult> AddCarPhoto([FromForm] CarPhotoDto carPhoto)
        {
            try
            {
                if (carPhoto.Photo?.Length > 15 * 1024 * 1024)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "File size should not exceed 15 MB");
                }

                await _fileService.SaveFileAsync(carPhoto);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("/DownloadFile")]
        public IActionResult DownloadFile(long vin)
        {
            try
            {
                var file = _fileService.Download(vin);

                return File(file.FileStream, file.ContentType, file.FileName);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}

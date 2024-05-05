using CarTroubleSolver.Logic.Services;
using CarTroubleSolver.Logic.Services.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CarTroubleSolver.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController(IMediator mediator, ICarService carService) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;
        private readonly ICarService _carService = carService;

        [HttpGet]
        public async Task<IActionResult> GetCarSpecification()
        {
            var result = _mediator.Send("asdas");
            return Ok(result);
        }
    }
}

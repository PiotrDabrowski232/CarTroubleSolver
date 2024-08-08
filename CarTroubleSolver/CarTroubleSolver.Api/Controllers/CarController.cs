using CarTroubleSolver.Logic.Dto.Car;
using CarTroubleSolver.Logic.Functions.Car.Command;
using CarTroubleSolver.Logic.Functions.Car.Query;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarTroubleSolver.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CarController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpGet]
        [Route("/Brands")]
        public async Task<IActionResult> GetCarSpecification()
        {
            try
            {
                var result = _mediator.Send(new GetBasicCarConfigQuery());
                return Ok(result);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("/Models")]
        public async Task<IActionResult> GetCarModels([FromQuery] string Brand)
        {
            try
            {
                var result = _mediator.Send(new GetBrandModelsQuery(Brand));
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("/AddCar")]
        [Authorize]
        public IActionResult Add([FromBody] CarDto Car)
        {
            try
            {
                var result = _mediator.Send(new AddCarCommand(Car));
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("/Delete")]
        [Authorize]
        public IActionResult Delete([FromQuery] string VIN)
        {
            try
            {
                var result = _mediator.Send(new RemoveCarCommand(VIN));
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

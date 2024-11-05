using CarTroubleSolver.Logic.Functions.Accident;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CarTroubleSolver.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccidentController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;


        [HttpPost]
        [Route("/AddAccident")]
        public async Task<IActionResult> AddAccident([FromQuery] string id, [FromBody] bool isAccepted)
        {
            try
            {
                var result = await _mediator.Send(new AddAccidentCommand(id, isAccepted));
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("/GetCarAccidents")]
        public async Task<IActionResult> GetCarAccidents([FromQuery] long carVin)
        {
            try
            {
                var result = await _mediator.Send(new GetCarAccidentsQuery(carVin));
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}

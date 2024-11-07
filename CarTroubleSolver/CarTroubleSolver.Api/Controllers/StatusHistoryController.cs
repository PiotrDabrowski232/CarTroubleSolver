using CarTroubleSolver.Logic.Functions.StausHistory;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CarTroubleSolver.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusHistoryController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpGet]
        [Route("/StatusHistory")]
        public async Task<IActionResult> StatusHistory([FromQuery] string accidentId)
        {
            try
            {
                var result = await _mediator.Send(new GetStatusHistoryQuery(accidentId));
                return Ok(result);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

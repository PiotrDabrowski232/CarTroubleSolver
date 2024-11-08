using CarTroubleSolver.Logic.Functions.RepairHistory;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CarTroubleSolver.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RepairHistoryController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpGet]
        [Route("/RepairsHitories")]
        public async Task<IActionResult> RepairsHitories([FromQuery] string carVin)
        {
            try
            {
                var result = await _mediator.Send(new GetCarRepairsHitoriesQuery(carVin));
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

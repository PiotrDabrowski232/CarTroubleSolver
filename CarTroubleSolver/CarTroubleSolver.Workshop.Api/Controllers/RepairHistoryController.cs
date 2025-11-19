using CarTroubleSolver.Workshop.Logic.Dto;
using CarTroubleSolver.Workshop.Logic.Functions.Repairs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CarTroubleSolver.Workshop.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RepairHistoryController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpGet]
        [Route("/GetRepairHistory")]
        public async Task<IActionResult> GetRepairHistory([FromQuery] string accidentId)
        {
            try
            {
                var result = await _mediator.Send(new GetRepairHistoryQuery(accidentId));
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("/UpdateRepairs")]
        public async Task<IActionResult> UpdateRepairs([FromQuery] string accidentId, [FromBody] RepairHistoryDto repairs)
        {
            try
            {
                var result = await _mediator.Send(new UpdateRepairHistoryCommand(accidentId, repairs));
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

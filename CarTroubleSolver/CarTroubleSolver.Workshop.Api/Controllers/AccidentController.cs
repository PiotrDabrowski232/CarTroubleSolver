using CarTroubleSolver.Workshop.Logic.Dto;
using CarTroubleSolver.Workshop.Logic.Functions.Accident;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarTroubleSolver.Workshop.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccidentController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpGet]
        [Route("/Accidents")]
        [Authorize]
        public async Task<IActionResult> Accidents()
        {
            try
            {
                var result = await _mediator.Send(new GetAccidentsQuery());
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("/AccidentFullInfo")]
        public async Task<IActionResult> AccidentFullInfo([FromQuery] string accidentId)
        {
            try
            {
                var result = await _mediator.Send(new GetAccidentFullInfoQuery(accidentId));
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("/AccidentServiceDetails")]
        public async Task<IActionResult> AccidentServiceDetails([FromQuery] string accidentId)
        {
            try
            {
                var result = await _mediator.Send(new AccidentServiceDetailsQuery(accidentId));
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("/ChangeAccidentStatus")]
        public async Task<IActionResult> ChangeAccidentStatus([FromQuery] string accidentId, [FromBody] RepairHistoryDto? repairs)
        {
            try
            {
                var result = await _mediator.Send(new ChangeAccidentStatusCommand(accidentId, repairs));
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

using CarTroubleSolver.Workshop.Logic.Dto.Workshop;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using CarTroubleSolver.Workshop.Logic.Functions.Workshop.Command;
using CarTroubleSolver.Workshop.Logic.Functions.Workshop.Query;
using CarTroubleSolver.Shared.Exceptions;
using CarTroubleSolver.Workshop.Logic.Functions.Hour.Command;
using CarTroubleSolver.Workshop.Logic.Dto.Hour;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Authorization;

namespace CarTroubleSolver.Workshop.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class WorkshopController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;


        [HttpPost]
        [Route("/Register")]
        public ActionResult Register([FromBody] RegisterWorkshopDto workshop)
        {
            try
            {
                var result = _mediator.Send(new RegiserWorkshopCommand(workshop));
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("/Login")]
        public async Task<ActionResult> Login([FromBody] LoginWorkshopDto workshop)
        {
            try
            {
                var result = await _mediator.Send(new LoginQuery(workshop));
                return Ok(result);
            }
            catch (InvalidProvidedDataException ex)
            {
                return BadRequest(ex.Message); 
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An unexpected error occurred: " + ex.Message);  
            }
        }

        [HttpPost]
        [Route("/HoursConfiguration")]
        public async Task<ActionResult> HoursConfiguration([FromBody] IList<WorkingHoursDto> hours)
        {
            try
            {
                var result = await _mediator.Send(new HourConfigurationCommand(hours));
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An unexpected error occurred: " + ex.Message);
            }
        }

        [HttpPut]
        [Authorize]
        [Route("/ResetPassword")]
        public async Task<ActionResult> ResetPassword([FromBody] NewPasswordDto password)
        {
            try
            {
                var result = await _mediator.Send(new ResetPasswordCommand(password));
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}

using CarTroubleSolver.Workshop.Logic.Dto.Workshop;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using CarTroubleSolver.Workshop.Logic.Functions.Workshop.Command;

namespace CarTroubleSolver.Workshop.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class WorkshopController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;


        [HttpPost]
        [Route("/Register")]
        public ActionResult Post([FromBody] RegisterWorkshopDto workshop)
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
    }
}

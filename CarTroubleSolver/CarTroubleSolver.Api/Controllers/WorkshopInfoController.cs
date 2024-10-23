using CarTroubleSolver.Logic.Dto.Car;
using CarTroubleSolver.Logic.Functions.Workshop;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CarTroubleSolver.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkshopInfoController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpGet]
        [Route("/Workshops")]
        public async Task<IActionResult> Worskhops()
        {
            try
            {
                var result = await _mediator.Send(new WorkshopInfoQuery());

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

using CarTroubleSolver.Workshop.Logic.Dto.Service;
using CarTroubleSolver.Workshop.Logic.Functions.Services;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarTroubleSolver.Workshop.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ServiceController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpGet]
        [Route("/ServiceType")]
        public async Task<IActionResult> GetServices()
        {
            try
            {
                var result = await _mediator.Send(new GetServiceTypesQuery());
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPost]
        [Route("/AddServices")]
        public async Task<IActionResult> AddServices([FromQuery] string id, [FromBody] IEnumerable<ServiceDto> services)
        {
            try
            {
                var result = await _mediator.Send(new AddServicesForWorkshopCommand(Guid.Parse(id), services));
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpGet]
        [Route("/GetServices")]
        public async Task<IActionResult> GetServices([FromQuery] string id)
        {
            try
            {
                var result = await _mediator.Send(new GetWorkshopServicesQuery(Guid.Parse(id)));
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
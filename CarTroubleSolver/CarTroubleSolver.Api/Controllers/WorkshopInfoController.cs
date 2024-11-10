using CarTroubleSolver.Logic.Dto;
using CarTroubleSolver.Logic.Dto.Car;
using CarTroubleSolver.Logic.Functions.Workshop;
using CarTroubleSolver.Shared.Models.ExtraModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace CarTroubleSolver.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkshopInfoController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpGet]
        [Route("/Workshops")]
        public async Task<IActionResult> Worskhops([FromQuery] GeoDto? geo)
        {
            try
            {
                var result = await _mediator.Send(new WorkshopInfoQuery(geo));

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("/GetWorkshop")]
        public async Task<IActionResult> GetWorkshop([FromQuery] string id)
        {
            try
            {
                var result = await _mediator.Send(new WorkshopDetailsQuery(id));

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

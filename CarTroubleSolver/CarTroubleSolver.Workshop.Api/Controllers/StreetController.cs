using CarTroubleSolver.Workshop.Logic.Functions.Street.Query;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CarTroubleSolver.Workshop.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class StreetController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpGet]
        [Route("/Provinces")]
        public async Task<IActionResult> GetProvinces()
        {
            try
            {
                var result = await _mediator.Send(new GetAllProvincesQuery());
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

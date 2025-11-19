using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarTroubleSolver.Workshop.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatingController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpGet]
        [Route("/Ratings")]
        [Authorize]
        public async Task<IActionResult> Ratings()
        {
            try
            {
                var result = await _mediator.Send(new Logic.Functions.Rating.GetRatingsQuery());
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

using CarTroubleSolver.Workshop.Logic.Dto.Message;
using CarTroubleSolver.Workshop.Logic.Functions.Message;
using CarTroubleSolver.Workshop.Logic.Functions.Services;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarTroubleSolver.Workshop.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpGet]
        [Route("/ReceiveMessage")]
        [Authorize]
        public async Task<IActionResult> ReceiveMessage()
        {
            try
            {
                var result = await _mediator.Send(new ReceiveMessageQuery());
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("/FullMessage")]
        public async Task<IActionResult> FullMessage([FromQuery] string id)
        {
            try
            {
                var result = await _mediator.Send(new ReceiveMessageFullInfoQuery(Guid.Parse(id)));
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("/SetRead")]
        public async Task<IActionResult> SetRead([FromQuery] string id)
        {
            try
            {
                var result = await _mediator.Send(new SetReadCommand(Guid.Parse(id)));
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        [HttpPost]
        [Route("/SendMessage")]
        public async Task<IActionResult> SendMessage([FromQuery] string id, [FromBody] SendMessageDto message)
        {
            try
            {
                var result = await _mediator.Send(new SendMessageCommand(message, id));
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

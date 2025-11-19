using CarTroubleSolver.Logic.Dto;
using CarTroubleSolver.Logic.Functions.Message;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarTroubleSolver.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpPost]
        [Route("/SendMessage")]
        [Authorize]
        public async Task<IActionResult> SendMessage([FromBody] MessageDto message)
        {
            try
            {
                var result = await _mediator.Send(new SendMessageCommand(message));
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

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
        [Route("/ReceiveMessageDetails")]
        public async Task<IActionResult> ReceiveMessageDetails([FromQuery] string id)
        {
            try
            {
                var result = await _mediator.Send(new ReceiveMessageDetailsQuery(id));
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("/SetMessageRead")]
        public async Task<IActionResult> SetMessageRead([FromQuery] string id)
        {
            try
            {
                var result = await _mediator.Send(new SetUserMessageReadCommand(id));
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

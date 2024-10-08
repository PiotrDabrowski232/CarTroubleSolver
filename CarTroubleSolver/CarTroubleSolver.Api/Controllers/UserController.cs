﻿using CarTroubleSolver.Logic.Dto.User;
using CarTroubleSolver.Logic.Functions.User.Querry;
using CarTroubleSolver.Logic.Services.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace CarTroubleSolver.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController(IMediator mediator, IUserService userService) : ControllerBase
    {
        public IUserService _userService = userService;
        private readonly IMediator _mediator = mediator;


        [HttpGet]
        [Route("/GetAllUsers")]
        [Authorize]
        public ActionResult<IEnumerable<UserDto>> GetAll()
        {
            try
            {
                var users = _userService.GetUsers();
                return users.Any() ? Ok(users) : NotFound("There is no Users");
            }
            catch
            {
                return NotFound("There is no Users");
            }
        }

        [HttpGet]
        [Route("/User")]
        [Authorize]
        public ActionResult<UserDto> Get()
        {
            try
            {
                var user = _mediator.Send(new GetUserInfoQuerry());
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("/Register")]
        public ActionResult<UserDto> Post([FromBody] RegisterUserDto registerUser)
        {
            try
            {
                var userResult = _userService.RegisterUser(registerUser);
                return Ok(userResult);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("/ChangePassword")]
        [Authorize]
        public ActionResult ChangePassword([FromBody] ChangePasswordUserDto user)
        {
            try
            {
                _userService.ChangePassword(user);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Authorize(Roles = "Admin, BasicUser")]
        [Route("/RemoveAccount")]
        public ActionResult Delete(string id)
        {
            try
            {
                _userService.DeleteUser(Guid.Parse(id));
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("/Login")]
        public ActionResult Login([FromBody] LoginDto login)
        {
            try
            {
                string token = _userService.GenerateJwt(login);
                return Ok(token);
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}

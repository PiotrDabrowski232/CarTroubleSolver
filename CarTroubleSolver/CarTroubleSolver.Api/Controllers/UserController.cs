using CarTroubleSolver.Data.Models;
using CarTroubleSolver.Logic.Dto.User;
using CarTroubleSolver.Logic.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace CarTroubleSolver.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController(IUserService userService) : ControllerBase
    {
        public IUserService _userService = userService;

        [HttpGet]
        [Route("/GetAllUsers")]
        public ActionResult<IEnumerable<UserDto>> GetAll()
        {
            try
            {
                var users = _userService.GetUsers();
                return users.Any() ? Ok(users) : NotFound("There is no Users");
            }
            catch (Exception ex)
            {
                return NotFound("There is no Users");
            }
        }

        [HttpGet]
        [Route("/User")]
        public ActionResult<UserDto> Get([FromQuery]Guid id)
        {
            try 
            { 
            var user = _userService.GetUser(id);
            return user != null ?  Ok(user) : NotFound("There is no User with provide id");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("/Register")]
        public ActionResult Post([FromBody] RegisterUserDto registerUser)
        {
            try
            {
                _userService.RegisterUser(registerUser);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("/ChangePassword")]
        public ActionResult Post2([FromBody] ChangePasswordUserDto user)
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

        [HttpPut]
        [Route("/UpdateUserData")]
        public ActionResult Put([FromBody] UpdateUserDto user)
        {
            try
            {
                _userService.UpdateUserData(user);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("/RemoveAccount")]
        public ActionResult Delete(Guid id)
        {
            try
            {
                _userService.DeleteUser(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

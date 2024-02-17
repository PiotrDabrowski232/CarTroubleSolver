using CarTroubleSolver.Logic.Dto.User;
using CarTroubleSolver.Logic.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace CarTroubleSolver.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public IUserService _userService;
        public UserController(IUserService userService) 
        {
            _userService = userService;
        }


        // GET: api/<UserController>
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

        // GET api/<UserController>/5
        [HttpGet]
        [Route("/User")]
        public ActionResult<UserDto> Get([FromQuery]Guid id)
        {
            var user = _userService.GetUser(id);
            return user != null ?  Ok(user) : NotFound("There is no User with provide id");
        }

        // POST api/<UserController>
        [HttpPost]
        [Route("/Register")]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<UserController>/5
        [HttpPut]
        [Route("/UpdateUserData")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UserController>/5
        [HttpDelete]
        [Route("/RemoveAccount")]
        public void Delete(int id)
        {
        }
    }
}

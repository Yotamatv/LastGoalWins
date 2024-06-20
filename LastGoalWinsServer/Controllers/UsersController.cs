using LastGoalWinsServer.Auth;
using LastGoalWinsServer.Exceptions;
using LastGoalWinsServer.Models.Login;
using LastGoalWinsServer.Models.SQLModels;
using LastGoalWinsServer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LastGoalWinsServer.Controllers
{
        [ApiController]
        [Route("[controller]")]
    public class UsersController : ControllerBase
    {
       private readonly UsersService _usersService;
        public UsersController(UsersService usersService)
        {
            _usersService = usersService;
        }
        // GET: /<UsersController>
        [HttpGet]
        [Authorize(Policy = "MustBeAdmin")]
        public async Task<IActionResult> Get()
        {
            List<UserSql> users = await _usersService.GetUsersAsync();
            return Ok(users);
        }


        // GET /<UsersController>/5
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            bool.TryParse(HttpContext.User.FindFirstValue("isAdmin"), out bool isAdmin);
            bool isMyId = HttpContext.User.FindFirstValue("Id") == id;
            if (!isAdmin && !isMyId)
            {
                return Unauthorized("You can watch only your own profile");
            }

            try
            {
                UserSql? u = await _usersService.GetOneUserAsync(id);
                return Ok(u);
            }
            catch (UserNotFoundException e)
            {
                return NotFound(e.Message);
            }

        }

        // POST /<UsersController>
        [HttpPost("signup")]

        public async Task<IActionResult> Post([FromBody] UserSql newUser)
        {
            Console.WriteLine("dude wtf");
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                object DTOuser = await _usersService.CreateUserAsync(newUser);
                return CreatedAtAction(nameof(Get), new { Id = newUser.Id }, DTOuser);

            }
            catch (UserAlreadyExistsException ex)
            {
                return Conflict(ex.Message);
            }


        }

        // PUT /<UsersController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] UserSql updatedUser)
        {
            bool.TryParse(HttpContext.User.FindFirstValue("isAdmin"), out bool isAdmin);
            bool isMyId = HttpContext.User.FindFirstValue("id") == id;
            if (!isAdmin && !isMyId)
            {
                return Unauthorized("You can watch only your own profile");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                UserSql newUser = await _usersService.EditUserAsync(id, updatedUser);
            }
            catch (UserNotFoundException e)
            {
                return NotFound(e.Message);
            }
            return NoContent();
        }

        // DELETE /<UsersController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            bool.TryParse(HttpContext.User.FindFirstValue("isAdmin"), out bool isAdmin);
            bool isMyId = HttpContext.User.FindFirstValue("id") == id;
            if (!isAdmin && !isMyId)
            {
                return Unauthorized("You can watch only your own profile");
            }

            try
            {
                await _usersService.DeleteUserAsync(id);
            }
            catch (UserNotFoundException e)
            {
                return NotFound(e.Message);
            }

            return NoContent();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                UserSql? u = await _usersService.LoginAsync(loginModel);
                string token = JwtHelper.GenerateAuthToken(u);
                return Ok(token);

            }
            catch (AuthenticationException ex)
            {
                return Unauthorized("Email or Password wrong");

            }


        }
    }
}

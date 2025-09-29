using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using API.Infrastructure;
namespace ManyToManyRelation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserService _userService;
        public UsersController(UserService userService)
        {
            _userService = userService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllUsers();
            return Ok(users);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _userService.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody] API.Domain.User user)
        {
            var newUser = await _userService.AddUser(user);
            return CreatedAtAction(nameof(GetUserById), new { id = newUser.UserId }, newUser);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] API.Domain.User user)
        {
            if (id != user.UserId)
            {
                return BadRequest("Route ID and body ID do not match.");
            }

            var result = await _userService.Update(user);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }



    }
}

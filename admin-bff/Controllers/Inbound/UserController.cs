using admin_bff.Dtos;
using admin_bff.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace admin_bff.Controllers.Inbound
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class UserController : Controller
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        // Save user from ID token
        [HttpPost]
        public async Task<IActionResult> SaveUser([FromQuery] string idToken)
        {
            var response = await _userService.SaveUserAsync(idToken);

            if (response.Success)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }

        // Update user
        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromBody] UserDto userDto)
        {
            var response = await _userService.UpdateUserAsync(userDto);

            if (response.Success)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }

        // Find all users by role
        [HttpGet("ByRole")]
        public async Task<IActionResult> FindAllUsersByRole([FromQuery] string role)
        {
            var response = await _userService.FindAllUsersByRoleAsync(role);

            if (response.Success)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }

        // Find user by ID
        [HttpGet("{id}")]
        public async Task<IActionResult> FindUserById([FromRoute] Guid id)
        {
            var response = await _userService.FindUserByIdAsync(id);

            if (response.Success)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }
    }
}

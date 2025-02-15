using Microsoft.AspNetCore.Mvc;
using SPCManagementSystemAPI.Models;
using SPCManagementSystemAPI.Repositories;
using System.Threading.Tasks;

namespace SPCManagementSystemAPI.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public AuthController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(User user)
        {
            var newUser = await _userRepository.Register(user);
            return Ok(new { message = "User registered successfully", userId = newUser.Id });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            // Find the user by email
            var user = await _userRepository.Login(loginRequest.Email, loginRequest.Password);

            // If user is not found or credentials are incorrect
            if (user == null)
            {
                return Unauthorized(new { message = "Invalid credentials." });
            }

            // Return the user details (Name, Role, Id) without sending the password
            return Ok(new
            {
                id = user.Id,
                name = user.Name,
                role = user.Role
            });
        }
    }
}

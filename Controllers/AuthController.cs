using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SPCManagementSystemAPI.Models;
using SPCManagementSystemAPI.Repositories;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
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

        // Register User (Signup)
        [HttpPost("register")]
        public async Task<IActionResult> Register(User user)
        {
            var newUser = await _userRepository.Register(user);
            return Ok(new { message = "User registered successfully", userId = newUser.Id });
        }

        // Login User
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var user = await _userRepository.Login(request.Email, request.Password);
            if (user == null)
            {
                return Unauthorized(new { message = "Invalid credentials" });
            }

            var tokenHandler = new JwtSecurityTokenHandler();

            // Use a secure key (256-bit key is required for HMAC SHA-256)
            var key = Encoding.ASCII.GetBytes("adsd3dsdvcdjfldnflsdncdljsnfljfndsnvdsjvnfkjndlvnfjnvljnvlfnvlfvdyowxkkwnkckkdjjdkkdkjdjdcjjdkk"); // Ensure this key is at least 32 bytes

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {
            new Claim(ClaimTypes.Name, user.Id.ToString()),
            new Claim(ClaimTypes.Role, user.Role)
        }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return Ok(new { token = tokenHandler.WriteToken(token), role = user.Role });
        }

    }
}

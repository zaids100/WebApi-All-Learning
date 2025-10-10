using Microsoft.AspNetCore.Mvc;
using Playlist_Manager.DTOs.User;
using Playlist_Manager.Interfaces;
using Playlist_Manager.Models;
using Playlist_Manager.Services;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Playlist_Manager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUser _userRepository;
        private readonly IToken _tokenService;

        public AuthController(IUser userRepository, IToken tokenService)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var emailExists = await _userRepository.IsEmailExistsAsync(dto.Email);
            if (emailExists)
                return Conflict(new { message = "Email already exists." });

            var usernameExists = await _userRepository.IsUsernameExistsAsync(dto.Username);
            if (usernameExists)
                return Conflict(new { message = "Username already taken." });

            var hashedPassword = HashPassword(dto.Password);
            var newUser = new User
            {
                Username = dto.Username,
                Email = dto.Email,
                PasswordHash = hashedPassword,
                ProfileImage = dto.ProfileImage,
                Role = dto.Role
            };

            var createdUser = await _userRepository.CreateUserAsync(newUser);

            var (token, expiresAt) = await _tokenService.GenerateAccessTokenAsync(createdUser);

            return Ok(new
            {
                user = new
                {
                    createdUser.Id,
                    createdUser.Username,
                    createdUser.Email,
                    createdUser.Role
                },
                accessToken = token,
                expiresAt
            });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var hashedPassword = HashPassword(dto.Password);
            var user = await _userRepository.ValidateUserCredentialsAsync(dto.Email, hashedPassword);

            if (user == null)
                return Unauthorized(new { message = "Invalid email or password." });

            var (token, expiresAt) = await _tokenService.GenerateAccessTokenAsync(user);

            return Ok(new
            {
                user = new
                {
                    user.Id,
                    user.Username,
                    user.Email,
                    user.Role
                },
                accessToken = token,
                expiresAt
            });
        }

        private static string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(password);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }
    }
}

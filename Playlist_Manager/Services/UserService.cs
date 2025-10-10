using Playlist_Manager.DTOs;
using Playlist_Manager.DTOs.User;
using Playlist_Manager.Interfaces;
using Playlist_Manager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Playlist_Manager.Services
{
    public class UserService
    {
        private readonly IUser _userRepository;

        public UserService(IUser userRepository)
        {
            _userRepository = userRepository;
        }

        //  Register a new user
        public async Task<UserResponseDto?> RegisterUserAsync(UserRegisterDto dto)
        {
           
            if (await _userRepository.IsEmailExistsAsync(dto.Email))
                throw new Exception("Email already registered.");

            if (await _userRepository.IsUsernameExistsAsync(dto.Username))
                throw new Exception("Username already taken.");

            
            string hashedPassword = HashPassword(dto.Password);

            
            var newUser = new User
            {
                Username = dto.Username,
                Email = dto.Email,
                PasswordHash = hashedPassword,
                Role = dto.Role,
                ProfileImage = dto.ProfileImage,
                CreatedAt = DateTime.UtcNow
            };

            
            var createdUser = await _userRepository.CreateUserAsync(newUser);

            return new UserResponseDto
            {
                Id = createdUser.Id,
                Username = createdUser.Username,
                Email = createdUser.Email,
                Role = createdUser.Role,
                CreatedAt = createdUser.CreatedAt,
                ProfileImage = createdUser.ProfileImage
            };
        }

       
        public async Task<UserResponseDto?> LoginUserAsync(UserLoginDto dto)
        {
            var hashedPassword = HashPassword(dto.Password);
            var user = await _userRepository.ValidateUserCredentialsAsync(dto.Email, hashedPassword);

            if (user == null)
                throw new Exception("Invalid email or password.");

            return new UserResponseDto
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                Role = user.Role,
                CreatedAt = user.CreatedAt,
                ProfileImage = user.ProfileImage
            };
        }

       
        public async Task<UserResponseDto?> GetUserByIdAsync(int id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            if (user == null) return null;

            return new UserResponseDto
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                Role = user.Role,
                CreatedAt = user.CreatedAt,
                ProfileImage = user.ProfileImage
            };
        }

       
        public async Task<IEnumerable<UserResponseDto>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllUsersAsync();
            return users.Select(u => new UserResponseDto
            {
                Id = u.Id,
                Username = u.Username,
                Email = u.Email,
                Role = u.Role,
                CreatedAt = u.CreatedAt,
                ProfileImage = u.ProfileImage
            });
        }

        
        public async Task<bool> UpdateUserAsync(int id, UserUpdateDto dto)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            if (user == null)
                throw new Exception("User not found.");

            user.Username = dto.Username ?? user.Username;
            user.Email = dto.Email ?? user.Email;
            user.ProfileImage = dto.ProfileImage ?? user.ProfileImage;

            return await _userRepository.UpdateUserAsync(user);
        }

        
        public async Task<bool> DeleteUserAsync(int id)
        {
            return await _userRepository.DeleteUserAsync(id);
        }

       
        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(bytes);
        }
    }
}

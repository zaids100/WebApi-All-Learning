using Playlist_Manager.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Playlist_Manager.Interfaces
{
    public interface IUser
    {
        //  Create a new user (during registration)
        Task<User> CreateUserAsync(User user);

        //  Get a user by their unique ID
        Task<User?> GetUserByIdAsync(int id);

        //  Get a user by email (useful for login)
        Task<User?> GetUserByEmailAsync(string email);

        //  Get a user by username (for profile checks)
        Task<User?> GetUserByUsernameAsync(string username);

        //  Get all users (admin-only)
        Task<IEnumerable<User>> GetAllUsersAsync();

        //  Update user details (profile update, password change, etc.)
        Task<bool> UpdateUserAsync(User user);

        //  Delete a user by ID
        Task<bool> DeleteUserAsync(int id);

        //  Verify login credentials (email + password hash check)
        Task<User?> ValidateUserCredentialsAsync(string email, string passwordHash);

        //  Check if an email is already registered
        Task<bool> IsEmailExistsAsync(string email);

        //  Check if a username is already taken
        Task<bool> IsUsernameExistsAsync(string username);
    }
}

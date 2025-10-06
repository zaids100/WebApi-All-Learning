using Microsoft.EntityFrameworkCore;
using Playlist_Manager.Interfaces;
using Playlist_Manager.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Playlist_Manager.Repositories
{
    public class UserRepository : IUser
    {
        private readonly PlaylistManagerContext _context;

        public UserRepository(PlaylistManagerContext context)
        {
            _context = context;
        }

        //  Create a new user
        public async Task<User> CreateUserAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<bool> UpdateUserAsync(User user)
        {
            var existingUser = await _context.Users.FindAsync(user.Id);
            if (existingUser == null)
                return false;

            // Update only non-null or changed fields
            existingUser.Username = user.Username;
            existingUser.Email = user.Email;
            existingUser.ProfileImage = user.ProfileImage ?? existingUser.ProfileImage;
            existingUser.Role = user.Role;

            _context.Users.Update(existingUser);
            await _context.SaveChangesAsync();
            return true;
        }



        //  Get user by ID
        public async Task<User?> GetUserByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        //  Get user by Email
        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower());
        }

        //  Get user by Username
        public async Task<User?> GetUserByUsernameAsync(string username)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Username.ToLower() == username.ToLower());
        }

        // Get all users (for admin)
        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _context.Users.AsNoTracking().ToListAsync();
        }

        //  Delete user by ID
        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return false;

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }

        //  Validate login credentials
        public async Task<User?> ValidateUserCredentialsAsync(string email, string passwordHash)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u =>
                    u.Email.ToLower() == email.ToLower() &&
                    u.PasswordHash == passwordHash);
        }

        //  Check if email exists
        public async Task<bool> IsEmailExistsAsync(string email)
        {
            return await _context.Users.AnyAsync(u => u.Email.ToLower() == email.ToLower());
        }

        //  Check if username exists
        public async Task<bool> IsUsernameExistsAsync(string username)
        {
            return await _context.Users.AnyAsync(u => u.Username.ToLower() == username.ToLower());
        }
    }
}

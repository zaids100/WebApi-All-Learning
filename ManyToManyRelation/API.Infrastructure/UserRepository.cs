using API.Application;
using API.DATA;
using API.Domain;
using Microsoft.EntityFrameworkCore;

namespace API.Infrastructure
{
    public class UserRepository : IUserPost<User, int>
    {
        private readonly UserPostContext _context;
        public UserRepository(UserPostContext context)
        {
            _context = context;
        }

        public async Task<User?> GetByName(string name)
        {
            return await _context.Users
                                 .FirstOrDefaultAsync(u => u.Username == name);
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await _context.Users
    .Include(u => u.UserPosts)        // loads UserPosts collection
    .ThenInclude(up => up.Post)       // loads the Post for each UserPost
    .ToListAsync();

        }

        public async Task<User?> GetById(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<User> Add(User entity)
        {
            _context.Users.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<User?> Update(User entity)
        {
            var existing = await _context.Users.FindAsync(entity.UserId);
            if (existing == null)
            {
                return null;
            }

            _context.Entry(existing).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();

            return existing;
        }

    }
}

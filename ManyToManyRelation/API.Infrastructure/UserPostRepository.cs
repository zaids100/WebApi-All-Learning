using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API.Application;
using API.DATA;
using API.Domain;
using Microsoft.EntityFrameworkCore;

namespace API.Infrastructure
{
    public class UserPostRepository : IUserPost<UserPost, int>
    {
        private readonly UserPostContext _context;

        public UserPostRepository(UserPostContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<UserPost>> GetByName(string uname)
        {
            return await _context.UserPosts
                                 .Include(up => up.User)
                                 .Include(up => up.Post)
                                 .Where(up => up.User.Username == uname)
                                 .ToListAsync();
        }


        public async Task<IEnumerable<UserPost>> GetAll()
        {
            return await _context.UserPosts
                                 .Include(up => up.User)
                                 .Include(up => up.Post)
                                 .ToListAsync();
        }

        public async Task<UserPost?> GetById(int id)
        {
            return await _context.UserPosts
                                 .Include(up => up.User)
                                 .Include(up => up.Post)
                                 .FirstOrDefaultAsync(up => up.UserId == id || up.PostId == id);
        }

        public async Task<UserPost> Add(UserPost entity)
        {
            await _context.UserPosts.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<UserPost> Update(UserPost entity)
        {
            _context.UserPosts.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}

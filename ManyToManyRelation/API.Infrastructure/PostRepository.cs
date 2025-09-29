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
    public class PostRepository : IUserPost<Post, int>
    {
        private readonly UserPostContext _context;
        public PostRepository(UserPostContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Post>>? GetByName(string name)
        {
            return await _context.Posts
                                 .FirstOrDefaultAsync(p => p.Title == name);
        }
        public async Task<IEnumerable<Post>> GetAll()
        {
            return await _context.Posts.ToListAsync();
        }

        public async Task<Post?> GetById(int id)
        {
            return await _context.Posts.FindAsync(id);
        }

        public async Task<Post> Add(Post entity)
        {
            _context.Posts.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Post?> Update(Post entity)
        {
            var existing = await _context.Posts.FindAsync(entity.PostId);
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
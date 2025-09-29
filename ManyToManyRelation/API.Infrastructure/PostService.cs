using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API.Application;
using API.Domain;
namespace API.Infrastructure
{
    public class PostService
    {
        private readonly IUserPost<Post, int> _postRepository;
        public PostService(IUserPost<Post, int> postRepository)
        {
            _postRepository = postRepository;
        }
        public async Task<IEnumerable<Post>> GetAllPosts()
        {
            return await _postRepository.GetAll();
        }
        public async Task<Post?> GetPostById(int id)
        {
            return await _postRepository.GetById(id);
        }
        public async Task<Post> AddPost(Post post)
        {
            return await _postRepository.Add(post);
        }
        public async Task<Post> Update(Post entity)
        {
            return await _postRepository.Update(entity);
        }
    }
}

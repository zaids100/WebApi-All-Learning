using API.Domain;
using API.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ManyToManyRelation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly PostService _postService;
        public PostsController(PostService postService)
        {
            _postService = postService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPosts()
        {
            var posts = await _postService.GetAllPosts();
            return Ok(posts);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPostById(int id)
        {
            var post = await _postService.GetPostById(id);
            if (post == null)
            {
                return NotFound();
            }
            return Ok(post);
        }

        [HttpPost]
        public async Task<IActionResult> AddPost(Post post)
        {
            var createdPost = await _postService.AddPost(post);
            return CreatedAtAction(nameof(GetPostById), new { id = createdPost.PostId }, createdPost);
        }

        [HttpPut]
        public async Task<IActionResult> Update(Post post)
        {
            var updatedPost = await _postService.Update(post);
            if (updatedPost == null)
            {
                return NotFound();
            }
            return Ok(updatedPost);
        }

    }
}

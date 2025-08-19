using BlogsAPI.Data;
using BlogsAPI.Models;
using BlogsAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace BlogsAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostsController : Controller
    {
        private readonly IPostsService _postsService;
        public PostsController(IPostsService postsService)
        {
            _postsService = postsService;
        }

        [HttpGet]
        public  ActionResult<IEnumerable<Post>> GetPosts()
        {
            try
            {
                return  Ok( _postsService.GetAllPosts());
            }
            catch (Exception ex)
            {
              return StatusCode(500, $"Internal server error: {ex.Message}");
            }
            
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<Post>> GetPost(int id)
        {
            var post = await _postsService.GetPost(id);
            if (post == null)
            {
                return NotFound();
            }
            return Ok(post);
        }

        [HttpPost()]
        [Route("addpost")]
        public  ActionResult<Post> AddPost(AddpostDTO postDTO)
        {
           
            if (postDTO == null)
            {
                return BadRequest("Post can't be null");
            }
            
            return  Ok(_postsService.AddPost(postDTO));

        }

        [HttpPut("updata/{id}")]
        public ActionResult<Post> UpdatePost(int id, UpdatePostDTO newPost)
        {
            if (id != newPost.Id)
            {
                return BadRequest("Post ID mismatch.");
            }
            try
            {
                var post  =  _postsService.UpdatePost(id, newPost);
                return Ok(post); 
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("delete/{id}")]
        public IActionResult DeletePost(int id)
        {
            try
            {
                _postsService.DeletePost(id);
                return NoContent(); // 204 No Content
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}

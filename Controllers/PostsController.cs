using BlogsAPI.Data;
using BlogsAPI.Models;
using BlogsAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace BlogsAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostsController : ControllerBase
    {
        private readonly IPostsService _postsService;
        public PostsController(IPostsService postsService)
        {
            _postsService = postsService;
        }

        [HttpGet]
        public ActionResult<ResultViewModel<IEnumerable<Post>>> GetPosts()
        {
            var result = new ResultViewModel<IEnumerable<Post>>();
            try
            {
                result.Data = _postsService.GetAllPosts();
                result.IsSuccess = true;
                result.Message = "Posts retrieved successfully";
                return Ok(result);
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = ex.Message;
                return StatusCode(500, result);
            }

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResultViewModel<Post>>> GetPost(int id)
        {
            var result = new ResultViewModel<Post>();

            try
            {
                result.Data = await _postsService.GetPost(id);
                result.IsSuccess = result.Data != null;
                if (result.Data == null)
                {
                    return NotFound(result);
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = $"Internal server error: {ex.Message}";
                return StatusCode(500, result);
            }
            
        }

        [HttpPost()]
        [Route("Add")]
        public ActionResult<ResultViewModel<Post>> AddPost(AddpostDTO postDTO)
        {
            var result = new ResultViewModel<Post>();

            try {

                if (postDTO == null)
                {
                    result.IsSuccess = false;
                    result.Message = "Post can't be null";
                    return BadRequest(result);
                }
                result.Data = _postsService.AddPost(postDTO); 
                result.Message = "Post added successfully";
                result.IsSuccess = true;
                return Ok(result);
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = $"Internal server error: {ex.Message}";
                return StatusCode(500, result);
            }

        }

        [HttpPut("Update/{id}")]
        public ActionResult<ResultViewModel<Post>> UpdatePost(int id, UpdatePostDTO newPost)
        {
            var result = new ResultViewModel<Post>();
            
            try
            {
                if (id != newPost.Id)
                {
                    result.IsSuccess = false;
                    result.Message = "Post ID mismatch.";
                    return BadRequest(result);
                }
                result.Data = _postsService.UpdatePost(id, newPost);
                result.IsSuccess = true;
                result.Message = "Post updated successfully";
                return Ok(result);
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = $"Internal server error: {ex.Message}";
                return StatusCode(500, result);

            }
        }

        [HttpDelete("Delete/{id}")]
        public ActionResult<ResultViewModel> DeletePost(int id)
        {
            var result = new ResultViewModel();

            try
            {
                _postsService.DeletePost(id);
                result.IsSuccess = true;
                result.Message = "Post deleted successfully";
                return Ok(result); 
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = $"Internal server error: {ex.Message}";
                return StatusCode(500, result);
            }
        }
        [HttpGet("{postId}/Comments")]
        public ActionResult<ResultViewModel<IEnumerable<Comment>>> GetCommentsByPostId(int postId)
        {
            var result = new ResultViewModel<IEnumerable<Comment>>();
            try
            {
                 result.Data = _postsService.GetCommentsByPostId(postId);
                if (result.Data == null || !result.Data.Any())
                {
                    result.IsSuccess = false;
                    result.Message = "No comments found for this post.";
                    return NotFound(result);
                }
                result.IsSuccess = true;
                result.Message = "Comments retrieved successfully";
                return Ok(result);
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = $"Internal server error: {ex.Message}";
                return StatusCode(500, result);

            }
        }
    }
}

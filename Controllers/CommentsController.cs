using Microsoft.AspNetCore.Mvc;
using BlogsAPI.Services;
using BlogsAPI.Models;
namespace BlogsAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommentsController : Controller
    {

        private readonly ICommentsService _commentsService;
        public CommentsController(ICommentsService commentsService)
        {
            _commentsService = commentsService ?? throw new ArgumentNullException(nameof(commentsService));
        }

        // get all comments
        [HttpGet]
        public  ActionResult<List<Comment>>  GetAllComments()
        {
            try
            {
                return Ok(_commentsService.GetAllComments());
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("{id}")]

        public ActionResult<Comment> GetCommentById(int id)
        {
            try
            {
                var comment = _commentsService.GetCommentById(id);
                return Ok(comment);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        [Route("addComment")]

        public ActionResult<Comment> AddComment(AddCommentDTO comment) {

            try
            {
                var addedComment =  _commentsService.AddComment(comment);
                return Ok(addedComment);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");

            }

        }

        [HttpPut]
        [Route("UpdateComment/{id}")]
        public ActionResult<Comment> UpdateComment(int id,UpdateCommentDTO comment)
        {
            if (id != comment.Id)
            {
                return BadRequest("Post ID mismatch.");
            }
            try
            {
                var commentUpdated = _commentsService.UpdateComment(id, comment);
                return Ok(commentUpdated);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete]
        [Route("Delete/{id}")]

        public IActionResult DeleteComment(int id)
        {
            try
            {
                _commentsService.DeleteComment(id);
                 return NoContent(); // 204 No Content
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}

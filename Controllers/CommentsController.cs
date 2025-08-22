using Microsoft.AspNetCore.Mvc;
using BlogsAPI.Services;
using BlogsAPI.Models;
namespace BlogsAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommentsController : ControllerBase
    {

        private readonly ICommentsService _commentsService;
        public CommentsController(ICommentsService commentsService)
        {
            _commentsService = commentsService ?? throw new ArgumentNullException(nameof(commentsService));
        }

        // get all comments
        [HttpGet]
        public  ActionResult<ResultViewModel<IEnumerable<Comment>>>  GetAllComments()
        {
            var result = new ResultViewModel<IEnumerable<Comment>>();  
            try
            {
                result.Data = _commentsService.GetAllComments();
                result.IsSuccess = true;
                result.Message = "Comments retrieved successfully.";
                return Ok(result);
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = ex.Message;
                return StatusCode(500, result);
            }
        }

        [HttpGet]
        [Route("{id}")]

        public ActionResult<ResultViewModel<Comment>> GetCommentById(int id)
        {
            var result = new ResultViewModel<Comment>();
            try
            {
                result.Data = _commentsService.GetCommentById(id);
                result.IsSuccess = true;
                result.Message = "Comment retrieved successfully.";
                return Ok(result);
            }
            catch (KeyNotFoundException ex)
            {
                result.IsSuccess = false;
                result.Message = ex.Message;
                return NotFound(result);
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = ex.Message;
                return StatusCode(500, result);
            }
        }

        [HttpPost]
        [Route("Add")]

        public ActionResult<ResultViewModel<Comment>> AddComment(AddCommentDTO comment) 
        {
            var result = new ResultViewModel<Comment>();
            try
            {
                result.Data = _commentsService.AddComment(comment);
                result.IsSuccess = true;
                result.Message = "Comment added successfully.";
                return Ok(result);
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = ex.Message;
                return StatusCode(500, result);

            }

        }

        [HttpPut]
        [Route("Update/{id}")]
        public ActionResult<ResultViewModel<Comment>> UpdateComment([FromRoute]int id,[FromBody] UpdateCommentDTO comment)
        {
           var result = new ResultViewModel<Comment>();
            try
            {
                if (id != comment.Id)
                {
                    result.IsSuccess = false;
                    result.Message = "Comment ID mismatch.";
                    return BadRequest(result);
                }
                result.Data = _commentsService.UpdateComment(id, comment);
                result.IsSuccess = true;
                result.Message = "Comment updated successfully.";
                return Ok(result);
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = ex.Message;
                return StatusCode(500, result);
            }
        }

        [HttpDelete]
        [Route("Delete/{id}")]

        public ActionResult<ResultViewModel> DeleteComment(int id)
        {
            var result = new ResultViewModel();
            try
            {
                _commentsService.DeleteComment(id);
                 result.IsSuccess = true;
                result.Message = "Comment deleted successfully.";
                return Ok(result);
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = ex.Message;
                return StatusCode(500, result);
            }
        }
    }
}

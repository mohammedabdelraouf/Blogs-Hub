using BlogsAPI.Data;
using BlogsAPI.Models;
using BlogsAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogsAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorsService _authorsService;

        public AuthorsController(IAuthorsService authorsService)
        {
            _authorsService = authorsService;
        }

        // This controller handles requests related to authors in the blogging API.
        [HttpGet]
        [AllowAnonymous]
        public  ActionResult<ResultViewModel<IEnumerable<Author>>> GetAuthors()
        {
            var result = new ResultViewModel<IEnumerable<Author>>();
            try { 
                result.Data = _authorsService.GetAllAuthors();
                result.IsSuccess = true;
                result.Message = "Authors retrieved successfully";
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
        [AllowAnonymous]
        public async Task<ActionResult<ResultViewModel<Author>>> GetAuthor(int id)
        {
            var result = new ResultViewModel<Author>();
            try
            {
                result.Data = await _authorsService.GetAuthor(id);
    
                if (result.Data == null)
                {
                    result.IsSuccess = false;
                    result.Message = "Author not found";
                    return NotFound(result);
                }
                result.IsSuccess = true;
                result.Message = "Author retrieved successfully";
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
        [Route("{id}/Posts")]
        [AllowAnonymous]
        public async Task<ActionResult<ResultViewModel<IEnumerable<Post>>>> GetPostsByAuthorId(int id)
        {
            var result = new ResultViewModel<IEnumerable<Post>>();
            try
            {
                result.Data = await _authorsService.GetPosts(id);
                result.IsSuccess = true;
                result.Message = "Posts retrieved successfully";
                return Ok(result);
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = $"Internal server error: {ex.Message}";
                return StatusCode(500, result);
            }
        }


        [HttpPost]
        [Route("Add")]
        [Authorize(Roles = "Admin")]
        public ActionResult<ResultViewModel<Author>> AddAuthor([FromBody] AddAuthorDTO authorDto)
        {
            var result = new ResultViewModel<Author>();
            try
            {
                if (authorDto == null)
                {
                    result.IsSuccess = false;
                    result.Message = "Author data is null.";
                    return BadRequest(result);
                }

                result.Data = _authorsService.AddAuthor(authorDto);
                return CreatedAtAction(nameof(GetAuthor), new { id = result.Data.Id }, result.Data);
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = ex.Message;
                return StatusCode(500, result);
            }
        }

        [HttpPut("Update/{id}")]
        [Authorize (Roles = "Admin")]
        public ActionResult<ResultViewModel<Author>> UpdateAuthor(int id, [FromBody] UpdateAuthorDTO updateAuthorDto)
        {
            var result = new ResultViewModel<Author>();
            try
            {
                if (updateAuthorDto == null)
                {
                    result.IsSuccess = false;
                    result.Message = "Update data is null.";
                    return BadRequest(result);
                }

                result.Data = _authorsService.UpdateAuthor(id, updateAuthorDto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = ex.Message;
                return StatusCode(500, result);
            }
        }
        [HttpDelete("Delete/{id}")]
        [Authorize (Roles = "Admin")]
        public ActionResult<ResultViewModel> DeleteAuthor(int id)
        {
            var result = new ResultViewModel();
            try
            {
                
                _authorsService.DeleteAuthor(id);
                result.IsSuccess = true;
                result.Message = "Author deleted successfully";
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

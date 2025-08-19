using BlogsAPI.Data;
using BlogsAPI.Models;
using BlogsAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace BlogsAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorsController : Controller
    {
        private readonly IAuthorsService _authorsService;

        public AuthorsController(IAuthorsService authorsService)
        {
            _authorsService = authorsService;
        }

        // This controller handles requests related to authors in the blogging API.
        [HttpGet]
        public  ActionResult<List<Author>> GetAuthors()
        {
            try { 
                var authors = _authorsService.GetAllAuthors();
                return Ok(authors);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Author>> GetAuthor(int id)
        {
            try
            {
                var author = await _authorsService.GetAuthor(id);
                if (author == null)
                {
                    return NotFound();
                }
                return Ok(author);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("{id}/Posts")]
        public async Task<ActionResult<List<Post>>> GetPostsByAuthorId(int id)
        {

            try {

                return  Ok(await _authorsService.GetPosts(id));
            }
            catch (Exception ex) {

                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

        }


        [HttpPost]
        [Route("Add")]
        public ActionResult<Author> AddAuthor([FromBody] AddAuthorDTO authorDto)
        {
            try
            {
                if (authorDto == null)
                {
                    return BadRequest("Author data is null.");
                }
                var newAuthor = _authorsService.AddAuthor(authorDto);
                return CreatedAtAction(nameof(GetAuthor), new { id = newAuthor.Id }, newAuthor);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpPut("Update/{id}")]
        public ActionResult UpdateAuthor(int id, [FromBody] UpdateAuthorDTO updateAuthorDto)
        {
            try
            {
                if (updateAuthorDto == null)
                {
                    return BadRequest("Update data is null.");
                }
                _authorsService.UpdateAuthor(id, updateAuthorDto);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpDelete("Delete/{id}")]
        public ActionResult DeleteAuthor(int id)
        {
            try
            {
                _authorsService.DeleteAuthor(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

    }
}

using BookandBeats.Models;
using BookandBeats.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookandBeats.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorApiController : ControllerBase
    {
        private readonly IAuthorService _service;

        public AuthorApiController(IAuthorService service)
        {
            _service = service;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var authors = await _service.GetAllAsync();
            return Ok(authors);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var author = await _service.GetByIdAsync(id);
            if (author == null) return NotFound();
            return Ok(author);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Author author)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            await _service.AddAsync(author);
            return CreatedAtAction(nameof(GetById), new { id = author.AuthorId }, author);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Author author)
        {
            if (id != author.AuthorId) return BadRequest();

            await _service.UpdateAsync(author);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}

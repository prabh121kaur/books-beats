using BookandBeats.Models;
using BookandBeats.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookandBeats.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookAuthorApiController : ControllerBase
    {
        private readonly IBookAuthorService _service;

        public BookAuthorApiController(IBookAuthorService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var bookAuthors = await _service.GetAllAsync();
            return Ok(bookAuthors);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var bookAuthor = await _service.GetByIdAsync(id);
            if (bookAuthor == null) return NotFound();
            return Ok(bookAuthor);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] BookAuthor bookAuthor)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            await _service.AddAsync(bookAuthor);
            return CreatedAtAction(nameof(GetById), new { id = bookAuthor.BookAuthorId }, bookAuthor);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] BookAuthor bookAuthor)
        {
            if (id != bookAuthor.BookAuthorId) return BadRequest();

            await _service.UpdateAsync(bookAuthor);
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

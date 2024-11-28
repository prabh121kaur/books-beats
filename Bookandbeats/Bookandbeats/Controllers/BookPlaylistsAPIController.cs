using BookandBeats.Models;
using BookandBeats.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookandBeats.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookPlaylistsApiController : ControllerBase
    {
        private readonly IBookPlaylistService _bookPlaylistService;

        public BookPlaylistsApiController(IBookPlaylistService bookPlaylistService)
        {
            _bookPlaylistService = bookPlaylistService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookPlaylist>>> GetBookPlaylists()
        {
            var bookPlaylists = await _bookPlaylistService.GetAllBookPlaylistsAsync();
            return Ok(bookPlaylists);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BookPlaylist>> GetBookPlaylist(int id)
        {
            var bookPlaylist = await _bookPlaylistService.GetBookPlaylistByIdAsync(id);
            if (bookPlaylist == null)
            {
                return NotFound();
            }
            return Ok(bookPlaylist);
        }

        [HttpPost]
        public async Task<ActionResult<BookPlaylist>> CreateBookPlaylist([FromBody] BookPlaylist bookPlaylist)
        {
            var created = await _bookPlaylistService.AddBookPlaylistAsync(bookPlaylist);
            return CreatedAtAction(nameof(GetBookPlaylist), new { id = created.BookPlaylistId }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBookPlaylist(int id, [FromBody] BookPlaylist bookPlaylist)
        {
            var updated = await _bookPlaylistService.UpdateBookPlaylistAsync(id, bookPlaylist);
            if (!updated)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBookPlaylist(int id)
        {
            var deleted = await _bookPlaylistService.DeleteBookPlaylistAsync(id);
            if (!deleted)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}

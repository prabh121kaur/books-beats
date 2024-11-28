using BookandBeats.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bookandbeats.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SongsAPIController : ControllerBase
    {
        private readonly ISongService _songService;

        public SongsAPIController(ISongService songService)
        {
            _songService = songService;
        }

        // GET: api/Songs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Song>>> GetSongs()
        {
            var songs = await _songService.GetAllSongsAsync();
            return Ok(songs);
        }

        // GET: api/Songs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Song>> GetSong(int id)
        {
            var song = await _songService.GetSongByIdAsync(id);

            if (song == null)
            {
                return NotFound();
            }

            return Ok(song);
        }

        // POST: api/Songs
        [HttpPost]
        public async Task<ActionResult<Song>> PostSong(Song song)
        {
            var createdSong = await _songService.CreateSongAsync(song);
            return CreatedAtAction(nameof(GetSong), new { id = createdSong}, createdSong);
        }

        // PUT: api/Songs/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSong(int id, Song song)
        {
            if (id != song.SongId)
            {
                return BadRequest();
            }

            try
            {
                await _songService.UpdateSongAsync(song);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }

            return NoContent();
        }

        // DELETE: api/Songs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSong(int id)
        {
            await _songService.DeleteSongAsync(id);
            return NoContent();
        }
    }
}

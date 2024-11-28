using BookandBeats.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookandBeats.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlaylistController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PlaylistController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Playlist
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Playlist>>> GetPlaylists()
        {
            var playlists = await _context.Playlists
                .Include(p => p.BookPlaylists)
                .Include(p => p.PlaylistSongs)
                .ToListAsync();
            return Ok(playlists);
        }

        // GET: api/Playlist/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Playlist>> GetPlaylist(int id)
        {
            var playlist = await _context.Playlists
                .Include(p => p.BookPlaylists)
                .Include(p => p.PlaylistSongs)
                .FirstOrDefaultAsync(p => p.PlaylistId == id);

            if (playlist == null)
            {
                return NotFound($"Playlist with ID {id} not found.");
            }

            return Ok(playlist);
        }

        // POST: api/Playlist
        [HttpPost]
        public async Task<ActionResult<Playlist>> CreatePlaylist([FromBody] Playlist playlist)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Playlists.Add(playlist);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPlaylist), new { id = playlist.PlaylistId }, playlist);
        }

        // PUT: api/Playlist/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePlaylist(int id, [FromBody] Playlist updatedPlaylist)
        {
            if (id != updatedPlaylist.PlaylistId)
            {
                return BadRequest("Playlist ID mismatch.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingPlaylist = await _context.Playlists.FindAsync(id);
            if (existingPlaylist == null)
            {
                return NotFound($"Playlist with ID {id} not found.");
            }

            existingPlaylist.Name = updatedPlaylist.Name;
            existingPlaylist.Mood = updatedPlaylist.Mood;
            existingPlaylist.PrivacySetting = updatedPlaylist.PrivacySetting;

            _context.Entry(existingPlaylist).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlaylistExists(id))
                {
                    return NotFound($"Playlist with ID {id} not found.");
                }
                throw;
            }

            return NoContent();
        }

        // DELETE: api/Playlist/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlaylist(int id)
        {
            var playlist = await _context.Playlists.FindAsync(id);
            if (playlist == null)
            {
                return NotFound($"Playlist with ID {id} not found.");
            }

            _context.Playlists.Remove(playlist);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PlaylistExists(int id)
        {
            return _context.Playlists.Any(e => e.PlaylistId == id);
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookandBeats.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookandBeats.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlaylistSongsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PlaylistSongsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/PlaylistSongs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlaylistSong>>> GetPlaylistSongs()
        {
            return await _context.PlaylistSongs
                .Include(ps => ps.Playlist)  // Include Playlist information
                .Include(ps => ps.Song)      // Include Song information
                .ToListAsync();
        }

        // GET: api/PlaylistSongs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PlaylistSong>> GetPlaylistSong(int id)
        {
            var playlistSong = await _context.PlaylistSongs
                .Include(ps => ps.Playlist)  // Include Playlist information
                .Include(ps => ps.Song)      // Include Song information
                .FirstOrDefaultAsync(ps => ps.PlaylistId == id);

            if (playlistSong == null)
            {
                return NotFound();
            }

            return playlistSong;
        }

        // POST: api/PlaylistSongs
        [HttpPost]
        public async Task<ActionResult<PlaylistSong>> PostPlaylistSong(PlaylistSong playlistSong)
        {
            // Check if the combination of PlaylistId and SongId already exists
            var existingPlaylistSong = await _context.PlaylistSongs
                .AnyAsync(ps => ps.PlaylistId == playlistSong.PlaylistId && ps.SongId == playlistSong.SongId);

            if (existingPlaylistSong)
            {
                return Conflict("The song is already in the playlist.");
            }

            _context.PlaylistSongs.Add(playlistSong);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPlaylistSong", new { id = playlistSong.PlaylistId }, playlistSong);
        }

        // DELETE: api/PlaylistSongs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlaylistSong(int id)
        {
            var playlistSong = await _context.PlaylistSongs.FindAsync(id);
            if (playlistSong == null)
            {
                return NotFound();
            }

            _context.PlaylistSongs.Remove(playlistSong);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // PUT: api/PlaylistSongs/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPlaylistSong(int id, PlaylistSong playlistSong)
        {
            if (id != playlistSong.PlaylistId)
            {
                return BadRequest();
            }

            _context.Entry(playlistSong).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlaylistSongExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        private bool PlaylistSongExists(int id)
        {
            return _context.PlaylistSongs.Any(e => e.PlaylistId == id);
        }
    }
}

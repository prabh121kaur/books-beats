using BookandBeats.Models;
using CoreEntityFramework.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoreEntityFramework.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlaylistsController : ControllerBase
    {
        private readonly IPlaylistService _playlistService;

        public PlaylistsController(IPlaylistService playlistService)
        {
            _playlistService = playlistService;
        }

        // GET: api/Playlists
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Playlist>>> GetPlaylists()
        {
            var playlists = await _playlistService.GetPlaylistsAsync();
            return Ok(playlists);
        }

        // GET: api/Playlists/5
        [HttpGet("{id}")]
     
        public async Task<ActionResult<Playlist>> GetPlaylist(int id)
        {
            var playlist = await _playlistService.GetPlaylistByIdAsync(id);

            if (playlist == null)
            {
                return NotFound();
            }

            return Ok(playlist);
        }

        // POST: api/Playlists
        [HttpPost]
        public async Task<ActionResult<Playlist>> PostPlaylist(Playlist playlist)
        {
            var createdPlaylist = await _playlistService.CreatePlaylistAsync(playlist);
            return CreatedAtAction(nameof(GetPlaylist), new { id = createdPlaylist.PlaylistId }, createdPlaylist);
        }

        // PUT: api/Playlists/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPlaylist(int id, Playlist playlist)
        {
            if (id != playlist.PlaylistId)
            {
                return BadRequest();
            }

            try
            {
                await _playlistService.UpdatePlaylistAsync(id, playlist);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }

            return NoContent();
        }

        // DELETE: api/Playlists/5

        [HttpDelete("{id}")]
        
        public async Task<IActionResult> DeletePlaylist(int id)
        {
            await _playlistService.DeletePlaylistAsync(id);
            return NoContent();
        }
    }
}

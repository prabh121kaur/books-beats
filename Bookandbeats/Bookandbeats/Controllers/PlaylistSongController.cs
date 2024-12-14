using Microsoft.AspNetCore.Mvc;
using BookandBeats.Models;
using BookandBeats.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace BookandBeats.Controllers
{
    public class PlaylistSongController : Controller
    {
        private readonly IPlaylistSongService _playlistSongService;

        public PlaylistSongController(IPlaylistSongService playlistSongService)
        {
            _playlistSongService = playlistSongService;
        }

        // GET: PlaylistSongs
        public async Task<IActionResult> Index()
        {
            var playlistSongs = await _playlistSongService.GetAllPlaylistSongsAsync();
            return View(playlistSongs);
        }

        // GET: PlaylistSongs/Details/5
        public async Task<IActionResult> Details(int playlistId, int songId)
        {
            var playlistSong = await _playlistSongService.GetPlaylistSongByIdAsync(playlistId, songId);
            if (playlistSong == null) return NotFound();
            return View(playlistSong);
        }

        // GET: PlaylistSongs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PlaylistSongs/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PlaylistSong playlistSong)
        {
            if (ModelState.IsValid)
            {
                await _playlistSongService.AddPlaylistSongAsync(playlistSong);
                return RedirectToAction(nameof(Index));
            }
            return View(playlistSong);
        }

        // GET: PlaylistSongs/Edit/5
        public async Task<IActionResult> Edit(int playlistId, int songId)
        {
            var playlistSong = await _playlistSongService.GetPlaylistSongByIdAsync(playlistId, songId);
            if (playlistSong == null) return NotFound();
            return View(playlistSong);
        }

        // POST: PlaylistSongs/Edit/5
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int playlistId, int songId, PlaylistSong playlistSong)
        {
            if (playlistId != playlistSong.PlaylistId || songId != playlistSong.SongId) return BadRequest();

            if (ModelState.IsValid)
            {
                try
                {
                    await _playlistSongService.UpdatePlaylistSongAsync(playlistSong);
                }
                catch
                {
                    if (!_playlistSongService.PlaylistSongExists(playlistId, songId)) return NotFound();
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(playlistSong);
        }

        // GET: PlaylistSongs/Delete/5
        public async Task<IActionResult> Delete(int playlistId, int songId)
        {
            var playlistSong = await _playlistSongService.GetPlaylistSongByIdAsync(playlistId, songId);
            if (playlistSong == null) return NotFound();
            return View(playlistSong);
        }

        // POST: PlaylistSongs/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int playlistId, int songId)
        {
            await _playlistSongService.RemovePlaylistSongAsync(playlistId, songId);
            return RedirectToAction(nameof(Index));
        }
    }
}

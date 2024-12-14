using BookandBeats.Models;
using BookandBeats.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookandBeats.Controllers
{
    public class BookPlaylistsController : Controller
    {
        private readonly IBookPlaylistService _bookPlaylistService;

        public BookPlaylistsController(IBookPlaylistService bookPlaylistService)
        {
            _bookPlaylistService = bookPlaylistService;
        }

        public async Task<IActionResult> Index()
        {
            var bookPlaylists = await _bookPlaylistService.GetAllBookPlaylistsAsync();
            return View(bookPlaylists);
        }

        public async Task<IActionResult> Details(int id)
        {
            var bookPlaylist = await _bookPlaylistService.GetBookPlaylistByIdAsync(id);
            if (bookPlaylist == null)
            {
                return NotFound();
            }
            return View(bookPlaylist);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BookPlaylist bookPlaylist)
        {
            if (ModelState.IsValid)
            {
                await _bookPlaylistService.AddBookPlaylistAsync(bookPlaylist);
                return RedirectToAction(nameof(Index));
            }
            return View(bookPlaylist);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var bookPlaylist = await _bookPlaylistService.GetBookPlaylistByIdAsync(id);
            if (bookPlaylist == null)
            {
                return NotFound();
            }
            return View(bookPlaylist);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, BookPlaylist bookPlaylist)
        {
            if (ModelState.IsValid)
            {
                var updated = await _bookPlaylistService.UpdateBookPlaylistAsync(id, bookPlaylist);
                if (!updated)
                {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(bookPlaylist);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var bookPlaylist = await _bookPlaylistService.GetBookPlaylistByIdAsync(id);
            if (bookPlaylist == null)
            {
                return NotFound();
            }
            return View(bookPlaylist);
        }

        [HttpPost, ActionName("Delete")]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var deleted = await _bookPlaylistService.DeleteBookPlaylistAsync(id);
            if (!deleted)
            {
                return NotFound();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}

using BookandBeats.Models;
using BookandBeats.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookandBeats.Controllers
{
    public class BookAuthorController : Controller
    {
        private readonly IBookAuthorService _service;

        public BookAuthorController(IBookAuthorService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var bookAuthors = await _service.GetAllAsync();
            return View(bookAuthors);
        }

        public async Task<IActionResult> Details(int id)
        {
            var bookAuthor = await _service.GetByIdAsync(id);
            if (bookAuthor == null) return NotFound();

            return View(bookAuthor);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BookAuthor bookAuthor)
        {
            if (ModelState.IsValid)
            {
                await _service.AddAsync(bookAuthor);
                return RedirectToAction(nameof(Index));
            }
            return View(bookAuthor);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var bookAuthor = await _service.GetByIdAsync(id);
            if (bookAuthor == null) return NotFound();

            return View(bookAuthor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, BookAuthor bookAuthor)
        {
            if (id != bookAuthor.BookAuthorId) return BadRequest();

            if (ModelState.IsValid)
            {
                await _service.UpdateAsync(bookAuthor);
                return RedirectToAction(nameof(Index));
            }
            return View(bookAuthor);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var bookAuthor = await _service.GetByIdAsync(id);
            if (bookAuthor == null) return NotFound();

            return View(bookAuthor);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}

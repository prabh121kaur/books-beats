using BookandBeats.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookandBeats.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly AppDbContext _context;

        public GenresController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Genres
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Genre>>> GetGenres()
        {
            return await _context.Genres
                .Include(g => g.Books) // Include related books
                .ToListAsync();
        }

        // GET: api/Genres/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Genre>> GetGenre(int id)
        {
            var genre = await _context.Genres
                .Include(g => g.Books) // Include related books
                .FirstOrDefaultAsync(g => g.GenreId == id);

            if (genre == null)
            {
                return NotFound();
            }

            return genre;
        }

        // POST: api/Genres
        [HttpPost]
        public async Task<ActionResult<Genre>> CreateGenre([FromBody] Genre genre)
        {
            if (genre == null)
            {
                return BadRequest("Genre data is invalid.");
            }

            _context.Genres.Add(genre);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetGenre), new { id = genre.GenreId }, genre);
        }

        // PUT: api/Genres/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGenre(int id, [FromBody] Genre genre)
        {
            if (id != genre.GenreId)
            {
                return BadRequest("Genre ID mismatch.");
            }

            _context.Entry(genre).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GenreExists(id))
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

        // DELETE: api/Genres/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGenre(int id)
        {
            var genre = await _context.Genres
                .Include(g => g.Books) // Include related books to handle cascading delete
                .FirstOrDefaultAsync(g => g.GenreId == id);

            if (genre == null)
            {
                return NotFound();
            }

            // Optionally: Prevent deletion if books are associated
            if (genre.Books.Any())
            {
                return BadRequest("Cannot delete genre with associated books.");
            }

            _context.Genres.Remove(genre);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Helper method to check if a genre exists
        private bool GenreExists(int id)
        {
            return _context.Genres.Any(g => g.GenreId == id);
        }
    }
}

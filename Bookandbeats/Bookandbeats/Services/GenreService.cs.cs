using BookandBeats.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace BookandBeats.Services
{
    public class GenreService : IGenreService
    {
        private readonly AppDbContext _context;

        public GenreService(AppDbContext context)
        {
            _context = context;
        }

        // Retrieve all genres
        public async Task<IEnumerable<Genre>> GetAllGenresAsync()
        {
            return await _context.Genres
                .Include(g => g.Books) // Include related books
                .ToListAsync();
        }

        // Retrieve a genre by ID
        public async Task<Genre> GetGenreByIdAsync(int id)
        {
            return await _context.Genres
                .Include(g => g.Books) // Include related books
                .FirstOrDefaultAsync(g => g.GenreId == id);
        }

        // Add a new genre
        public async Task<Genre> AddGenreAsync(Genre genre)
        {
            if (genre == null)
            {
                throw new ArgumentNullException(nameof(genre));
            }

            _context.Genres.Add(genre);
            await _context.SaveChangesAsync();
            return genre;
        }

        // Update an existing genre
        public async Task<bool> UpdateGenreAsync(int id, Genre genre)
        {
            if (id != genre.GenreId)
            {
                return false; // ID mismatch
            }

            _context.Entry(genre).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await GenreExistsAsync(id))
                {
                    return false; // Genre not found
                }
                else
                {
                    throw;
                }
            }
        }

        // Delete a genre
        public async Task<bool> DeleteGenreAsync(int id)
        {
            var genre = await _context.Genres
                .Include(g => g.Books) // Include related books
                .FirstOrDefaultAsync(g => g.GenreId == id);

            if (genre == null)
            {
                return false; // Genre not found
            }

            // Optionally: Prevent deletion if books are associated
            if (genre.Books.Any())
            {
                throw new InvalidOperationException("Cannot delete genre with associated books.");
            }

            _context.Genres.Remove(genre);
            await _context.SaveChangesAsync();
            return true;
        }

        // Check if a genre exists
        public async Task<bool> GenreExistsAsync(int id)
        {
            return await _context.Genres.AnyAsync(g => g.GenreId == id);
        }
    }
}

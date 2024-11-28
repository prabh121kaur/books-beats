using BookandBeats.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace BookandBeats.Services
{
    public class BookPlaylistService : IBookPlaylistService
    {
        private readonly AppDbContext _context;

        public BookPlaylistService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<BookPlaylist>> GetAllBookPlaylistsAsync()
        {
            return await _context.BookPlaylists
                .Include(bp => bp.Book)       // Include related Book
                .Include(bp => bp.Playlist)  // Include related Playlist
                .ToListAsync();
        }

        public async Task<BookPlaylist> GetBookPlaylistByIdAsync(int id)
        {
            return await _context.BookPlaylists
                .Include(bp => bp.Book)       // Include related Book
                .Include(bp => bp.Playlist)  // Include related Playlist
                .FirstOrDefaultAsync(bp => bp.BookPlaylistId == id);
        }

        public async Task<BookPlaylist> AddBookPlaylistAsync(BookPlaylist bookPlaylist)
        {
            if (bookPlaylist == null)
            {
                throw new ArgumentNullException(nameof(bookPlaylist));
            }

            _context.BookPlaylists.Add(bookPlaylist);
            await _context.SaveChangesAsync();
            return bookPlaylist;
        }

        public async Task<bool> UpdateBookPlaylistAsync(int id, BookPlaylist bookPlaylist)
        {
            if (id != bookPlaylist.BookPlaylistId)
            {
                return false; // ID mismatch
            }

            _context.Entry(bookPlaylist).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await BookPlaylistExistsAsync(id))
                {
                    return false; // BookPlaylist not found
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task<bool> DeleteBookPlaylistAsync(int id)
        {
            var bookPlaylist = await GetBookPlaylistByIdAsync(id);
            if (bookPlaylist == null)
            {
                return false; // BookPlaylist not found
            }

            _context.BookPlaylists.Remove(bookPlaylist);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> BookPlaylistExistsAsync(int id)
        {
            return await _context.BookPlaylists.AnyAsync(bp => bp.BookPlaylistId == id);
        }
    }
}

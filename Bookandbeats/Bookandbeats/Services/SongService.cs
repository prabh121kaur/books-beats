using BookandBeats.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookandBeats.Services
{
    public class SongService : ISongService
    {
        private readonly AppDbContext _context;

        public SongService(AppDbContext context)
        {
            _context = context;
        }

        // Get all songs
        public async Task<IEnumerable<Song>> GetAllSongsAsync()
        {
            return await _context.Songs.ToListAsync();
        }

        // Get a song by its ID
        public async Task<Song> GetSongByIdAsync(int id)
        {
            return await _context.Songs
                .FirstOrDefaultAsync(s => s.SongId == id);
        }

        // Create a new song
        public async Task<bool> CreateSongAsync(Song song)
        {
            if (song == null)
                return false;

            _context.Songs.Add(song);
            await _context.SaveChangesAsync();
            return true;
        }

        // Update an existing song
        public async Task<bool> UpdateSongAsync(Song song)
        {
            if (song == null)
                return false;

            _context.Songs.Update(song);
            await _context.SaveChangesAsync();
            return true;
        }

        // Delete a song by its ID
        public async Task<bool> DeleteSongAsync(int id)
        {
            var song = await _context.Songs.FindAsync(id);
            if (song == null)
                return false;

            _context.Songs.Remove(song);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}

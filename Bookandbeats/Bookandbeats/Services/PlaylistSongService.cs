using BookandBeats.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookandBeats.Services
{
    public class PlaylistSongService : IPlaylistSongService
    {
        private readonly AppDbContext _context;

        public PlaylistSongService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PlaylistSong>> GetAllPlaylistSongsAsync()
        {
            return await _context.PlaylistSongs
                .Include(ps => ps.Playlist)  // Include related Playlist
                .Include(ps => ps.Song)      // Include related Song
                .ToListAsync();
        }

        public async Task<PlaylistSong> GetPlaylistSongByIdAsync(int playlistId, int songId)
        {
            return await _context.PlaylistSongs
                .Include(ps => ps.Playlist)
                .Include(ps => ps.Song)
                .FirstOrDefaultAsync(ps => ps.PlaylistId == playlistId && ps.SongId == songId);
        }

        public async Task<PlaylistSong> AddPlaylistSongAsync(PlaylistSong playlistSong)
        {
            if (PlaylistSongExists(playlistSong.PlaylistId, playlistSong.SongId))
            {
                throw new System.Exception("The song is already in the playlist.");
            }

            _context.PlaylistSongs.Add(playlistSong);
            await _context.SaveChangesAsync();
            return playlistSong;
        }

        public async Task<bool> RemovePlaylistSongAsync(int playlistId, int songId)
        {
            var playlistSong = await GetPlaylistSongByIdAsync(playlistId, songId);
            if (playlistSong == null) return false;

            _context.PlaylistSongs.Remove(playlistSong);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<PlaylistSong> UpdatePlaylistSongAsync(PlaylistSong playlistSong)
        {
            if (!PlaylistSongExists(playlistSong.PlaylistId, playlistSong.SongId))
            {
                throw new System.Exception("PlaylistSong does not exist.");
            }

            _context.Entry(playlistSong).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return playlistSong;
        }

        public bool PlaylistSongExists(int playlistId, int songId)
        {
            return _context.PlaylistSongs.Any(ps => ps.PlaylistId == playlistId && ps.SongId == songId);
        }
    }
}

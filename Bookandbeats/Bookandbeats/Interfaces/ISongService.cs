using BookandBeats.Models;

public interface ISongService
{
    Task<IEnumerable<Song>> GetAllSongsAsync();
    Task<Song> GetSongByIdAsync(int id);
    Task<bool> CreateSongAsync(Song song);
    Task<bool> UpdateSongAsync(Song song);
    Task<bool> DeleteSongAsync(int id);
}

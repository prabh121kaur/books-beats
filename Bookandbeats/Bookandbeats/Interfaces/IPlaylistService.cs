using BookandBeats.Models;


    public interface IPlaylistService
    {
        Task<IEnumerable<Playlist>> GetPlaylistsAsync();
        Task<Playlist> GetPlaylistByIdAsync(int id);
        Task<Playlist> CreatePlaylistAsync(Playlist playlist);
        Task UpdatePlaylistAsync(int id, Playlist playlist);
        Task DeletePlaylistAsync(int id);
        bool PlaylistExists(int id);
    }

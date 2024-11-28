using BookandBeats.Models;



    public interface IPlaylistSongService
    {
        Task<IEnumerable<PlaylistSong>> GetAllPlaylistSongsAsync();
        Task<PlaylistSong> GetPlaylistSongByIdAsync(int playlistId, int songId);
        Task<PlaylistSong> AddPlaylistSongAsync(PlaylistSong playlistSong);
        Task<bool> RemovePlaylistSongAsync(int playlistId, int songId);
        Task<PlaylistSong> UpdatePlaylistSongAsync(PlaylistSong playlistSong);
        bool PlaylistSongExists(int playlistId, int songId);
    }


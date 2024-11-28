namespace BookandBeats.Models
{
    public class Playlist
    {
        public int PlaylistId { get; set; }  // Primary Key
        public string Name { get; set; }
        public string Mood { get; set; }
        public string PrivacySetting { get; set; }

        public ICollection<BookPlaylist> BookPlaylists { get; set; } = new List<BookPlaylist>();  // Navigation Property
        public ICollection<PlaylistSong> PlaylistSongs { get; set; } = new List<PlaylistSong>();  // Navigation Property
    }
}

namespace BookandBeats.Models
{
    public class Song
    {
        public int SongId { get; set; }  // Primary Key
        public string Title { get; set; }
        public string Artist { get; set; }

        public ICollection<PlaylistSong> PlaylistSongs { get; set; } = new List<PlaylistSong>();  // Navigation Property
    }
}

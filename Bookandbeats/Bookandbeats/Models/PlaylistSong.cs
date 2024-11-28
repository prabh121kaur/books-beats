namespace BookandBeats.Models
{
    public class PlaylistSong
    {
        public int PlaylistId { get; set; }  // Foreign Key to Playlist
        public int SongId { get; set; }  // Foreign Key to Song

        public Playlist Playlist { get; set; }
        public Song Song { get; set; }
    }
}

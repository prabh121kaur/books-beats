namespace BookandBeats.Models
{
    public class BookPlaylist
    {
        public int BookId { get; set; }   // Foreign Key to Book
        public int PlaylistId { get; set; }   // Foreign Key to Playlist
        public int BookPlaylistId { get; set; }

        public Book Book { get; set; }
        public Playlist Playlist { get; set; }
    }
}

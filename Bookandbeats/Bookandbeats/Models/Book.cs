namespace BookandBeats.Models
{
    public class Book
    {
        public int BookId { get; set; }  // Primary Key
        public string Title { get; set; }
        public int GenreId { get; set; }  // Foreign Key to Genre
        public Genre Genre { get; set; }
        public int Id { get; set; }

        public ICollection<BookAuthor> BookAuthors { get; set; } = new List<BookAuthor>();  // Navigation Property
        public ICollection<BookPlaylist> BookPlaylists { get; set; } = new List<BookPlaylist>();  // Navigation Property
    }
}

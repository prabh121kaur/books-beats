namespace BookandBeats.Models
{
    public class Genre
    {
        public int GenreId { get; set; }  // Primary Key
        public string Name { get; set; }

        public ICollection<Book> Books { get; set; } = new List<Book>();  // Navigation Property
    }
}

namespace BookandBeats.Models
{
    public class BookAuthor
    {
        public int BookId { get; set; }    // Foreign key to Book
        public int AuthorId { get; set; }  // Foreign key to Author
        public int BookAuthorId { get; set; }


        public Book Book { get; set; }
        public Author Author { get; set; }
    }
}

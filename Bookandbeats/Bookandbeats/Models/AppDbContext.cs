using Microsoft.EntityFrameworkCore;

namespace BookandBeats.Models
{
    public class AppDbContext : DbContext
    {
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<BookAuthor> BookAuthors { get; set; }
        public DbSet<BookPlaylist> BookPlaylists { get; set; }
        public DbSet<Playlist> Playlists { get; set; }
        public DbSet<PlaylistSong> PlaylistSongs { get; set; }
        public DbSet<Song> Songs { get; set; }
        public DbSet<Genre> Genres { get; set; }

        // Constructor to pass options to the base DbContext
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        // Defining the composite keys and other relationships in the OnModelCreating method
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Composite key for BookAuthor (BookId, AuthorId)
            modelBuilder.Entity<BookAuthor>()
                .HasKey(ba => new { ba.BookId, ba.AuthorId });

            // Composite key for BookPlaylist (BookId, PlaylistId)
            modelBuilder.Entity<BookPlaylist>()
                .HasKey(bp => new { bp.BookId, bp.PlaylistId });

            // Composite key for PlaylistSong (PlaylistId, SongId)
            modelBuilder.Entity<PlaylistSong>()
                .HasKey(ps => new { ps.PlaylistId, ps.SongId });

            // Relationship configurations (Optional if you want to define navigation properties with specific behavior)
            modelBuilder.Entity<BookAuthor>()
                .HasOne(ba => ba.Book)    // A BookAuthor has one Book
                .WithMany(b => b.BookAuthors) // A Book has many BookAuthors
                .HasForeignKey(ba => ba.BookId);

            modelBuilder.Entity<BookAuthor>()
                .HasOne(ba => ba.Author)  // A BookAuthor has one Author
                .WithMany(a => a.BookAuthors) // An Author has many BookAuthors
                .HasForeignKey(ba => ba.AuthorId);

            modelBuilder.Entity<BookPlaylist>()
                .HasOne(bp => bp.Book)     // A BookPlaylist has one Book
                .WithMany(b => b.BookPlaylists) // A Book has many BookPlaylists
                .HasForeignKey(bp => bp.BookId);

            modelBuilder.Entity<BookPlaylist>()
                .HasOne(bp => bp.Playlist) // A BookPlaylist has one Playlist
                .WithMany(p => p.BookPlaylists) // A Playlist has many BookPlaylists
                .HasForeignKey(bp => bp.PlaylistId);

            modelBuilder.Entity<PlaylistSong>()
                .HasOne(ps => ps.Playlist) // A PlaylistSong has one Playlist
                .WithMany(p => p.PlaylistSongs) // A Playlist has many PlaylistSongs
                .HasForeignKey(ps => ps.PlaylistId);

            modelBuilder.Entity<PlaylistSong>()
                .HasOne(ps => ps.Song) // A PlaylistSong has one Song
                .WithMany(s => s.PlaylistSongs) // A Song has many PlaylistSongs
                .HasForeignKey(ps => ps.SongId);

            // Other configurations for the models can go here (optional)
        }
    }
}

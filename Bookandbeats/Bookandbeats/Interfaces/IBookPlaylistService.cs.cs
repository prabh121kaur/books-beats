using BookandBeats.Models;

    public interface IBookPlaylistService
    {
        Task<IEnumerable<BookPlaylist>> GetAllBookPlaylistsAsync(); // Retrieve all BookPlaylist records
        Task<BookPlaylist> GetBookPlaylistByIdAsync(int id);        // Retrieve a single BookPlaylist by its ID
        Task<BookPlaylist> AddBookPlaylistAsync(BookPlaylist bookPlaylist); // Add a new BookPlaylist
        Task<bool> UpdateBookPlaylistAsync(int id, BookPlaylist bookPlaylist); // Update an existing BookPlaylist
        Task<bool> DeleteBookPlaylistAsync(int id);                 // Delete a BookPlaylist
        Task<bool> BookPlaylistExistsAsync(int id);                 // Check if a BookPlaylist exists
    }


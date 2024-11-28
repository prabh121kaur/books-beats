using BookandBeats.Models;


    public interface IGenreService
    {
        Task<IEnumerable<Genre>> GetAllGenresAsync(); // Retrieve all genres
        Task<Genre> GetGenreByIdAsync(int id);        // Retrieve a genre by its ID
        Task<Genre> AddGenreAsync(Genre genre);       // Add a new genre
        Task<bool> UpdateGenreAsync(int id, Genre genre); // Update an existing genre
        Task<bool> DeleteGenreAsync(int id);          // Delete a genre
        Task<bool> GenreExistsAsync(int id);          // Check if a genre exists
    }


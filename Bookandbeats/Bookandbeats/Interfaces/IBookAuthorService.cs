using BookandBeats.Models;


    public interface IBookAuthorService
    {
        Task<IEnumerable<BookAuthor>> GetAllAsync();
        Task<BookAuthor> GetByIdAsync(int id);
        Task AddAsync(BookAuthor bookAuthor);
        Task UpdateAsync(BookAuthor bookAuthor);
        Task DeleteAsync(int id);
    }


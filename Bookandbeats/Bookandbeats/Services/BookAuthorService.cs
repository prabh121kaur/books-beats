using BookandBeats.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace BookandBeats.Services
{
    public class BookAuthorService : IBookAuthorService
    {
        private readonly AppDbContext _context;

        public BookAuthorService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<BookAuthor>> GetAllAsync()
        {
            return await _context.BookAuthors
                .Include(ba => ba.Book)
                .Include(ba => ba.Author)
                .ToListAsync();
        }

        public async Task<BookAuthor> GetByIdAsync(int id)
        {
            return await _context.BookAuthors
                .Include(ba => ba.Book)
                .Include(ba => ba.Author)
                .FirstOrDefaultAsync(ba => ba.BookAuthorId == id);
        }

        public async Task AddAsync(BookAuthor bookAuthor)
        {
            _context.BookAuthors.Add(bookAuthor);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(BookAuthor bookAuthor)
        {
            _context.BookAuthors.Update(bookAuthor);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var bookAuthor = await _context.BookAuthors.FindAsync(id);
            if (bookAuthor != null)
            {
                _context.BookAuthors.Remove(bookAuthor);
                await _context.SaveChangesAsync();
            }
        }
    }
}

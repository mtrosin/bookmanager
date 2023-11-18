using BookManager.API.Data;
using BookManager.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace BookManager.API.Repositories
{
    public class SQLBookRepository : IBookRepository
    {
        private readonly BookManagerDbContext dbContext;

        public SQLBookRepository(BookManagerDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Book> CreateAsync(Book book)
        {
            await dbContext.Books.AddAsync(book);
            await dbContext.SaveChangesAsync();
            return book;
        }

        public async Task<Book?> DeleteAsync(int id)
        {
            var existingBook = await dbContext.Books.FirstOrDefaultAsync(x => x.Id == id);

            if (existingBook == null)
            {
                return null;
            }

            dbContext.Books.Remove(existingBook);
            await dbContext.SaveChangesAsync();
            return existingBook;
        }

        public async Task<List<Book>> GetAllAsync()
        {
            return await dbContext.Books.ToListAsync();

        }

        public async Task<Book?> GetByIdAsync(int id)
        {
            return await dbContext.Books.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Book?> UpdateAsync(int id, Book book)
        {
            var existingBook = await dbContext.Books.FirstOrDefaultAsync(x => x.Id == id);

            if (existingBook == null)
            {
                return null;
            }

            existingBook.Title = book.Title;
            existingBook.Year = book.Year;
            existingBook.AuthorId = book.AuthorId;
            existingBook.CoverImage = book.CoverImage;

            await dbContext.SaveChangesAsync();
            return existingBook;
        }
    }
}

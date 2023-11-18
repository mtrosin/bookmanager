using BookManager.API.Data;
using BookManager.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace BookManager.API.Repositories
{
    public class SQLAuthorRepository : IAuthorRepository
    {
        private readonly BookManagerDbContext dbContext;

        public SQLAuthorRepository(BookManagerDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Author> CreateAsync(Author author)
        {
            await dbContext.Authors.AddAsync(author);
            await dbContext.SaveChangesAsync();
            return author;
        }

        public async Task<Author?> DeleteAsync(int id)
        {
            var existingAuthor = await dbContext.Authors.FirstOrDefaultAsync(x => x.Id == id);

            if (existingAuthor == null)
            {
                return null;
            }

            dbContext.Authors.Remove(existingAuthor);
            await dbContext.SaveChangesAsync();
            return existingAuthor;
        }

        public async Task<List<Author>> GetAllAsync()
        {
            return await dbContext.Authors.ToListAsync();
        }

        public async Task<Author?> GetByIdAsync(int id)
        {
            return await dbContext.Authors.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Author?> UpdateAsync(int id, Author author)
        {
            var existingAuthor = await dbContext.Authors.FirstOrDefaultAsync(x => x.Id == id);

            if(existingAuthor == null)
            {
                return null;
            }

            existingAuthor.Name = author.Name;

            await dbContext.SaveChangesAsync();
            return existingAuthor;
        }
    }
}

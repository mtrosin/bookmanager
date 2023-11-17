using BookManager.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace BookManager.API.Data
{
    public class BookManagerDbContext: DbContext
    {
        public BookManagerDbContext(DbContextOptions dbContextOptions): base(dbContextOptions) 
        {
            
        }

        public DbSet<Author> Authors { get; set; }
    }
}

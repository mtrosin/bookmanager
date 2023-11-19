using BookManager.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace BookManager.API.Data
{
    public class BookManagerDbContext : DbContext
    {
        public BookManagerDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {

        }
        public DbSet<Author> Authors { get; set; }

        public DbSet<Book> Books { get; set; }

        public DbSet<Image> Images { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var authors = new List<Author>()
            {
                new Author()
                {
                    Id = 1,
                    Name = "AuthorTest1",
                },
                new Author()
                {
                    Id = 2,
                    Name = "AuthorTest2",
                },
                new Author()
                {
                    Id = 3,
                    Name = "AuthorTest3",
                }
            };

            modelBuilder.Entity<Author>().HasData(authors);

            var books = new List<Book>()
            {
                new Book()
                {
                    Id = 1,
                    Title = "Book 1",
                    AuthorId = 1,
                    Year = 2010,
                    CoverImage = 1
                },
                new Book()
                {
                    Id = 2,
                    Title = "Book 2",
                    AuthorId = 2,
                    Year = 2011,
                    CoverImage = 1
                },
                new Book()
                {
                    Id = 3,
                    Title = "Book 3",
                    AuthorId = 3,
                    Year = 2012,
                    CoverImage = 1
                }
            };

            modelBuilder.Entity<Book>().HasData(books);
        }
    }
}

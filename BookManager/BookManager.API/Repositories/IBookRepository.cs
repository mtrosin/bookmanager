using BookManager.API.Models.Domain;

namespace BookManager.API.Repositories
{
    public interface IBookRepository
    {
        Task<List<Book>> GetAllAsync();

        Task<Book?> GetByIdAsync(int id);

        Task<Book> CreateAsync(Book book);

        Task<Book?> UpdateAsync(int id, Book book);

        Task<Book?> DeleteAsync(int id);
    }
}

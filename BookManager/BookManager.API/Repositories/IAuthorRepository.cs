using BookManager.API.Models.Domain;

namespace BookManager.API.Repositories
{
    public interface IAuthorRepository
    {
        Task<List<Author>> GetAllAsync();

        Task<Author?> GetByIdAsync(int id);

        Task<Author> CreateAsync(Author author);

        Task<Author?> UpdateAsync(int id, Author author);

        Task<Author?> DeleteAsync(int id);
    }
}

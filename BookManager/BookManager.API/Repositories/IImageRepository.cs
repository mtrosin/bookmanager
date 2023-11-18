using BookManager.API.Models.Domain;

namespace BookManager.API.Repositories
{
    public interface IImageRepository
    {
        Task<Image> Upload(Image image);
    }
}

using BookManager.API.Models.Domain;

namespace BookManager.API.Models.DTO
{
    public class AuthorDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Book?> Books { get; }
    }
}

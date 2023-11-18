using BookManager.API.Models.Domain;

namespace BookManager.API.Models.DTO
{
    public class AddBookRequestDto
    {
        public string Title { get; set; }
        public int AuthorId { get; set; }
        public int Year { get; set; }
        public string? CoverImage { get; set; }
    }
}

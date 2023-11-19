using BookManager.API.Models.Domain;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookManager.API.Models.DTO
{
    public class BookDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int AuthorId { get; set; }
        public int Year { get; set; }
        public int? CoverImage { get; set; }

        public Author Author { get; set; }
    }
}

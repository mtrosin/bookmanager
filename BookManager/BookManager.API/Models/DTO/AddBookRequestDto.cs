using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookManager.API.Models.DTO
{
    public class AddBookRequestDto
    {
        [Required]
        [MinLength(3, ErrorMessage = "Title has to be a minimum of 3 characters")]
        [MaxLength(60, ErrorMessage = "Title has to be a maximum of 60 characters")]
        public string Title { get; set; }

        [Required]
        public int AuthorId { get; set; }

        [Required]
        public int Year { get; set; }

        public int? CoverImage { get; set; }
    }
}

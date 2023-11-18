using System.ComponentModel.DataAnnotations;

namespace BookManager.API.Models.DTO
{
    public class AddAuthorRequestDto
    {
        [Required]
        [MinLength(3, ErrorMessage = "Name has to be a minimum of 3 characters")]
        [MaxLength(60, ErrorMessage = "Name has to be a maximum of 60 characters")]
        public string Name { get; set; }
    }
}

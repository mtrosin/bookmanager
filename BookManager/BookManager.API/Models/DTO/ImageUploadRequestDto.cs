using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookManager.API.Models.DTO
{
    public class ImageUploadRequestDto
    {
        [NotMapped]
        public IFormFile File { get; set; }

        [Required]
        public string Filename { get; set; }
        public string? FileDescription { get; set; }
    }
}

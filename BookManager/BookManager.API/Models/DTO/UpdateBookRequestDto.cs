namespace BookManager.API.Models.DTO
{
    public class UpdateBookRequestDto
    {
        public string Title { get; set; }
        public int AuthorId { get; set; }
        public int Year { get; set; }
        public int? CoverImage { get; set; }
    }
}

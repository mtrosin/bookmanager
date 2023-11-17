namespace BookManager.API.Models.Domain
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int AuthorId { get; set; }
        public int Year {  get; set; }
        public string? CoverImage { get; set; }

        public Author Author { get; set; }
    }
}

using AutoMapper;
using BookManager.API.Models.Domain;
using BookManager.API.Models.DTO;

namespace BookManager.API.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Author, AuthorDto>().ReverseMap();
            CreateMap<AddAuthorRequestDto, Author>().ReverseMap();
            CreateMap<UpdateAuthorRequestDto, Author>().ReverseMap();

            CreateMap<Book, BookDto>().ReverseMap();
            CreateMap<AddBookRequestDto, Book>().ReverseMap();
            CreateMap<UpdateBookRequestDto, Book>().ReverseMap();
        }
    }
}

using AutoMapper;
using BookManager.API.Controllers;
using BookManager.API.Data;
using BookManager.API.Models.Domain;
using BookManager.API.Models.DTO;
using BookManager.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace BookManager.API.Tests
{
    public class BooksControllerTest
    {
        private Mock<BookManagerDbContext> dbContextMock;
        private Mock<IBookRepository> bookRepositoryMock;
        private Mock<IMapper> mapperMock;
        private BooksController booksController;

        public BooksControllerTest()
        {
            this.dbContextMock = new Mock<BookManagerDbContext>();
            this.bookRepositoryMock = new Mock<IBookRepository>();
            this.mapperMock = new Mock<IMapper>();

            this.booksController = new BooksController(dbContextMock.Object, bookRepositoryMock.Object, mapperMock.Object);
        }

        [Fact]
        public async Task GetAll_ShouldReturnOkResultWithBookDtos()
        {
            var booksDomainModel = new List<Book>();
            bookRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(booksDomainModel);

            var bookDtos = new List<BookDto>();
            mapperMock.Setup(m => m.Map<List<BookDto>>(booksDomainModel)).Returns(bookDtos);

            var result = await booksController.GetAll();

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedBookDtos = Assert.IsType<List<BookDto>>(okResult.Value);
            Assert.Equal(bookDtos, returnedBookDtos);
        }

        [Fact]
        public async Task GetById_ShouldReturnOkResultWithBookDto()
        {
            var bookDomainModel = new Book();
            bookRepositoryMock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(bookDomainModel);

            var bookDtos = new BookDto();
            mapperMock.Setup(m => m.Map<BookDto>(bookDomainModel)).Returns(bookDtos);

            var result = await booksController.GetById(1);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedBookDto = Assert.IsType<BookDto>(okResult.Value);
            Assert.Equal(bookDtos, returnedBookDto);
        }

        [Fact]
        public async Task Create_ShouldReturnCreatedAtAction()
        {
            var addBookRequestDto = new AddBookRequestDto
            {
                AuthorId = 1,
                CoverImage = 1,
                Title = "Test",
                Year = 2010
            };

            var bookDomainModel = new Book
            {
                Id = 1,
                AuthorId = 1,
                CoverImage = 1,
                Title = "Test",
                Year = 2010
            };

            mapperMock.Setup(m => m.Map<Book>(addBookRequestDto)).Returns(bookDomainModel);
            bookRepositoryMock.Setup(repo => repo.CreateAsync(It.IsAny<Book>())).ReturnsAsync(bookDomainModel);
            mapperMock.Setup(m => m.Map<BookDto>(bookDomainModel)).Returns(new BookDto {
                Id = 1,
                AuthorId = 1,
                CoverImage = 1,
                Title = "Test",
                Year = 2010
            });

            var result = await booksController.Create(addBookRequestDto);

            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            var bookDto = Assert.IsType<BookDto>(createdAtActionResult.Value);

            Assert.Equal(bookDomainModel.Id, bookDto.Id);
            Assert.Equal(bookDomainModel.Title, bookDto.Title);
            Assert.Equal(bookDomainModel.AuthorId, bookDto.AuthorId);
            Assert.Equal(bookDomainModel.Year, bookDto.Year);
            Assert.Equal(bookDomainModel.CoverImage, bookDto.CoverImage);
        }

        [Fact]
        public async Task Update_ShouldReturnOkResultWithUpdatedBookDto()
        {
            var updateBookRequestDto = new UpdateBookRequestDto
            {
                AuthorId = 1,
                CoverImage = 1,
                Title = "Test",
                Year = 2010
            };

            var bookDomainModel = new Book
            {
                Id = 1,
                AuthorId = 1,
                CoverImage = 1,
                Title = "Test",
                Year = 2010
            };

            mapperMock.Setup(m => m.Map<Book>(updateBookRequestDto)).Returns(bookDomainModel);
            bookRepositoryMock.Setup(repo => repo.UpdateAsync(It.IsAny<int>(), It.IsAny<Book>())).ReturnsAsync(bookDomainModel);
            mapperMock.Setup(m => m.Map<BookDto>(bookDomainModel)).Returns(new BookDto {
                Id = 1,
                AuthorId = 1,
                CoverImage = 1,
                Title = "Test",
                Year = 2010
            });

            var result = await booksController.Update(1, updateBookRequestDto);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var updatedBookDto = Assert.IsType<BookDto>(okResult.Value);

            Assert.Equal(bookDomainModel.Id, updatedBookDto.Id);
            Assert.Equal(bookDomainModel.Title, updatedBookDto.Title);
            Assert.Equal(bookDomainModel.AuthorId, updatedBookDto.AuthorId);
            Assert.Equal(bookDomainModel.Year, updatedBookDto.Year);
            Assert.Equal(bookDomainModel.CoverImage, updatedBookDto.CoverImage);
        }

        [Fact]
        public async Task Update_ShouldReturnNotFoundWhenBookNotFound()
        {
            var updateBookRequestDto = new UpdateBookRequestDto
            {
                AuthorId = 1,
                CoverImage = 1,
                Title = "Test",
                Year = 2010
            };

            bookRepositoryMock.Setup(repo => repo.UpdateAsync(It.IsAny<int>(), It.IsAny<Book>())).ReturnsAsync((Book)null);

            var result = await booksController.Update(1, updateBookRequestDto);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Delete_ShouldReturnOkResultWithDeletedBookDto()
        {
            var bookDomainModel = new Book
            {
                Id = 1
            };

            bookRepositoryMock.Setup(repo => repo.DeleteAsync(It.IsAny<int>())).ReturnsAsync(bookDomainModel);
            mapperMock.Setup(m => m.Map<BookDto>(bookDomainModel)).Returns(new BookDto { Id = 1 });

            var result = await booksController.Delete(1);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var deletedBookDto = Assert.IsType<BookDto>(okResult.Value);

            Assert.Equal(bookDomainModel.Id, deletedBookDto.Id);
        }

        [Fact]
        public async Task Delete_ShouldReturnNotFoundWhenBookNotFound()
        {
            bookRepositoryMock.Setup(repo => repo.DeleteAsync(It.IsAny<int>())).ReturnsAsync((Book)null);

            var result = await booksController.Delete(1);

            Assert.IsType<NotFoundResult>(result);
        }

    }
}
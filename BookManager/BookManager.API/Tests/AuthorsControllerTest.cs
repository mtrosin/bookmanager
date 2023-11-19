using AutoMapper;
using BookManager.API.Controllers;
using BookManager.API.Data;
using BookManager.API.Models.Domain;
using BookManager.API.Models.DTO;
using BookManager.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace BookManager.API.Tests
{
    public class AuthorsControllerTest
    {
        private Mock<BookManagerDbContext> dbContextMock;
        private Mock<IAuthorRepository> authorRepositoryMock;
        private Mock<IMapper> mapperMock;
        private AuthorsController authorsController;

        public AuthorsControllerTest()
        {
            var dbContextOptions = new DbContextOptionsBuilder<BookManagerDbContext>()
                    .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // You can use an in-memory database for testing
                    .Options;

            this.dbContextMock = new Mock<BookManagerDbContext>(dbContextOptions);
            this.authorRepositoryMock = new Mock<IAuthorRepository>();
            this.mapperMock = new Mock<IMapper>();

            this.authorsController = new AuthorsController(dbContextMock.Object, authorRepositoryMock.Object, mapperMock.Object);
        }

        [Fact]
        public async Task GetAll_ShouldReturnOkResultWithAuthorDtos()
        {
            var authorsDomainModel = new List<Author>();
            authorRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(authorsDomainModel);

            var authorDtos = new List<AuthorDto>(); 
            mapperMock.Setup(m => m.Map<List<AuthorDto>>(authorsDomainModel)).Returns(authorDtos);

            var result = await authorsController.GetAll();

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedAuthorDtos = Assert.IsType<List<AuthorDto>>(okResult.Value);
            Assert.Equal(authorDtos, returnedAuthorDtos);
        }

        [Fact]
        public async Task GetById_ShouldReturnOkResultWithAuthorDto()
        {
            var authorDomainModel = new Author(); 
            authorRepositoryMock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(authorDomainModel);

            var authorDtos = new AuthorDto(); 
            mapperMock.Setup(m => m.Map<AuthorDto>(authorDomainModel)).Returns(authorDtos);

            var result = await authorsController.GetById(1);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedAuthorDto = Assert.IsType<AuthorDto>(okResult.Value);
            Assert.Equal(authorDtos, returnedAuthorDto);
        }

        [Fact]
        public async Task Create_ShouldReturnCreatedAtAction()
        {
            var addAuthorRequestDto = new AddAuthorRequestDto
            {
                Name = "Test",
            };

            var authorDomainModel = new Author
            {
                Id = 1,
                Name = "Test", 
            };

            mapperMock.Setup(m => m.Map<Author>(addAuthorRequestDto)).Returns(authorDomainModel);
            authorRepositoryMock.Setup(repo => repo.CreateAsync(It.IsAny<Author>())).ReturnsAsync(authorDomainModel);
            mapperMock.Setup(m => m.Map<AuthorDto>(authorDomainModel)).Returns(new AuthorDto { Id = 1, Name = "Test" });

            var result = await authorsController.Create(addAuthorRequestDto);

            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            var authorDto = Assert.IsType<AuthorDto>(createdAtActionResult.Value);

            Assert.Equal(authorDomainModel.Id, authorDto.Id);
            Assert.Equal(authorDomainModel.Name, authorDto.Name);
        }

        [Fact]
        public async Task Update_ShouldReturnOkResultWithUpdatedAuthorDto()
        {
            var updateAuthorRequestDto = new UpdateAuthorRequestDto
            {
                Name = "Test",
            };

            var authorDomainModel = new Author
            {
                Id = 1,
                Name = "Test",
            };

            mapperMock.Setup(m => m.Map<Author>(updateAuthorRequestDto)).Returns(authorDomainModel);
            authorRepositoryMock.Setup(repo => repo.UpdateAsync(It.IsAny<int>(), It.IsAny<Author>())).ReturnsAsync(authorDomainModel);
            mapperMock.Setup(m => m.Map<AuthorDto>(authorDomainModel)).Returns(new AuthorDto { Id = 1, Name = "Test" });

            var result = await authorsController.Update(1, updateAuthorRequestDto);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var updatedAuthorDto = Assert.IsType<AuthorDto>(okResult.Value);

            Assert.Equal(authorDomainModel.Id, updatedAuthorDto.Id);
            Assert.Equal(authorDomainModel.Name, updatedAuthorDto.Name);
        }

        [Fact]
        public async Task Update_ShouldReturnNotFoundWhenAuthorNotFound()
        {
            var updateAuthorRequestDto = new UpdateAuthorRequestDto
            {
                Name = "Test",
            };

            authorRepositoryMock.Setup(repo => repo.UpdateAsync(It.IsAny<int>(), It.IsAny<Author>())).ReturnsAsync((Author)null);

            var result = await authorsController.Update(1, updateAuthorRequestDto);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Delete_ShouldReturnOkResultWithDeletedAuthorDto()
        {
            var authorDomainModel = new Author
            {
                Id = 1
            };

            authorRepositoryMock.Setup(repo => repo.DeleteAsync(It.IsAny<int>())).ReturnsAsync(authorDomainModel);
            mapperMock.Setup(m => m.Map<AuthorDto>(authorDomainModel)).Returns(new AuthorDto { Id = 1 });

            var result = await authorsController.Delete(1);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var deletedAuthorDto = Assert.IsType<AuthorDto>(okResult.Value);

            Assert.Equal(authorDomainModel.Id, deletedAuthorDto.Id);
            Assert.Equal(authorDomainModel.Name, deletedAuthorDto.Name);
        }

        [Fact]
        public async Task Delete_ShouldReturnNotFoundWhenAuthorNotFound()
        {
            authorRepositoryMock.Setup(repo => repo.DeleteAsync(It.IsAny<int>())).ReturnsAsync((Author)null);

            var result = await authorsController.Delete(1);

            Assert.IsType<NotFoundResult>(result);
        }

    }
}
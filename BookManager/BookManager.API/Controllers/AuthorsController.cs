using BookManager.API.Data;
using BookManager.API.Models.Domain;
using BookManager.API.Models.DTO;
using BookManager.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly BookManagerDbContext dbContext;
        private readonly IAuthorRepository authorRepository;

        public AuthorsController(BookManagerDbContext dbContext, IAuthorRepository authorRepository)
        {
            this.dbContext = dbContext;
            this.authorRepository = authorRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var authorsDomainModel = await authorRepository.GetAllAsync();

            List<AuthorDto> authorsDto = new List<AuthorDto>();
            foreach (Author authorDomainModel in authorsDomainModel)
            {
                authorsDto.Add(
                    new AuthorDto()
                    {
                        Id = authorDomainModel.Id,
                        Name = authorDomainModel.Name,
                    }
                );
            }

            return Ok(authorsDto);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var authorDomainModel = await authorRepository.GetByIdAsync(id);

            if (authorDomainModel == null)
            {
                return NotFound();
            }

            AuthorDto authorDto = new AuthorDto
            {
                Id = authorDomainModel.Id,
                Name = authorDomainModel.Name,
            };

            return Ok(authorDto);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddAuthorRequestDto addAuthorRequestDto)
        {
            var authorDomainModel = new Author
            {
                Name = addAuthorRequestDto.Name,
            };

            authorDomainModel = await authorRepository.CreateAsync(authorDomainModel);

            AuthorDto authorDto = new AuthorDto
            {
                Id = authorDomainModel.Id, 
                Name = authorDomainModel.Name,
            };

            return CreatedAtAction(nameof(GetById), new { id = authorDto.Id }, authorDto);
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateAuthorRequestDto updateAuthorRequestDto)
        {
            var authorDomainModel = new Author
            {
                Name = updateAuthorRequestDto.Name,
            };

            authorDomainModel = await authorRepository.UpdateAsync(id, authorDomainModel);

            if(authorDomainModel == null)
            {
                return NotFound();
            }

            AuthorDto authorDto = new AuthorDto
            {
                Id = authorDomainModel.Id,
                Name = authorDomainModel.Name,
            };

            return Ok(authorDto);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var authorDomainModel = await authorRepository.DeleteAsync(id);

            if(authorDomainModel == null) 
            {
                return NotFound();
            }

            var authorDto = new AuthorDto
            {
                Id = authorDomainModel.Id,
                Name = authorDomainModel.Name,
            };

            return Ok(authorDto);
        }
    }
}

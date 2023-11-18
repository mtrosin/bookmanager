using BookManager.API.Data;
using BookManager.API.Models.Domain;
using BookManager.API.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly BookManagerDbContext dbContext;
        public AuthorsController(BookManagerDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            List<Author> authorsDomainModel = dbContext.Authors.ToList();

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
        public IActionResult GetById([FromRoute] int id)
        {
            Author authorDomainModel = dbContext.Authors.Find(id);

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

        [HttpPost]
        public IActionResult Create([FromBody] AddAuthorRequestDto addAuthorRequestDto)
        {
            Author authorDomainModel = new Author
            {
                Name = addAuthorRequestDto.Name,
            };

            dbContext.Authors.Add(authorDomainModel);
            dbContext.SaveChanges();

            AuthorDto authorDto = new AuthorDto
            {
                Id = authorDomainModel.Id, 
                Name = authorDomainModel.Name,
            };

            return CreatedAtAction(nameof(GetById), new { id = authorDto.Id }, authorDto);
        }

        [HttpPut]
        [Route("{id:int}")]
        public IActionResult Update([FromRoute] int id, [FromBody] UpdateAuthorRequestDto updateAuthorRequestDto)
        {
            Author authorDomainModel = dbContext.Authors.Find(id);

            if(authorDomainModel == null)
            {
                return NotFound();
            }

            authorDomainModel.Name = updateAuthorRequestDto.Name;

            dbContext.SaveChanges();

            AuthorDto authorDto = new AuthorDto
            {
                Id = authorDomainModel.Id,
                Name = authorDomainModel.Name,
            };

            return Ok(authorDto);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public IActionResult Delete([FromRoute] int id)
        {
            Author authorDomainModel = dbContext.Authors.Find(id);

            if(authorDomainModel == null) 
            {
                return NotFound();
            }

            dbContext.Authors.Remove(authorDomainModel);
            dbContext.SaveChanges();

            return Ok();
        }
    }
}

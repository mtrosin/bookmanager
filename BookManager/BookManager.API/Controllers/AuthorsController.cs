using BookManager.API.Data;
using BookManager.API.Models.Domain;
using BookManager.API.Models.DTO;
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
        public AuthorsController(BookManagerDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var authorsDomainModel = await dbContext.Authors.ToListAsync();

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
            var authorDomainModel = await dbContext.Authors.FirstOrDefaultAsync(x => x.Id == id);

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
        public async Task<IActionResult> Create([FromBody] AddAuthorRequestDto addAuthorRequestDto)
        {
            var authorDomainModel = new Author
            {
                Name = addAuthorRequestDto.Name,
            };

            await dbContext.Authors.AddAsync(authorDomainModel);
            await dbContext.SaveChangesAsync();

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
            var authorDomainModel = await dbContext.Authors.FirstOrDefaultAsync(x => x.Id == id);

            if(authorDomainModel == null)
            {
                return NotFound();
            }

            authorDomainModel.Name = updateAuthorRequestDto.Name;

            await dbContext.SaveChangesAsync();

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
            var authorDomainModel = await dbContext.Authors.FirstOrDefaultAsync(x => x.Id == id);

            if(authorDomainModel == null) 
            {
                return NotFound();
            }

            dbContext.Authors.Remove(authorDomainModel);
            await dbContext.SaveChangesAsync();

            return Ok();
        }
    }
}

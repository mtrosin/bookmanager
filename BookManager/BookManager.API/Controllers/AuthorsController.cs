using AutoMapper;
using BookManager.API.Data;
using BookManager.API.Models.Domain;
using BookManager.API.Models.DTO;
using BookManager.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace BookManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly BookManagerDbContext dbContext;
        private readonly IAuthorRepository authorRepository;
        private readonly IMapper mapper;

        public AuthorsController(BookManagerDbContext dbContext, IAuthorRepository authorRepository,
            IMapper mapper)
        {
            this.dbContext = dbContext;
            this.authorRepository = authorRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var authorsDomainModel = await authorRepository.GetAllAsync();

            return Ok(mapper.Map<List<AuthorDto>>(authorsDomainModel));
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

            return Ok(mapper.Map<AuthorDto>(authorDomainModel));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddAuthorRequestDto addAuthorRequestDto)
        {
            var authorDomainModel = mapper.Map<Author>(addAuthorRequestDto);

            authorDomainModel = await authorRepository.CreateAsync(authorDomainModel);

            AuthorDto authorDto = mapper.Map<AuthorDto>(authorDomainModel);

            return CreatedAtAction(nameof(GetById), new { id = authorDto.Id }, authorDto);
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateAuthorRequestDto updateAuthorRequestDto)
        {
            var authorDomainModel = mapper.Map<Author>(updateAuthorRequestDto);

            authorDomainModel = await authorRepository.UpdateAsync(id, authorDomainModel);

            if(authorDomainModel == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<AuthorDto>(authorDomainModel));
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

            return Ok(mapper.Map<AuthorDto>(authorDomainModel));
        }
    }
}

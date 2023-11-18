using AutoMapper;
using BookManager.API.CustomActionFilters;
using BookManager.API.Data;
using BookManager.API.Models.Domain;
using BookManager.API.Models.DTO;
using BookManager.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BookManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly BookManagerDbContext dbContext;
        private readonly IBookRepository bookRepository;
        private readonly IMapper mapper;

        public BooksController(BookManagerDbContext dbContext, IBookRepository bookRepository,
            IMapper mapper)
        {
            this.dbContext = dbContext;
            this.bookRepository = bookRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var booksDomainModel = await bookRepository.GetAllAsync();

            return Ok(mapper.Map<List<BookDto>>(booksDomainModel));
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var bookDomainModel = await bookRepository.GetByIdAsync(id);

            if (bookDomainModel == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<BookDto>(bookDomainModel));
        }

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody] AddBookRequestDto addBookRequestDto)
        {
            var bookDomainModel = mapper.Map<Book>(addBookRequestDto);

            bookDomainModel = await bookRepository.CreateAsync(bookDomainModel);

            BookDto bookDto = mapper.Map<BookDto>(bookDomainModel);

            return CreatedAtAction(nameof(GetById), new { id = bookDto.Id }, bookDto);
        }

        [HttpPut]
        [ValidateModel]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateBookRequestDto updateBookRequestDto)
        {
            var bookDomainModel = mapper.Map<Book>(updateBookRequestDto);

            bookDomainModel = await bookRepository.UpdateAsync(id, bookDomainModel);

            if (bookDomainModel == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<BookDto>(bookDomainModel));
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var bookDomainModel = await bookRepository.DeleteAsync(id);

            if (bookDomainModel == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<BookDto>(bookDomainModel));
        }
    }
}

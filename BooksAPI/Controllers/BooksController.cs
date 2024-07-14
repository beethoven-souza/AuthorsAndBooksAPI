using Domain.Example;
using Domain.Models;
using Infrastructure.Data;
using Infrastructure.ServicesInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Filters;

namespace BooksAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;
        private readonly LibraryContext _context;
        private readonly BookValidations _validationHelper;

        public BooksController(IBookService bookService, LibraryContext context, BookValidations validationHelper)
        {
            _bookService = bookService ?? throw new ArgumentNullException(nameof(bookService));
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _validationHelper = validationHelper ?? throw new ArgumentNullException(nameof(validationHelper));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetBooks()
        {
            var books = await _bookService.GetAllBooksAsync();
            if(!books.Any())
            {
                return BadRequest("Não há livros cadastrados.");
            }
            return Ok(books);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBook(int id)
        {
            var book = await _bookService.GetBookByIdAsync(id);

            if (book == null)
            {
                return NotFound("Livro não cadastrado.");
            }

            return book;
        }

        [HttpPost]
        [SwaggerRequestExample(typeof(Book), typeof(BookExample))]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<ActionResult<Book>> PostBook(Book book)
        {

            var errors = await _validationHelper.ValidateBook(book);

            if (errors.Any())
            {
                return BadRequest(new { Message = "Existem erros a serem trtados.", Errors = errors });
            }

            _context.Books.Add(book);

            try
            {
                await _context.SaveChangesAsync();
                return CreatedAtAction("GetBook", new { id = book.Id }, book);
            }
            catch
            {
                return StatusCode(500, "Ocorreu um erro ao salvar as alterações.");

            }
        }

        [HttpPut("{id}")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> PutBook(int id, Book book)
        {
            if (id != book.Id)
            {
                return BadRequest("ID do livro não corresponde.");
            }

            var existingBook = await _context.Books
                                     .Include(b => b.Author)
                                     .FirstOrDefaultAsync(b => b.Id == id);

            if (existingBook == null)
            {
                return NotFound("Livro não encontrado.");
            }

            existingBook.Title = book.Title;

            try
            {
                await _context.SaveChangesAsync();
                return Ok("Livro atualizado com sucesso.");
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, $"Ocorreu um erro ao salvar as alterações: {ex.InnerException?.Message ?? ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ocorreu um erro ao salvar as alterações: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();

            return Ok("Livro removido com sucesso!");
        }
    }
}

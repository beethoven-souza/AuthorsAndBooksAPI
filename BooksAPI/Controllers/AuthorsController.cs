using Domain.DTO;
using Domain.Models;
using Infrastructure.ServicesInterfaces;
using Infrastructure.Validations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BooksAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorService _authorService;
        private readonly AuthorValidations _validationHelper;

        public AuthorsController(IAuthorService authorService)
        {
            _authorService = authorService;
            _validationHelper = new AuthorValidations(authorService); // Injetando o serviço
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Author>>> GetAuthors()
        {
            var authors = await _authorService.GetAllAuthorsAsync();

            if (!authors.Any())
            {
                return NotFound("Nenhum Autor cadastrado!");
            }
            return Ok(authors);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Author>> GetAuthor(int id)
        {
            var author = await _authorService.GetAuthorByIdAsync(id);

            if (author == null)
            {
                return NotFound("Autor não encontrado!");
            }
            return Ok(author);
        }

        [HttpGet("books/{authorId}")]
        public async Task<ActionResult<IEnumerable<Book>>> GetBooksByAuthorId(int authorId)
        {
            var books = await _authorService.GetBooksByAuthorIdAsync(authorId);

            if (books == null || !books.Any())
            {
                return NotFound("Autor não encontrado!");
            }

            return Ok(books);
        }

        [HttpPost]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<ActionResult<Author>> PostAuthor(AuthorInputModel inputModel)
        {
            var errors = await _validationHelper.ValidateAuthor(inputModel);

            if (errors.Any())
            {
                return BadRequest(new { Message = "Existem erros a serem trtados.", Errors = errors });
            }

            var newAuthor = new Author
            {
                Name = inputModel.Name
            };

            if (inputModel.Books != null && inputModel.Books.Any())
            {
                newAuthor.Books = inputModel.Books?.Select(b => new Book { Title = b.Title }).ToList();
            }
            else{
                newAuthor.Books = new List<Book>();
            }

            await _authorService.AddAuthorAsync(newAuthor);
            return Ok("Autor cadastrado com sucesso!");
        }

        [HttpPut("{id}")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> PutAuthor(int id, Author author)
        {
            if (id != author.Id)
            {
                return BadRequest("Preencha o Id do Author");
            }

            var existingAuthor = await _authorService.GetAuthorByIdAsync(id);
            if (existingAuthor == null)
            {
                return NotFound("Autor não encontrado.");
            }
            else
            {
                existingAuthor.Name = author.Name;

                await _authorService.UpdateAuthorAsync(existingAuthor);
                return Ok("Autor atualizado com sucesso!");
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            var author = await _authorService.GetAuthorByIdAsync(id);

            if (author == null)
            {
                return NotFound("Não é possível excluir. Autor não encontrado!");
            }
            else
            {
                await _authorService.DeleteAuthorAsync(id);
                return Ok("Author excluído com sucesso!");
            }
        }
    }
}

using Domain.Models;
using Domain.RepositoriesInterfaces;
using Infrastructure.Data;
using Infrastructure.ServicesInterfaces;

public class BookValidations
{
    private readonly IBookService _bookService;
    private readonly LibraryContext _context;
    private readonly IRepository<Book> _bookRepository;

    public BookValidations(IBookService bookService, LibraryContext context, IRepository<Book> bookRepository)
    {
        _bookService = bookService ?? throw new ArgumentNullException(nameof(bookService));
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _bookRepository = bookRepository ?? throw new ArgumentNullException(nameof(bookRepository));
    }

    public async Task<List<string>> ValidateBook(Book book)
    {
        var errors = new List<string>();

        // Validação para Title
        if (string.IsNullOrWhiteSpace(book.Title))
        {
            errors.Add("O campo Título é obrigatório.");
        }
        else if (book.Title.Length < 3)
        {
            errors.Add("O campo Título deve ter no mínimo 3 caracteres.");
        }

        // Verifica se o Book já existe
        var title = book.Title;
        var existingBook = await _bookRepository.ExistsBookByNameAsync(title);
        if (existingBook)
        {
            errors.Add("Livro já existe.");
        }

        if (book.AuthorId == 0)
        {
            if (book.Author != null)
            {
                _context.Authors.Add(book.Author);

                book.AuthorId = book.Author.Id;
            }
            else
            {
                errors.Add("Informações do Autor são necessárias para criar um novo Autor a partir de um novo Livro.");
            }
        }
        else
        {
            // Se AuthorId for fornecido, busca o autor existente
            var existingAuthor = await _context.Authors.FindAsync(book.AuthorId);
            if (existingAuthor == null)
            {
                errors.Add($"Autor com ID {book.AuthorId} não encontrado.");
            }
            else
            {
                // Associa o autor existente ao livro
                book.Author = existingAuthor;
            }
        }

        return errors;
    }
}

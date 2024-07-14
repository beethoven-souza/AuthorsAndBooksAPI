using Domain.Models;
using Infrastructure.Data;
using Domain.RepositoriesInterfaces;
using Infrastructure.ServicesInterfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services
{
    public class BookService : IBookService
    {
        private readonly IRepository<Book> _bookRepository;
        private readonly LibraryContext _context;

        public BookService(IRepository<Book> bookRepository, LibraryContext context)
        {
            _bookRepository = bookRepository;
            _context = context;

        }

        public async Task<IEnumerable<Book>> GetAllBooksAsync()
        {
            return await _bookRepository.GetAllAsync();
        }

        public async Task<Book> GetBookByIdAsync(int id)
        {
            return await _bookRepository.GetByIdAsync(id);
        }

        public async Task AddBookAsync(Book book)
        {
            await _bookRepository.AddAsync(book);
        }

        public async Task UpdateBookAsync(Book book)
        {
            await _bookRepository.UpdateAsync(book);
        }

        public async Task DeleteBookAsync(int id)
        {
            await _bookRepository.DeleteAsync(id);
        }

        public async Task<Book> GetBookByNameAsync(string title)
        {
            return await _context.Books.FirstOrDefaultAsync(a => a.Title == title);
        }
    }
}

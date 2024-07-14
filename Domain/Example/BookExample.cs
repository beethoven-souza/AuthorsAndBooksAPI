using Domain.Models;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Example
{
    public class BookExample : IExamplesProvider<Book>
    {
        public Book GetExamples()
        {
            return new Book
            {
                Id = 0,
                Title = "Exemplo de Título de Livro",
                AuthorId = 0,
                Author = new Author
                {
                    Id = 0,
                    Name = "Exemplo de Nome de Autor",
                    // Books não será incluído no exemplo
                }
            };
        }
    }
}

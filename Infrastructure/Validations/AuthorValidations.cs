using Domain.DTO;
using Infrastructure.ServicesInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Validations
{
    public class AuthorValidations
    {
        private readonly IAuthorService _authorService;

        public AuthorValidations(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        public async Task<List<string>> ValidateAuthor(AuthorInputModel inputModel)
        {
            var errors = new List<string>();

            // Validação para Author
            if (string.IsNullOrWhiteSpace(inputModel.Name))
            {
                errors.Add("O campo Nome é obrigatório.");
            }
            else if (inputModel.Name.Length < 3)
            {
                errors.Add("O campo Nome deve ter no mínimo 3 caracteres.");
            }

            // Verifica se o autor já existe
            var existingAuthor = await _authorService.GetAuthorByNameAsync(inputModel.Name);
            if (existingAuthor != null)
            {
                errors.Add("Autor já existe.");
            }

            return errors;
        }
    }
}

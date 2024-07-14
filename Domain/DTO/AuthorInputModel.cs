using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO
{
    public class AuthorInputModel
    {
        [MaxLength(50, ErrorMessage = "Máximo de 50 caracteres.")]
        public string Name { get; set; }

        public List<BookInputModel> Books { get; set; }
    }
}

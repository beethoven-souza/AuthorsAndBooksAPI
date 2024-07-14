using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO
{
    public class BookInputModel
    {

        [MaxLength(100, ErrorMessage = "Máximo de 100 caracteres")]
        public string Title { get; set; }
        public int AuthorId { get; set; }

    }
}

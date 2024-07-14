using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Author
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(50, ErrorMessage = "Máximo de 50 caracteres.")]
        public string Name { get; set; }

        [JsonIgnore]
        public List<Book> Books { get; set; }
    }
}

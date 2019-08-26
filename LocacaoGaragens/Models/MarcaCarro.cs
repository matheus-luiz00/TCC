using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LocacaoGaragens.Models
{
    public class MarcaCarro
    {
        [Required]
        [Key]
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }
    }
}
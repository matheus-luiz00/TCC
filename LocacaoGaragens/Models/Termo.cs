using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LocacaoGaragens.Models
{
    public class Termo
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Texto { get; set; }

        [Required]
        public DateTime DataPublicacao { get; set; }

        public bool Ativo { get; set; } = true;
    }
}
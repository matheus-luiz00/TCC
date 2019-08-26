using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LocacaoGaragens.Models
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }
        public int IdRegistration { get; set; }
        public string Email { get; set; }
        public bool Pcd { get; set; }
        public bool TrabalhoNot { get; set; }

    }
}
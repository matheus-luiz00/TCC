using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LocacaoGaragens.Models
{
    public class Locacao
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int TipoVeiculo { get; set; }

        public int Marca { get; set; }
        public int Modelo { get; set; }
        public int Cor { get; set; }
        public string Placa { get; set; }

        [ForeignKey("PeriodoLocacao")]
        public int Periodo { get; set; }
        public int Usuario { get; set; }
        public bool TermoAceito { get; set; }

        public PeriodoLocacao PeriodoLocacao { get; set; }
    }
}
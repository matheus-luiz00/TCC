using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LocacaoGaragens.Models
{
    public class PeriodoLocacao
    {
        [Key]
        public int Id { get; set; }

        public virtual TipoVeiculo TipoVeiculo { get; set; }

        public DateTime DataInicial { get; set; }

        public DateTime DataFinal { get; set; }
    }
}
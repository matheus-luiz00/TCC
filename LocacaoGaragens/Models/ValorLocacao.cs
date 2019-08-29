using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LocacaoGaragens.Models
{
    public class ValorLocacao
    {
        public int Id { get; set; }

        [Required]
        public decimal Valor { get; set; }

        [JsonIgnore]
        [ForeignKey("TipoVeiculoFK")]
        public TipoVeiculo TipoVeiculo { get; set; }
        [Required]
        public int TipoVeiculoFK { get; set; }


        public bool Ativo { get; set; } = true;
    }
}
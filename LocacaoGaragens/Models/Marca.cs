using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LocacaoGaragens.Models
{
    public class Marca
    {
        
        [Key]
        public int Id { get; set; }

        
        public string Descricao { get; set; }
        
        public int Codigo { get; set; }
        [JsonIgnore]
        public virtual TipoVeiculo TipoVeiculo { get; set; }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LocacaoGaragens.Models
{
    public class Vaga
    {
        public int Id { get; set; }
        public int Quantidade { get; set; }
        public Enums.TipoVeiculo TipoVeiculo { get; set; }
    }
}
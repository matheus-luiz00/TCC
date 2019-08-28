﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LocacaoGaragens.Models
{
    public class TipoVeiculo
    {
        [Key]
        public int Id { get; set; }

        
        public string Descricao { get; set; }

        public int Codigo { get; set; }
    }
}
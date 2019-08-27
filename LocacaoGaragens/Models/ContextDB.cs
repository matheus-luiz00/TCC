using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace LocacaoGaragens.Models
{
    public class ContextDB : DbContext
    {
        
        public DbSet<Cor> cores { get; set; }
        public DbSet<Marca> Marcas { get; set; }
        public DbSet<Modelo> Modelos { get; set; }
        public DbSet<TipoVeiculo> TipoVeiculos { get; set; }
        public DbSet<PeriodoLocacao> periodoLocacoes { get; set; }
        public DbSet<Locacao> locacoes { get; set; }
        public DbSet<Termo> termos { get; set; }
        public DbSet<Usuario> usuarios { get; set; }
    }
}
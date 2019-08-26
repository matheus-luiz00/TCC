using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace LocacaoGaragens.Models
{
    public class ContextDB : DbContext
    {
        public DbSet<Automovel> automoveis { get; set; }
        public DbSet<Cor> cores { get; set; }
        public DbSet<MarcaCarro> marcaCarros { get; set; }
        public DbSet<MarcaMoto> marcaMotos { get; set; }
        public DbSet<Moto> motos { get; set; }
        public DbSet<Veiculo> veiculos { get; set; }
        public DbSet<PeriodoLocacao> periodoLocacoes { get; set; }
        public DbSet<Locacao> locacoes { get; set; }
        public DbSet<Termo> termos { get; set; }
        public DbSet<Usuario> usuarios { get; set; }
    }
}
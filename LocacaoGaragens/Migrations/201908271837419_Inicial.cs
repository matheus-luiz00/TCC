namespace LocacaoGaragens.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Inicial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Descricao = c.String(nullable: false),
                        Codigo = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Locacaos",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TipoVeiculo = c.Int(nullable: false),
                        Marca = c.Int(nullable: false),
                        Modelo = c.Int(nullable: false),
                        Cor = c.Int(nullable: false),
                        Placa = c.String(),
                        Periodo = c.Int(nullable: false),
                        Usuario = c.Int(nullable: false),
                        Status = c.String(),
                        TermoAceito = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PeriodoLocacaos", t => t.Periodo, cascadeDelete: true)
                .ForeignKey("dbo.Usuarios", t => t.Usuario, cascadeDelete: true)
                .Index(t => t.Periodo)
                .Index(t => t.Usuario);
            
            CreateTable(
                "dbo.PeriodoLocacaos",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DataInicial = c.DateTime(nullable: false),
                        DataFinal = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Usuarios",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IdRegistration = c.Int(nullable: false),
                        Email = c.String(),
                        Pcd = c.Boolean(nullable: false),
                        Noturno = c.Boolean(nullable: false),
                        ForaBnu = c.Boolean(nullable: false),
                        Carona = c.Boolean(nullable: false),
                        Nascimento = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Marcas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Descricao = c.String(nullable: false),
                        Codigo = c.Int(nullable: false),
                        Tipo = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TipoVeiculoes", t => t.Tipo, cascadeDelete: true)
                .Index(t => t.Tipo);
            
            CreateTable(
                "dbo.TipoVeiculoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Descricao = c.String(nullable: false),
                        Codigo = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Modeloes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Descricao = c.String(nullable: false),
                        MarcaTable_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Marcas", t => t.MarcaTable_Id)
                .Index(t => t.MarcaTable_Id);
            
            CreateTable(
                "dbo.Termoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Texto = c.String(nullable: false),
                        DataPublicacao = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Modeloes", "MarcaTable_Id", "dbo.Marcas");
            DropForeignKey("dbo.Marcas", "Tipo", "dbo.TipoVeiculoes");
            DropForeignKey("dbo.Locacaos", "Usuario", "dbo.Usuarios");
            DropForeignKey("dbo.Locacaos", "Periodo", "dbo.PeriodoLocacaos");
            DropIndex("dbo.Modeloes", new[] { "MarcaTable_Id" });
            DropIndex("dbo.Marcas", new[] { "Tipo" });
            DropIndex("dbo.Locacaos", new[] { "Usuario" });
            DropIndex("dbo.Locacaos", new[] { "Periodo" });
            DropTable("dbo.Termoes");
            DropTable("dbo.Modeloes");
            DropTable("dbo.TipoVeiculoes");
            DropTable("dbo.Marcas");
            DropTable("dbo.Usuarios");
            DropTable("dbo.PeriodoLocacaos");
            DropTable("dbo.Locacaos");
            DropTable("dbo.Cors");
        }
    }
}

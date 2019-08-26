namespace LocacaoGaragens.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DesignDeTabelaCompleto : DbMigration
    {
        public override void Up()
        {
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
                        TermoAceito = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PeriodoLocacaos", t => t.Periodo, cascadeDelete: true)
                .Index(t => t.Periodo);
            
            CreateTable(
                "dbo.Termoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Texto = c.String(nullable: false),
                        DataPublicacao = c.DateTime(nullable: false),
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
                        TrabalhoNot = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Locacaos", "Periodo", "dbo.PeriodoLocacaos");
            DropIndex("dbo.Locacaos", new[] { "Periodo" });
            DropTable("dbo.Usuarios");
            DropTable("dbo.Termoes");
            DropTable("dbo.Locacaos");
        }
    }
}

namespace LocacaoGaragens.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TabelaValor : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ValorLocacaos",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Valor = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TipoVeiculoFK = c.Int(nullable: false),
                        Ativo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TipoVeiculoes", t => t.TipoVeiculoFK, cascadeDelete: true)
                .Index(t => t.TipoVeiculoFK);
            
            AddColumn("dbo.Usuarios", "Ativo", c => c.Boolean(nullable: false));
            AddColumn("dbo.Termoes", "Ativo", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Usuarios", "Nascimento", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ValorLocacaos", "TipoVeiculoFK", "dbo.TipoVeiculoes");
            DropIndex("dbo.ValorLocacaos", new[] { "TipoVeiculoFK" });
            AlterColumn("dbo.Usuarios", "Nascimento", c => c.DateTime(nullable: false));
            DropColumn("dbo.Termoes", "Ativo");
            DropColumn("dbo.Usuarios", "Ativo");
            DropTable("dbo.ValorLocacaos");
        }
    }
}

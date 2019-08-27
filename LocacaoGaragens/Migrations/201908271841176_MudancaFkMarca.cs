namespace LocacaoGaragens.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MudancaFkMarca : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Marcas", "Tipo", "dbo.TipoVeiculoes");
            DropIndex("dbo.Marcas", new[] { "Tipo" });
            RenameColumn(table: "dbo.Marcas", name: "Tipo", newName: "TipoVeiculo_Id");
            AlterColumn("dbo.Marcas", "TipoVeiculo_Id", c => c.Int());
            CreateIndex("dbo.Marcas", "TipoVeiculo_Id");
            AddForeignKey("dbo.Marcas", "TipoVeiculo_Id", "dbo.TipoVeiculoes", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Marcas", "TipoVeiculo_Id", "dbo.TipoVeiculoes");
            DropIndex("dbo.Marcas", new[] { "TipoVeiculo_Id" });
            AlterColumn("dbo.Marcas", "TipoVeiculo_Id", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.Marcas", name: "TipoVeiculo_Id", newName: "Tipo");
            CreateIndex("dbo.Marcas", "Tipo");
            AddForeignKey("dbo.Marcas", "Tipo", "dbo.TipoVeiculoes", "Id", cascadeDelete: true);
        }
    }
}

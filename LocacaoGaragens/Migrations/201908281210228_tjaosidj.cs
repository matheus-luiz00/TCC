namespace LocacaoGaragens.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tjaosidj : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PeriodoLocacaos", "TipoVeiculo_Id", c => c.Int());
            CreateIndex("dbo.PeriodoLocacaos", "TipoVeiculo_Id");
            AddForeignKey("dbo.PeriodoLocacaos", "TipoVeiculo_Id", "dbo.TipoVeiculoes", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PeriodoLocacaos", "TipoVeiculo_Id", "dbo.TipoVeiculoes");
            DropIndex("dbo.PeriodoLocacaos", new[] { "TipoVeiculo_Id" });
            DropColumn("dbo.PeriodoLocacaos", "TipoVeiculo_Id");
        }
    }
}

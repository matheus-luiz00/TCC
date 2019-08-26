namespace LocacaoGaragens.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FkMoto : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Motoes", "Marca");
            AddForeignKey("dbo.Motoes", "Marca", "dbo.MarcaMotoes", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Motoes", "Marca", "dbo.MarcaMotoes");
            DropIndex("dbo.Motoes", new[] { "Marca" });
        }
    }
}

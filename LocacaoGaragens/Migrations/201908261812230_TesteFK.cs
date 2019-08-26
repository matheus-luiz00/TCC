namespace LocacaoGaragens.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TesteFK : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Automovels", "Marca");
            AddForeignKey("dbo.Automovels", "Marca", "dbo.MarcaCarroes", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Automovels", "Marca", "dbo.MarcaCarroes");
            DropIndex("dbo.Automovels", new[] { "Marca" });
        }
    }
}

namespace LocacaoGaragens.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CodigoId : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Automovels", "Marca", "dbo.MarcaCarroes");
            DropForeignKey("dbo.Motoes", "Marca", "dbo.MarcaMotoes");
            DropIndex("dbo.Automovels", new[] { "Marca" });
            DropIndex("dbo.Motoes", new[] { "Marca" });
            AddColumn("dbo.Automovels", "Codigo", c => c.Int(nullable: false));
            AddColumn("dbo.Cors", "Codigo", c => c.Int(nullable: false));
            AddColumn("dbo.Motoes", "Codigo", c => c.Int(nullable: false));
            AddColumn("dbo.Veiculoes", "Codigo", c => c.Int(nullable: false));
            AlterColumn("dbo.Cors", "Nome", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Cors", "Nome", c => c.String());
            DropColumn("dbo.Veiculoes", "Codigo");
            DropColumn("dbo.Motoes", "Codigo");
            DropColumn("dbo.Cors", "Codigo");
            DropColumn("dbo.Automovels", "Codigo");
            CreateIndex("dbo.Motoes", "Marca");
            CreateIndex("dbo.Automovels", "Marca");
            AddForeignKey("dbo.Motoes", "Marca", "dbo.MarcaMotoes", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Automovels", "Marca", "dbo.MarcaCarroes", "Id", cascadeDelete: true);
        }
    }
}

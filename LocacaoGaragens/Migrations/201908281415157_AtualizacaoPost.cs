namespace LocacaoGaragens.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AtualizacaoPost : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Modeloes", "MarcaTable_Id", "dbo.Marcas");
            DropIndex("dbo.Modeloes", new[] { "MarcaTable_Id" });
            AlterColumn("dbo.Modeloes", "MarcaTable_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.Modeloes", "MarcaTable_Id");
            AddForeignKey("dbo.Modeloes", "MarcaTable_Id", "dbo.Marcas", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Modeloes", "MarcaTable_Id", "dbo.Marcas");
            DropIndex("dbo.Modeloes", new[] { "MarcaTable_Id" });
            AlterColumn("dbo.Modeloes", "MarcaTable_Id", c => c.Int());
            CreateIndex("dbo.Modeloes", "MarcaTable_Id");
            AddForeignKey("dbo.Modeloes", "MarcaTable_Id", "dbo.Marcas", "Id");
        }
    }
}

namespace LocacaoGaragens.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CampoNotRequired : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TipoVeiculoes", "Descricao", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TipoVeiculoes", "Descricao", c => c.String(nullable: false));
        }
    }
}

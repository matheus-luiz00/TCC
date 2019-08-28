namespace LocacaoGaragens.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DescricaoCodigoNotRequiredMarca : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Marcas", "Descricao", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Marcas", "Descricao", c => c.String(nullable: false));
        }
    }
}

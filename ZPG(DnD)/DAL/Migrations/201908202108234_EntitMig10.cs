namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EntitMig10 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tbl_Characters", "Race", c => c.Int(nullable: false));
            AddColumn("dbo.tbl_Characters", "Clase", c => c.Int(nullable: false));
            AddColumn("dbo.tbl_Characters", "Aligment", c => c.Int(nullable: false));
            AddColumn("dbo.tbl_Characters", "Background", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.tbl_Characters", "Background");
            DropColumn("dbo.tbl_Characters", "Aligment");
            DropColumn("dbo.tbl_Characters", "Clase");
            DropColumn("dbo.tbl_Characters", "Race");
        }
    }
}

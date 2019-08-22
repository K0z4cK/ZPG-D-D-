namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EntitMig11 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tbl_Characters", "Level", c => c.Int(nullable: false));
            AddColumn("dbo.tbl_Characters", "Exp", c => c.Int(nullable: false));
            AddColumn("dbo.tbl_Characters", "Class", c => c.Int(nullable: false));
            DropColumn("dbo.tbl_Characters", "Clase");
        }
        
        public override void Down()
        {
            AddColumn("dbo.tbl_Characters", "Clase", c => c.Int(nullable: false));
            DropColumn("dbo.tbl_Characters", "Class");
            DropColumn("dbo.tbl_Characters", "Exp");
            DropColumn("dbo.tbl_Characters", "Level");
        }
    }
}

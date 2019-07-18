namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EntitMig3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tbl_Characters", "Name", c => c.String());
            AddColumn("dbo.tbl_Characters", "ArmorClass", c => c.Int(nullable: false));
            AddColumn("dbo.tbl_Characters", "Intitiative", c => c.Int(nullable: false));
            AddColumn("dbo.tbl_Characters", "Speed", c => c.Int(nullable: false));
            AddColumn("dbo.tbl_Characters", "HPMax", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.tbl_Characters", "HPMax");
            DropColumn("dbo.tbl_Characters", "Speed");
            DropColumn("dbo.tbl_Characters", "Intitiative");
            DropColumn("dbo.tbl_Characters", "ArmorClass");
            DropColumn("dbo.tbl_Characters", "Name");
        }
    }
}

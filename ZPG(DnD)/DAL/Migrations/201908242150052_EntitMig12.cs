namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EntitMig12 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tbl_Items", "TypeOfItem", c => c.Int(nullable: false));
            AddColumn("dbo.tbl_Items", "equipmentBonus", c => c.Int(nullable: false));
            AddColumn("dbo.tbl_Items", "TypeOfBonus", c => c.Int(nullable: false));
            AddColumn("dbo.tbl_Items", "itemBonus", c => c.Int(nullable: false));
            AddColumn("dbo.tbl_Items", "Price", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.tbl_Items", "Price");
            DropColumn("dbo.tbl_Items", "itemBonus");
            DropColumn("dbo.tbl_Items", "TypeOfBonus");
            DropColumn("dbo.tbl_Items", "equipmentBonus");
            DropColumn("dbo.tbl_Items", "TypeOfItem");
        }
    }
}

namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EntitMig14 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tbl_Characters", "Coins", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.tbl_Characters", "Coins");
        }
    }
}

namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EntitMig15 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tbl_CharacterItems", "isDressed", c => c.Boolean(nullable: false));
            DropColumn("dbo.tbl_Items", "isDressed");
        }
        
        public override void Down()
        {
            AddColumn("dbo.tbl_Items", "isDressed", c => c.Boolean(nullable: false));
            DropColumn("dbo.tbl_CharacterItems", "isDressed");
        }
    }
}

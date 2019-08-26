namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EntitMig13 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tbl_Items", "isDressed", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.tbl_Items", "isDressed");
        }
    }
}

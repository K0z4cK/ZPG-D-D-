namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EntitMig9 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.tbl_Users", "Email");
        }
        
        public override void Down()
        {
            AddColumn("dbo.tbl_Users", "Email", c => c.String(nullable: false));
        }
    }
}

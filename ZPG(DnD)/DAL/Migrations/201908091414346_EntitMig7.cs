namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EntitMig7 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tbl_Users", "Login", c => c.String(nullable: false, maxLength: 50));
            AddColumn("dbo.tbl_Users", "Email", c => c.String(nullable: false));
            AddColumn("dbo.tbl_Users", "Password", c => c.String(nullable: false, maxLength: 30));
            CreateIndex("dbo.tbl_Users", "Login", unique: true);
            DropColumn("dbo.tbl_Users", "Name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.tbl_Users", "Name", c => c.String());
            DropIndex("dbo.tbl_Users", new[] { "Login" });
            DropColumn("dbo.tbl_Users", "Password");
            DropColumn("dbo.tbl_Users", "Email");
            DropColumn("dbo.tbl_Users", "Login");
        }
    }
}

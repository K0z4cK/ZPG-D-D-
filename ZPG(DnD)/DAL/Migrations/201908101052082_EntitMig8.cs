namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EntitMig8 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tbl_Characters", "HP", c => c.Int(nullable: false));
            AddColumn("dbo.tbl_Enemies", "HP", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.tbl_Enemies", "HP");
            DropColumn("dbo.tbl_Characters", "HP");
        }
    }
}

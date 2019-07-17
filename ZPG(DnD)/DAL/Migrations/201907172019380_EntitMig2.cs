namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EntitMig2 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.tbl_CharacterSkills");
            DropPrimaryKey("dbo.tbl_CharacterStats");
            AlterColumn("dbo.tbl_CharacterSkills", "Id", c => c.Int(nullable: false));
            AlterColumn("dbo.tbl_CharacterStats", "Id", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.tbl_CharacterSkills", "Id");
            AddPrimaryKey("dbo.tbl_CharacterStats", "Id");
            CreateIndex("dbo.tbl_CharacterSkills", "Id");
            CreateIndex("dbo.tbl_CharacterStats", "Id");
            AddForeignKey("dbo.tbl_CharacterSkills", "Id", "dbo.tbl_Characters", "Id");
            AddForeignKey("dbo.tbl_CharacterStats", "Id", "dbo.tbl_Characters", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tbl_CharacterStats", "Id", "dbo.tbl_Characters");
            DropForeignKey("dbo.tbl_CharacterSkills", "Id", "dbo.tbl_Characters");
            DropIndex("dbo.tbl_CharacterStats", new[] { "Id" });
            DropIndex("dbo.tbl_CharacterSkills", new[] { "Id" });
            DropPrimaryKey("dbo.tbl_CharacterStats");
            DropPrimaryKey("dbo.tbl_CharacterSkills");
            AlterColumn("dbo.tbl_CharacterStats", "Id", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.tbl_CharacterSkills", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.tbl_CharacterStats", "Id");
            AddPrimaryKey("dbo.tbl_CharacterSkills", "Id");
        }
    }
}

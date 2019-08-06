namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EntitMig6 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tbl_Users", "Name", c => c.String());
            AddColumn("dbo.tbl_Enemies", "IsBoss", c => c.Boolean(nullable: false));
            DropColumn("dbo.tbl_CharacterSkills", "Arcana");
            DropColumn("dbo.tbl_CharacterSkills", "Deception");
            DropColumn("dbo.tbl_CharacterSkills", "History");
            DropColumn("dbo.tbl_CharacterSkills", "Insight");
            DropColumn("dbo.tbl_CharacterSkills", "Intimidation");
            DropColumn("dbo.tbl_CharacterSkills", "Investigation");
            DropColumn("dbo.tbl_CharacterSkills", "Nature");
            DropColumn("dbo.tbl_CharacterSkills", "Perception");
            DropColumn("dbo.tbl_CharacterSkills", "Performance");
        }
        
        public override void Down()
        {
            AddColumn("dbo.tbl_CharacterSkills", "Performance", c => c.Int(nullable: false));
            AddColumn("dbo.tbl_CharacterSkills", "Perception", c => c.Int(nullable: false));
            AddColumn("dbo.tbl_CharacterSkills", "Nature", c => c.Int(nullable: false));
            AddColumn("dbo.tbl_CharacterSkills", "Investigation", c => c.Int(nullable: false));
            AddColumn("dbo.tbl_CharacterSkills", "Intimidation", c => c.Int(nullable: false));
            AddColumn("dbo.tbl_CharacterSkills", "Insight", c => c.Int(nullable: false));
            AddColumn("dbo.tbl_CharacterSkills", "History", c => c.Int(nullable: false));
            AddColumn("dbo.tbl_CharacterSkills", "Deception", c => c.Int(nullable: false));
            AddColumn("dbo.tbl_CharacterSkills", "Arcana", c => c.Int(nullable: false));
            DropColumn("dbo.tbl_Enemies", "IsBoss");
            DropColumn("dbo.tbl_Users", "Name");
        }
    }
}

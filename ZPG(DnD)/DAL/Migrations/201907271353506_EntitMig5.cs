namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EntitMig5 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.tbl_Inventories", newName: "tbl_CharacterInventories");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.tbl_CharacterInventories", newName: "tbl_Inventories");
        }
    }
}

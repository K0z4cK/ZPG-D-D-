namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EntitMig4 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tbl_EnemyItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ItemId = c.Int(nullable: false),
                        InventoryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tbl_Items", t => t.ItemId, cascadeDelete: true)
                .Index(t => t.ItemId)
                .Index(t => t.InventoryId);
            
            CreateTable(
                "dbo.tbl_EnemyInventories",
                c => new
                    {
                        Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.tbl_Enemies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ExpGained = c.Int(nullable: false),
                        Name = c.String(),
                        ArmorClass = c.Int(nullable: false),
                        Intitiative = c.Int(nullable: false),
                        Speed = c.Int(nullable: false),
                        HPMax = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tbl_EnemyItems", "ItemId", "dbo.tbl_Items");
            DropIndex("dbo.tbl_EnemyInventories", new[] { "Id" });
            DropIndex("dbo.tbl_EnemyItems", new[] { "InventoryId" });
            DropIndex("dbo.tbl_EnemyItems", new[] { "ItemId" });
            DropTable("dbo.tbl_Enemies");
            DropTable("dbo.tbl_EnemyInventories");
            DropTable("dbo.tbl_EnemyItems");
        }
    }
}

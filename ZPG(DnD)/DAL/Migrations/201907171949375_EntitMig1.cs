namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EntitMig1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tbl_CharacterItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ItemId = c.Int(nullable: false),
                        InventoryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tbl_Inventories", t => t.InventoryId, cascadeDelete: true)
                .ForeignKey("dbo.tbl_Items", t => t.ItemId, cascadeDelete: true)
                .Index(t => t.ItemId)
                .Index(t => t.InventoryId);
            
            CreateTable(
                "dbo.tbl_Inventories",
                c => new
                    {
                        Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tbl_Characters", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.tbl_Characters",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tbl_Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.tbl_Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.tbl_Items",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.tbl_CharacterSkills",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: false),
                        Acrobatics = c.Int(nullable: false),
                        AnimalHandling = c.Int(nullable: false),
                        Arcana = c.Int(nullable: false),
                        Athletics = c.Int(nullable: false),
                        Deception = c.Int(nullable: false),
                        History = c.Int(nullable: false),
                        Insight = c.Int(nullable: false),
                        Intimidation = c.Int(nullable: false),
                        Investigation = c.Int(nullable: false),
                        Medicine = c.Int(nullable: false),
                        Nature = c.Int(nullable: false),
                        Perception = c.Int(nullable: false),
                        Performance = c.Int(nullable: false),
                        Persuasion = c.Int(nullable: false),
                        Religion = c.Int(nullable: false),
                        SleightOfHand = c.Int(nullable: false),
                        Stealth = c.Int(nullable: false),
                        Survival = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.tbl_CharacterStats",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: false),
                        Strength = c.Int(nullable: false),
                        Dexterity = c.Int(nullable: false),
                        Constitution = c.Int(nullable: false),
                        Intelligence = c.Int(nullable: false),
                        Wisdom = c.Int(nullable: false),
                        Charisma = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tbl_CharacterItems", "ItemId", "dbo.tbl_Items");
            DropForeignKey("dbo.tbl_CharacterItems", "InventoryId", "dbo.tbl_Inventories");
            DropForeignKey("dbo.tbl_Inventories", "Id", "dbo.tbl_Characters");
            DropForeignKey("dbo.tbl_Characters", "UserId", "dbo.tbl_Users");
            DropIndex("dbo.tbl_Characters", new[] { "UserId" });
            DropIndex("dbo.tbl_Inventories", new[] { "Id" });
            DropIndex("dbo.tbl_CharacterItems", new[] { "InventoryId" });
            DropIndex("dbo.tbl_CharacterItems", new[] { "ItemId" });
            DropTable("dbo.tbl_CharacterStats");
            DropTable("dbo.tbl_CharacterSkills");
            DropTable("dbo.tbl_Items");
            DropTable("dbo.tbl_Users");
            DropTable("dbo.tbl_Characters");
            DropTable("dbo.tbl_Inventories");
            DropTable("dbo.tbl_CharacterItems");
        }
    }
}

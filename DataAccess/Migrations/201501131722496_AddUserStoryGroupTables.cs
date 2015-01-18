namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserStoryGroupTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Groups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Password = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Stories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        Description = c.String(),
                        Content = c.String(nullable: false),
                        PostedOn = c.DateTime(nullable: false),
                        CreatorId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.CreatorId, cascadeDelete: true)
                .Index(t => t.CreatorId);
            
            CreateTable(
                "dbo.Users2Groups",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        GroupId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.GroupId })
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.Groups", t => t.GroupId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.GroupId);
            
            CreateTable(
                "dbo.Stories2Groups",
                c => new
                    {
                        StoryId = c.Int(nullable: false),
                        GroupId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.StoryId, t.GroupId })
                .ForeignKey("dbo.Stories", t => t.StoryId, cascadeDelete: true)
                .ForeignKey("dbo.Groups", t => t.GroupId, cascadeDelete: true)
                .Index(t => t.StoryId)
                .Index(t => t.GroupId);            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Stories2Groups", "GroupId", "dbo.Groups");
            DropForeignKey("dbo.Stories2Groups", "StoryId", "dbo.Stories");
            DropForeignKey("dbo.Stories", "CreatorId", "dbo.User");
            DropForeignKey("dbo.Users2Groups", "GroupId", "dbo.Groups");
            DropForeignKey("dbo.Users2Groups", "UserId", "dbo.User");
            DropIndex("dbo.Stories2Groups", new[] { "GroupId" });
            DropIndex("dbo.Stories2Groups", new[] { "StoryId" });
            DropIndex("dbo.Users2Groups", new[] { "GroupId" });
            DropIndex("dbo.Users2Groups", new[] { "UserId" });
            DropIndex("dbo.Stories", new[] { "CreatorId" });
            DropTable("dbo.Stories2Groups");
            DropTable("dbo.Users2Groups");
            DropTable("dbo.Stories");
            DropTable("dbo.User");
            DropTable("dbo.Groups");
        }
    }
}

namespace InnerTrack.DAL.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class InitialLoad : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Event",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SourceEventId = c.Int(nullable: false),
                        Title = c.String(nullable: false),
                        Message = c.String(),
                        CreatedBy = c.String(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        UpdatedBy = c.String(),
                        UpdatedOn = c.DateTime(),
                        FeedObj_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Feed", t => t.FeedObj_Id)
                .Index(t => t.FeedObj_Id);
            
            CreateTable(
                "Feed",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CreatedBy = c.String(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        UpdatedBy = c.String(),
                        UpdatedOn = c.DateTime(),
                        Type_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("FeedType", t => t.Type_Id, cascadeDelete: true)
                .Index(t => t.Type_Id);
            
            CreateTable(
                "FeedType",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        AssociatedClass = c.String(nullable: false),
                        CreatedBy = c.String(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        UpdatedBy = c.String(),
                        UpdatedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "Project",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Description = c.String(),
                        CreatedBy = c.String(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        UpdatedBy = c.String(),
                        UpdatedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "Tag",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        CreatedBy = c.String(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        UpdatedBy = c.String(),
                        UpdatedOn = c.DateTime(),
                        ProjectObj_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Project", t => t.ProjectObj_Id)
                .Index(t => t.ProjectObj_Id);
            
            CreateTable(
                "User",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Email = c.String(nullable: false),
                        CreatedBy = c.String(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        UpdatedBy = c.String(),
                        UpdatedOn = c.DateTime(),
                        ProjectObj_Id = c.Int(),
                        ProjectObj_Id1 = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Project", t => t.ProjectObj_Id)
                .ForeignKey("Project", t => t.ProjectObj_Id1)
                .Index(t => t.ProjectObj_Id)
                .Index(t => t.ProjectObj_Id1);
            
            CreateTable(
                "ProjectSource",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Configuration = c.String(nullable: false),
                        CreatedBy = c.String(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        UpdatedBy = c.String(),
                        UpdatedOn = c.DateTime(),
                        SourceConfig_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("ProjectSourceType", t => t.SourceConfig_Id)
                .Index(t => t.SourceConfig_Id);
            
            CreateTable(
                "ProjectSourceType",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FullClassName = c.String(nullable: false),
                        Name = c.String(nullable: false),
                        Interval = c.Int(nullable: false),
                        LastRun = c.DateTime(nullable: false),
                        NextRun = c.DateTime(),
                        Enabled = c.Boolean(nullable: false),
                        CreatedBy = c.String(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        UpdatedBy = c.String(),
                        UpdatedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "ProjectObjFeedObjs",
                c => new
                    {
                        ProjectObj_Id = c.Int(nullable: false),
                        FeedObj_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ProjectObj_Id, t.FeedObj_Id })
                .ForeignKey("Project", t => t.ProjectObj_Id, cascadeDelete: true)
                .ForeignKey("Feed", t => t.FeedObj_Id, cascadeDelete: true)
                .Index(t => t.ProjectObj_Id)
                .Index(t => t.FeedObj_Id);
            
        }
        
        public override void Down()
        {
            DropIndex("ProjectObjFeedObjs", new[] { "FeedObj_Id" });
            DropIndex("ProjectObjFeedObjs", new[] { "ProjectObj_Id" });
            DropIndex("ProjectSource", new[] { "SourceConfig_Id" });
            DropIndex("User", new[] { "ProjectObj_Id1" });
            DropIndex("User", new[] { "ProjectObj_Id" });
            DropIndex("Tag", new[] { "ProjectObj_Id" });
            DropIndex("Feed", new[] { "Type_Id" });
            DropIndex("Event", new[] { "FeedObj_Id" });
            DropForeignKey("ProjectObjFeedObjs", "FeedObj_Id", "Feed");
            DropForeignKey("ProjectObjFeedObjs", "ProjectObj_Id", "Project");
            DropForeignKey("ProjectSource", "SourceConfig_Id", "ProjectSourceType");
            DropForeignKey("User", "ProjectObj_Id1", "Project");
            DropForeignKey("User", "ProjectObj_Id", "Project");
            DropForeignKey("Tag", "ProjectObj_Id", "Project");
            DropForeignKey("Feed", "Type_Id", "FeedType");
            DropForeignKey("Event", "FeedObj_Id", "Feed");
            DropTable("ProjectObjFeedObjs");
            DropTable("ProjectSourceType");
            DropTable("ProjectSource");
            DropTable("User");
            DropTable("Tag");
            DropTable("Project");
            DropTable("FeedType");
            DropTable("Feed");
            DropTable("Event");
        }
    }
}

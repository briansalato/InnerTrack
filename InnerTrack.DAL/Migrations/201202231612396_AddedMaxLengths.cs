namespace InnerTrack.DAL.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class AddedMaxLengths : DbMigration
    {
        public override void Up()
        {
            AddColumn("ProjectSourceType", "AssemblyName", c => c.String(nullable: false, maxLength: 200));
            AlterColumn("Event", "Title", c => c.String(nullable: false, maxLength: 200));
            AlterColumn("Event", "CreatedBy", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("Event", "UpdatedBy", c => c.String(maxLength: 100));
            AlterColumn("Feed", "CreatedBy", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("Feed", "UpdatedBy", c => c.String(maxLength: 100));
            AlterColumn("FeedType", "Name", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("FeedType", "AssociatedClass", c => c.String(nullable: false, maxLength: 200));
            AlterColumn("FeedType", "CreatedBy", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("FeedType", "UpdatedBy", c => c.String(maxLength: 100));
            AlterColumn("Project", "Name", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("Project", "Description", c => c.String(maxLength: 1000));
            AlterColumn("Project", "CreatedBy", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("Project", "UpdatedBy", c => c.String(maxLength: 100));
            AlterColumn("Tag", "Name", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("Tag", "CreatedBy", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("Tag", "UpdatedBy", c => c.String(maxLength: 100));
            AlterColumn("User", "FirstName", c => c.String(maxLength: 50));
            AlterColumn("User", "LastName", c => c.String(maxLength: 50));
            AlterColumn("User", "Email", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("User", "CreatedBy", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("User", "UpdatedBy", c => c.String(maxLength: 100));
            AlterColumn("ProjectSource", "CreatedBy", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("ProjectSource", "UpdatedBy", c => c.String(maxLength: 100));
            AlterColumn("ProjectSourceType", "FullClassName", c => c.String(nullable: false, maxLength: 200));
            AlterColumn("ProjectSourceType", "Name", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("ProjectSourceType", "LastRun", c => c.DateTime());
            AlterColumn("ProjectSourceType", "NextRun", c => c.DateTime(nullable: false));
            AlterColumn("ProjectSourceType", "CreatedBy", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("ProjectSourceType", "UpdatedBy", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("ProjectSourceType", "UpdatedBy", c => c.String());
            AlterColumn("ProjectSourceType", "CreatedBy", c => c.String(nullable: false));
            AlterColumn("ProjectSourceType", "NextRun", c => c.DateTime());
            AlterColumn("ProjectSourceType", "LastRun", c => c.DateTime(nullable: false));
            AlterColumn("ProjectSourceType", "Name", c => c.String(nullable: false));
            AlterColumn("ProjectSourceType", "FullClassName", c => c.String(nullable: false));
            AlterColumn("ProjectSource", "UpdatedBy", c => c.String());
            AlterColumn("ProjectSource", "CreatedBy", c => c.String(nullable: false));
            AlterColumn("User", "UpdatedBy", c => c.String());
            AlterColumn("User", "CreatedBy", c => c.String(nullable: false));
            AlterColumn("User", "Email", c => c.String(nullable: false));
            AlterColumn("User", "LastName", c => c.String());
            AlterColumn("User", "FirstName", c => c.String());
            AlterColumn("Tag", "UpdatedBy", c => c.String());
            AlterColumn("Tag", "CreatedBy", c => c.String(nullable: false));
            AlterColumn("Tag", "Name", c => c.String(nullable: false));
            AlterColumn("Project", "UpdatedBy", c => c.String());
            AlterColumn("Project", "CreatedBy", c => c.String(nullable: false));
            AlterColumn("Project", "Description", c => c.String());
            AlterColumn("Project", "Name", c => c.String(nullable: false));
            AlterColumn("FeedType", "UpdatedBy", c => c.String());
            AlterColumn("FeedType", "CreatedBy", c => c.String(nullable: false));
            AlterColumn("FeedType", "AssociatedClass", c => c.String(nullable: false));
            AlterColumn("FeedType", "Name", c => c.String(nullable: false));
            AlterColumn("Feed", "UpdatedBy", c => c.String());
            AlterColumn("Feed", "CreatedBy", c => c.String(nullable: false));
            AlterColumn("Event", "UpdatedBy", c => c.String());
            AlterColumn("Event", "CreatedBy", c => c.String(nullable: false));
            AlterColumn("Event", "Title", c => c.String(nullable: false));
            DropColumn("ProjectSourceType", "AssemblyName");
        }
    }
}

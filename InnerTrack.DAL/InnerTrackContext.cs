using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using InnerTrack.Common.Objs;

namespace InnerTrack.DAL
{
    internal class InnerTrackContext : DbContext
    {
        public DbSet<EventObj> Events { get; set; }
        public DbSet<FeedObj> Feeds { get; set; }
        public DbSet<FeedTypeObj> FeedTypes { get; set; }
        public DbSet<ProjectObj> Projects { get; set; }
        public DbSet<ProjectSourceObj> ProjectSources { get; set; }
        public DbSet<ProjectSourceTypeObj> ProjectSourceTypes { get; set; }
        public DbSet<TagObj> Tags { get; set; }
        public DbSet<UserObj> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EventObj>().ToTable("Event");
            modelBuilder.Entity<FeedObj>().ToTable("Feed");
            modelBuilder.Entity<FeedTypeObj>().ToTable("FeedType");
            modelBuilder.Entity<ProjectObj>().ToTable("Project");
            modelBuilder.Entity<ProjectSourceObj>().ToTable("ProjectSource");
            modelBuilder.Entity<ProjectSourceTypeObj>().ToTable("ProjectSourceType");
            modelBuilder.Entity<TagObj>().ToTable("Tag");
            modelBuilder.Entity<UserObj>().ToTable("User");
        }

        public InnerTrackContext() : this("InnerTrackDb") { }
        public InnerTrackContext(string connectionStringName) : base(connectionStringName) 
        {
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;
        }
    }
}

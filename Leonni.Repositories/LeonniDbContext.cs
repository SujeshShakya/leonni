using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Leonni.Models;


namespace Leonni.Repositories
{
    public class LeonniDbContext : DbContext
    {
        public DbSet<Translation> Translation { get; set; }
        public DbSet<Language> Language { get; set; }
        public DbSet<PageContent> PageContent { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Discipline> Discipline{ get; set; }
        public DbSet<Year> Year { get; set; }
        public DbSet<Month> Month { get; set; }
        public DbSet<Country> Country { get; set; }
        public DbSet<Province> Province { get; set; }
        public DbSet<UserProfile> UserProfile { get; set; }
        public DbSet<Group> Group { get; set; }
        public DbSet<Project> Project { get; set; }
        public DbSet<EntityFile> EntityFile { get; set; }
        public DbSet<File> File { get; set; }
        public DbSet<Publication> Publication { get; set; }
        public DbSet<Activity> Activity { get; set; }
        public DbSet<Gallery> Gallery { get; set; }
        public DbSet<Advertisement> Advertisement { get; set; }
        public DbSet<Section> Section { get; set; }
        public DbSet<VideoLink> VideoLink { get; set; }
        public DbSet<FrontEntityFile> FrontEntityFile { get; set; }
        public DbSet<FrontContent> FrontContent { get; set; }
        public DbSet<Message> Message { get; set; }
        public DbSet<Comment> Comment { get; set; }
        public DbSet<CommentThread> CommentThread { get; set; }
        public DbSet<Like> Like { get; set; }
        public DbSet<Favourite> Favourite { get; set; }
        //public DbSet<Country> Country { get; set; }
        //public DbSet<User> User { get; set; }
        //public DbSet<Membership> Membership { get; set; }
        //public DbSet<aspnet_Applications> aspnet_Application { get; set; }
        //public DbSet<aspnet_Roles> aspnet_Role { get; set; }
        //public DbSet<aspnet_PersonalizationAllUsers> aspnet_PersonalizationAllUser { get; set; }
        //public DbSet<aspnet_Profile> aspnet_Profile { get; set; }
        //public DbSet<aspnet_PersonalizationPerUser> aspnet_PersonalizationPerUser { get; set; }
        //public DbSet<aspnet_SchemaVersions> aspnet_SchemaVersions { get; set; }
        //public DbSet<aspnet_WebEvent_Events> aspnet_WebEvent_Events { get; set; }
        //public DbSet<aspnet_Paths> aspnet_Paths { get; set; }


        protected override void OnModelCreating(System.Data.Entity.DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<IncludeMetadataConvention>();
        }
    }}

using DataAccess.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class FlatClubContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Story> Stories { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            this.Configuration.ValidateOnSaveEnabled = false;
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().HasMany(m => m.Groups).WithMany(m => m.Members).Map(m => { m.MapLeftKey("UserId"); m.MapRightKey("GroupId"); m.ToTable("Users2Groups");});
            
            modelBuilder.Entity<Story>().HasMany(m => m.Groups).WithMany(m => m.Stories).Map(m => { m.MapLeftKey("StoryId"); m.MapRightKey("GroupId"); m.ToTable("Stories2Groups"); });
        }
    }
}

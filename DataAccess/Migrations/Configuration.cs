namespace DataAccess.Migrations
{
    using Entity;
    using System;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<DataAccess.FlatClubContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }
        
        protected override void Seed(DataAccess.FlatClubContext context)
        {
            var user = new User
            {
                Name = "flatclub",
                Password = "qwerty".GetMD5()
            };

            var stories = new Story[]
            {
                new Story
                {
                    Title = "First Story",
                    Description = "First Storys Description",
                    Content = "First Storys Content",
                    PostedOn = DateTime.UtcNow,
                    Creator = user
                },
                new Story
                {
                    Title = "Second Story",
                    Description = "Second Storys Description",
                    Content = "Second Storys Content",
                    PostedOn = DateTime.UtcNow,
                    Creator = user
                }
            };

            var groups = new Group[]
            {
                new Group
                {
                    Name = "First Group",
                    Description = "First Groups Description",
                    Members = new User[] {
                        user
                    },
                    Stories = stories
                },
                new Group
                {
                    Name = "Second Group",
                    Description = "Second Groups Description",
                    Members = new User[] {
                        user
                    },
                    Stories = new Story[] {
                        stories.FirstOrDefault()
                    }
                }
            };

            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.

            context.Groups.AddOrUpdate(
              m => m.Name,
              groups
            );            
        }
    }
}

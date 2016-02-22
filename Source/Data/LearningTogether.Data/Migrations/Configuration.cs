namespace LearningTogether.Data.Migrations
{
    using System.Data.Entity.Migrations;
    using System.Linq;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    using LearningTogether.Common;
    using LearningTogether.Data.Models;

    public sealed class Configuration : DbMigrationsConfiguration<LearningTogetherDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(LearningTogetherDbContext context)
        {
            if (!context.Users.Any())
            {
                var user = this.SeedUsers(context);
                this.SeedSites(context, user);
                this.SeedArticles(context, user);
            }
        }

        private void SeedArticles(LearningTogetherDbContext context, User user)
        {
            var articles = new ArticleItem[]
                           {
                                new ArticleItem()
                                    {
                                        Author = user,
                                        Description = "asdasdas",
                                        Title = "1",
                                        Content = "asdasd"

                                    },
                                new ArticleItem()
                                    {
                                        Author = user,
                                        Description = "bbbbbbb",
                                        Title = "2",
                                        Content = "22222222222222"
                                    },
                           };
            context.ArticleItems.AddOrUpdate(articles);
            context.SaveChanges();
        }

        private void SeedSites(LearningTogetherDbContext context, User user)
        {
            var extItems = new ExternalItem[]
                            {
                                new ExternalItem()
                                    {
                                        Author = user,
                                        Description = @"Telerik Academy is a free educational initiative for training software engineers in Bulgaria. With over 20 in-depth courses in cutting-edge technologies, Telerik Academy helps students of almost any age and background develop programming skills and land jobs in the competitive IT industry.

Launched in 2009 to address the shortage of IT professionals, Telerik Academy is organized and sponsored entirely by Telerik, a Progress company. All courses are held in Bulgarian on the company’s Sofia, Bulgaria campus,
and are likewise accessible free on YouTube.com. ",
                                        Link = "http://telerikacademy.com/",
                                        Type = ExternalItemType.Site
                                    },
                                new ExternalItem()
                                    {
                                        Author = user,
                                        Description = @"Learn English Online",
                                        Link = "http://www.english-online.org.uk/",
                                        Type = ExternalItemType.Site
                                    },
                                new ExternalItem()
                                    {
                                        Author = user,
                                        Description = @"Looking to go off road for the first time? Here are tips and advice you need to make your introduction to mountain biking fun and successful.",
                                        Link = "http://www.active.com/mountain-biking/articles/beginner-s-guide-to-mountain-biking",
                                        Type = ExternalItemType.Article
                                    },

                            };
            context.ExternalItems.AddOrUpdate(extItems);
            context.SaveChanges();
        }

        private User SeedUsers(LearningTogetherDbContext context)
        {
            const string AdministratorUserName = "admin@site.com";
            const string AdministratorPassword = AdministratorUserName;
            const string ModeratorUserName = "modtor@site.com";
            const string ModeratorPassword = ModeratorUserName;

            // Create admin role
            var roleStore = new RoleStore<IdentityRole>(context);
            var roleManager = new RoleManager<IdentityRole>(roleStore);
            var role = new IdentityRole { Name = GlobalConstants.AdministratorRoleName };
            roleManager.Create(role);

            // Create moderator role
            role = new IdentityRole { Name = GlobalConstants.ModeratorRoleName };
            roleManager.Create(role);

            // Create admin user
            var userStore = new UserStore<User>(context);
            var userManager = new UserManager<User>(userStore);
            var admin = new User { UserName = AdministratorUserName, Email = AdministratorUserName };
            userManager.Create(admin, AdministratorPassword);
            userManager.AddToRole(admin.Id, GlobalConstants.AdministratorRoleName);

            // Create moderator user
            var moderator = new User { UserName = ModeratorUserName, Email = ModeratorUserName };
            userManager.Create(moderator, ModeratorPassword);
            userManager.AddToRole(moderator.Id, GlobalConstants.ModeratorRoleName);

            return admin;
        }
    }
}

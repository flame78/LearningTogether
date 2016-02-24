namespace LearningTogether.Data.Migrations
{
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using System.Linq;

    using LearningTogether.Common;
    using LearningTogether.Data.Models;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

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
                this.SeedData(context, user);
            }
        }

        private void SeedData(LearningTogetherDbContext context, User user)
        {
            var categories = new Category[]
                                 {
                                     new Category() { Name = "Programing" }, new Category() { Name = "Sport" },
                                     new Category() { Name = "Food" }, new Category() { Name = "Еntertainment" },
                                     new Category() { Name = "Health" }, new Category() { Name = "Language" },
                                     new Category() { Name = "Repair" }, new Category() { Name = "Other" },
                                 };

            context.Categories.AddOrUpdate(categories);
            context.SaveChanges();

            var books = new List<BookItem>();

            for (int i = 1; i < 30; i++)
            {
                books.Add(
                    new BookItem()
                    {
                        Author = user,
                        BookAuthor = "John Dow " + i,
                        Category = categories[7],
                        Description =
                                i
                                + " Sed ut perspiciatis unde omnis iste natus error sit voluptatem accusantium doloremque laudantium, totam rem aperiam, eaque ipsa quae ab illo inventore veritatis et quasi architecto beatae vitae dicta sunt explicabo. ",
                        Title = "Lorem Ipsum" + i,
                        ISBN = "978-3-16-148410-" + i,
                    });
            }

            context.BookItems.AddOrUpdate(books.ToArray());
            context.SaveChanges();

            var articles = new List<ArticleItem>();

            articles.AddRange(
                new ArticleItem[]
                    {
                        new ArticleItem()
                            {
                                Author = user,
                                Title = "None",
                                Description = "For now this nothing    !",
                                Content = "<h1>very important</h1>",
                                Category = categories[7]
                            },
                        new ArticleItem()
                            {
                                Author = user,
                                Title = "Coding",
                                Description = "How to make things to work",
                                Content = "<h1>Learn</h1> and <h2>Exercise</h2>",
                                Category = categories[0]
                            }
                    });

            //for (int i = 1; i < 30; i++)
            //{
            //    articles.Add(
            //        new ArticleItem()
            //        {
            //            Author = user,
            //            Category = categories[7],
            //            Description = i + " Sed ut perspiciatis unde omnis iste natus error sit voluptatem accusantium doloremque laudantium, totam rem aperiam, eaque ipsa quae ab illo inventore veritatis et quasi architecto beatae vitae",
            //            Title = "Lorem Ipsum" + i,
            //        });
            //}

            //context.ArticleItems.AddOrUpdate(articles.ToArray());
            //context.SaveChanges();

            var extItems = new ExternalItem[]
                               {
                                   new ExternalItem()
                                       {
                                           Author = user,
                                           Description =
                                               @"Telerik Academy is a free educational initiative for training software engineers in Bulgaria. With over 20 in-depth courses in cutting-edge technologies, Telerik Academy helps students of almost any age and background develop programming skills and land jobs in the competitive IT industry.

Launched in 2009 to address the shortage of IT professionals, Telerik Academy is organized and sponsored entirely by Telerik, a Progress company. All courses are held in Bulgarian on the company’s Sofia, Bulgaria campus,
and are likewise accessible free on YouTube.com. ",
                                           Link = "http://telerikacademy.com/",
                                           Type = ExternalItemType.Site,
                                           Category = categories[0]
                                       },
                                   new ExternalItem()
                                       {
                                           Author = user,
                                           Description = @"Learn English Online   ",
                                           Link = "http://www.english-online.org.uk/",
                                           Type = ExternalItemType.Site,
                                           Category = categories[5]
                                       },
                                   new ExternalItem()
                                       {
                                           Author = user,
                                           Description =
                                               @"Looking to go off road for the first time? Here are tips and advice you need to make your introduction to mountain biking fun and successful.",
                                           Link =
                                               "http://www.active.com/mountain-biking/articles/beginner-s-guide-to-mountain-biking",
                                           Type = ExternalItemType.Article,
                                           Category = categories[1]
                                       },
                               };
            context.ExternalItems.AddOrUpdate(extItems);
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
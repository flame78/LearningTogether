namespace LearningTogether.Data
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    using LearningTogether.Data.Common.Models;
    using LearningTogether.Data.Models;

    using Microsoft.AspNet.Identity.EntityFramework;

    public class LearningTogetherDbContext : IdentityDbContext<User>
    {
        public LearningTogetherDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public virtual IDbSet<ArticleItem> ArticleItems { get; set; }

        public virtual IDbSet<BookItem> BookItems { get; set; }

        public virtual IDbSet<ExternalItem> ExternalItems { get; set; }

        public virtual IDbSet<Comment> Comments { get; set; }

        public virtual IDbSet<Category> Categories { get; set; }

        public virtual IDbSet<Rating> Ratings { get; set; }

        public static LearningTogetherDbContext Create()
        {
            return new LearningTogetherDbContext();
        }

        public override int SaveChanges()
        {
            this.ApplyAuditInfoRules();
            return base.SaveChanges();
        }

        private void ApplyAuditInfoRules()
        {
            // Approach via @julielerman: http://bit.ly/123661P
            foreach (var entry in
                this.ChangeTracker.Entries()
                    .Where(
                        e =>
                        e.Entity is IAuditInfo && ((e.State == EntityState.Added) || (e.State == EntityState.Modified)))
                )
            {
                var entity = (IAuditInfo)entry.Entity;
                if (entry.State == EntityState.Added && entity.CreatedOn == default(DateTime))
                {
                    entity.CreatedOn = DateTime.Now;
                }
                else
                {
                    entity.ModifiedOn = DateTime.Now;
                }
            }
        }
    }
}
namespace LearningTogether.Services.Data
{
    using System.Linq;

    using LearningTogether.Common;
    using LearningTogether.Data.Common;
    using LearningTogether.Data.Models;

    public class ExternalItemsService : GenericItemsService<ExternalItem>, IExternalItemsService
    {
        public ExternalItemsService(IDbRepository<ExternalItem> items)
            : base(items)
        {
        }

        public IQueryable<ExternalItem> GetTop(int count, ExternalItemType type)
        {
            return this.All().Where(x => x.Type == type).OrderByDescending(y => y.Ratings.Average(z => z.Value)).Take(count);
        }

        public IQueryable<ExternalItem> All(ExternalItemType type)
        {
            return this.All().Where(x => x.Type == type);
        }
    }
}
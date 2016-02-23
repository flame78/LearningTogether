namespace LearningTogether.Services.Data
{
    using System.Linq;

    using LearningTogether.Common;
    using LearningTogether.Data.Common.Models;
    using LearningTogether.Data.Models;

    public interface IExternalItemsService : IGenericItemsService<ExternalItem>
    {
        IQueryable<ExternalItem> GetTop(int count, ExternalItemType type);

        IQueryable<ExternalItem> All(ExternalItemType type);
    }
}
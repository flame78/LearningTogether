namespace LearningTogether.Web.Models.ExternalItems
{
    using LearningTogether.Common;

    public class ExternalIndexViewModel
    {
        public PaginatedList<ExternalItemViewModel> Items { get; set; }

        public ExternalItemType Type { get; set; }

        public string Filter { get; set; }

        public int? CategoryId { get; set; }
    }
}
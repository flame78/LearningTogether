namespace LearningTogether.Web.ViewModels.ExternalItems
{
    using System.ComponentModel.DataAnnotations;

    using LearningTogether.Common;
    using LearningTogether.Data.Models;
    using LearningTogether.Web.Infrastructure.Mapping;

    public class ExternalItemUpdateModel : IMapFrom<ExternalItem>
    {
        public int Id { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        [StringLength(1000, ErrorMessage = "{0} cant be shorter {2} symbols and {1} longer  !", MinimumLength = 20)]
        public string Description { get; set; }

        [Required]
        [DataType(DataType.Url)]
        public string Link { get; set; }

        [Required]
        public ExternalItemType Type { get; set; }
    }
}

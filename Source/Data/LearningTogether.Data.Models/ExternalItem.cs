namespace LearningTogether.Data.Models
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using LearningTogether.Common;

    public class ExternalItem : BaseItem
    {
        [Required]
        public string Link { get; set; }

        [Required]
        public ExternalItemType Type { get; set; }

        public string ScreenShotName { get; set; }
    }
}

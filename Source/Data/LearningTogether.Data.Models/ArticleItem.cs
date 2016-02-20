namespace LearningTogether.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using LearningTogether.Data.Common.Models;

    public class ArticleItem : BaseItem
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }
    }
}

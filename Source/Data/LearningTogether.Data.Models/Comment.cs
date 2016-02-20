namespace LearningTogether.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using LearningTogether.Data.Common.Models;

    public class Comment : BaseModel<int>
    {
        [Required]
        public string Content { get; set; }

        [Required]
        public string AuthorId { get; set; }

        public virtual User Author { get; set; }
    }
}

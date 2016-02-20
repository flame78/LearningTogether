namespace LearningTogether.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using LearningTogether.Data.Common.Models;

    public abstract class BaseItem : BaseModel<int>
    {
        protected BaseItem()
        {
            this.Comments = new HashSet<Comment>();
            this.Ratings = new HashSet<Rating>();
            this.Categories = new HashSet<Category>();
        }

        [Required]
        public string AuthorId { get; set; }

        public virtual User Author { get; set; }

        [Required]
        public string Description { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public virtual ICollection<Rating> Ratings { get; set; }

        public virtual ICollection<Category> Categories { get; set; }
    }
}

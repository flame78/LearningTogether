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
            this.Tags = new HashSet<Tag>();
        }

        [Required]
        public string AuthorId { get; set; }

        public virtual User Author { get; set; }

        [Required]
        [StringLength(1000, ErrorMessage = "{0} cant be shorter {2} symbols and {1} longer  !", MinimumLength = 20)]
        public string Description { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public virtual ICollection<Rating> Ratings { get; set; }

        public virtual ICollection<Tag> Tags { get; set; }
    }
}

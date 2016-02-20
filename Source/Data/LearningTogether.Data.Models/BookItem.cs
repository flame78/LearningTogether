namespace LearningTogether.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class BookItem : BaseItem
    {
        public string ISBN { get; set; }

        [Required]
        public string BookAuthor { get; set; }

        [Required]
        public string Title { get; set; }
    }
}

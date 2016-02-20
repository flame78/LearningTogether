namespace LearningTogether.Data.Models
{
    using System.Collections.Generic;

    using LearningTogether.Data.Common.Models;

    public class Category : BaseModel<int>
    {
        public Category()
        {
            this.Items = new HashSet<BaseItem>();
        }

        public string Name { get; set; }

        public virtual ICollection<BaseItem> Items { get; set; }
    }
}

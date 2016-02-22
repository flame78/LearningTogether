using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningTogether.Data.Models
{
    using LearningTogether.Data.Common.Models;

    public class Tag : BaseModel<int>
    {
        public Tag()
        {
            this.Items = new HashSet<BaseItem>();
        }

        public string Name { get; set; }

        public virtual ICollection<BaseItem> Items { get; set; }
    }
}

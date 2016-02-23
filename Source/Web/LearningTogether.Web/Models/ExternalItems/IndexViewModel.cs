using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningTogether.Web.Models.ExternalItems
{
    using LearningTogether.Common;
    using LearningTogether.Web.ViewModels;

    public class IndexViewModel
    {
        public ICollection<ExternalItemViewModel> Items { get; set; }

        public ExternalItemType Type { get; set; }

        public int CurrentPage { get; set; }

        public int TotalPages { get; set; }

        public string Filter { get; set; }

        public string Category { get; set; }
    }
}

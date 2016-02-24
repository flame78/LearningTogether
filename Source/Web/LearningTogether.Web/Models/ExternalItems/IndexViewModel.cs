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
        public PaginatedList<ExternalItemViewModel> Items { get; set; }

        public ExternalItemType Type { get; set; }

        public string Filter { get; set; }

        public int? CategoryId { get; set; }
    }
}

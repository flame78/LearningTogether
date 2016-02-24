namespace LearningTogether.Web.Models.Home
{
    using System.Collections.Generic;

    using LearningTogether.Web.Models.ExternalItems;

    public class IndexViewModel
    {
        public ICollection<ExternalItemViewModel> Sites { get; set; }

        public ICollection<ExternalItemViewModel> Videos { get; set; }

        public ICollection<ExternalItemViewModel> Articles { get; set; }
    }
}

namespace LearningTogether.Web.ViewModels.Home
{
    using System.Collections.Generic;

    public class IndexViewModel
    {
        public ICollection<ExternalItemViewModel> Sites { get; set; }

        public ICollection<ExternalItemViewModel> Videos { get; set; }

        public ICollection<ExternalItemViewModel> Articles { get; set; }


    }
}

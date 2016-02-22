namespace LearningTogether.Web.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    using LearningTogether.Common;
    using LearningTogether.Data.Models;
    using LearningTogether.Services.Data;
    using LearningTogether.Web.Infrastructure.Mapping;
    using LearningTogether.Web.ViewModels;
    using LearningTogether.Web.ViewModels.Home;

    public class HomeController : BaseController
    {
        private readonly IGenericItemsService<ExternalItem> externalItemsService;

        public HomeController(IGenericItemsService<ExternalItem> externalItemsService)
        {
            this.externalItemsService = externalItemsService;
        }

        public ActionResult Index()
        {
            var sites = this.externalItemsService.All().Where(x => x.Type == ExternalItemType.Site).To<ExternalItemViewModel>().OrderBy( x => x.Rating).ToList();
            var articles =
                this.externalItemsService.All()
                    .Where(x => x.Type == ExternalItemType.Article)
                    .To<ExternalItemViewModel>()
                    .OrderBy(x => x.Rating)
                    .ToList();
            var viewModel = new IndexViewModel() { Sites = sites, Videos = new List<ExternalItemViewModel>(), Articles = articles };
            return this.View(viewModel);
        }
    }
}
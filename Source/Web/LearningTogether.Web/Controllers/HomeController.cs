namespace LearningTogether.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using LearningTogether.Data.Models;
    using LearningTogether.Services.Data;
    using LearningTogether.Web.Infrastructure.Mapping;
    using LearningTogether.Web.ViewModels;

    public class HomeController : BaseController
    {
        private readonly IGenericItemsService<ExternalItem> externalItemsService;

        public HomeController(IGenericItemsService<ExternalItem> externalItemsService)
        {
            this.externalItemsService = externalItemsService;
        }

        public ActionResult Index()
        {
            var items = this.externalItemsService.All().To<ExternalItemViewModel>().OrderBy( x => x.Rating).ToList();
            return this.View(items);
        }
    }
}
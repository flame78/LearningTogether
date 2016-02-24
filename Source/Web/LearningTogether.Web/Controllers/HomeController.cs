namespace LearningTogether.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;

    using LearningTogether.Common;
    using LearningTogether.Data.Models;
    using LearningTogether.Services.Data;
    using LearningTogether.Web.Infrastructure.Extensions;
    using LearningTogether.Web.Infrastructure.Mapping;
    using LearningTogether.Web.Models.ExternalItems;
    using LearningTogether.Web.Models.Home;

    using Microsoft.AspNet.Identity;

    public class HomeController : BaseController
    {
        private readonly IExternalItemsService externalItemsService;

        public HomeController(IExternalItemsService externalItemsService)
        {
            this.externalItemsService = externalItemsService;
        }

        public ActionResult Index()
        {
            var sites = this.externalItemsService.GetTop(3, ExternalItemType.Site).To<ExternalItemViewModel>().ToList();
            var articles =
                this.externalItemsService.GetTop(3, ExternalItemType.Article).To<ExternalItemViewModel>().ToList();
            var videos =
                this.externalItemsService.GetTop(3, ExternalItemType.Video).To<ExternalItemViewModel>().ToList();

            var viewModel = new IndexViewModel() { Sites = sites, Videos = videos, Articles = articles };

            return this.View(viewModel);
        }

        public JsonResult SetRating(int id, int vote)
        {
            var tryToVote = this.TryToVote(id, vote);

            if (tryToVote != null)
            {
                return tryToVote;
            }

            var item = this.externalItemsService.GetById(id);

            var itemVM = this.Mapper.Map<ExternalItemViewModel>(item);
            return
                this.Json(
                    new
                    {
                        Success = true,
                        Message = "Your Vote was cast successfully",
                        Result = new { Rating = itemVM.Rating.Rating, Raters = itemVM.Rating.Raters }
                    });
        }

        private JsonResult TryToVote(int id, int vote)
        {
            if (!this.Request.IsAuthenticated)
            {
                return this.Json(new { Success = false, Message = "Please log to Vote." });
            }

            if (this.externalItemsService.Rate(id, this.User.Identity.GetUserId(), vote))
            {
                return null;
            }

            return this.Json(new { Success = false, Message = "Sorry, you already voted for this item." });
        }
    }
}
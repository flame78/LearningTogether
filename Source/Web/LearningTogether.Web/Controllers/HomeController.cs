namespace LearningTogether.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
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
            var sites =
                this.externalItemsService.All()
                    .Where(x => x.Type == ExternalItemType.Site)
                    .To<ExternalItemViewModel>()
                    .OrderBy(x => x.Rating)
                    .Take(2)
                    .ToList();
            var articles =
                this.externalItemsService.All()
                    .Where(x => x.Type == ExternalItemType.Article)
                    .To<ExternalItemViewModel>()
                    .OrderBy(x => x.Rating)
                    .Take(2)
                    .ToList();
            var viewModel = new IndexViewModel()
                                {
                                    Sites = sites,
                                    Videos = new List<ExternalItemViewModel>(),
                                    Articles = articles
                                };
            return this.View(viewModel);
        }

        public JsonResult SetRating(int id, int vote)
        {
            try
            {
                //if (CanUserVote(id, rating) == false)
                //{
                //    return Json(new { Success = false, Message = "Sorry, you already voted for this post" });
                //}

                //   PostModel post = Engine.Posts.SetRating(id, rating);
                var item = this.externalItemsService.GetById(id);
                item.Ratings.Add(new Rating() { Value = vote });
                externalItemsService.Update(item);

                var itemVM = Mapper.Map<ExternalItemViewModel>(item);
                return
                    Json(

                        new {
                                Success = true,
                                Message = "Your Vote was cast successfully",
                                Result = new { Rating = itemVM.Rating.Rating, Raters = itemVM.Rating.Raters }
                            });
            }
            catch (Exception ex)
            {
                return Json(new { Success = false, Message = ex.Message });
            }
        }

        private Boolean CanUserVote(int id, double rating)
        {
            HttpCookie voteCookie = Request.Cookies["Votes"];
            if (voteCookie != null)
            {
                if (voteCookie[id.ToString()] != null)
                {
                    return false;
                }
            }

            //create the cookie and set the value 
            voteCookie = new HttpCookie("Votes");
            voteCookie[id.ToString()] = rating.ToString();
            Response.Cookies.Add(voteCookie);

            return true;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LearningTogether.Web.Controllers
{
    using LearningTogether.Common;
    using LearningTogether.Data.Models;
    using LearningTogether.Services.Data;
    using LearningTogether.Services.Web;
    using LearningTogether.Web.Infrastructure;
    using LearningTogether.Web.Infrastructure.Mapping;
    using LearningTogether.Web.Models;
    using LearningTogether.Web.Models.ExternalItems;
    using LearningTogether.Web.ViewModels;
    using LearningTogether.Web.ViewModels.ExternalItems;

    using Microsoft.AspNet.Identity;

    [Authorize]
    public class ExternalResourcesController : BaseController
    {
        private const int ItemsPerPage = 6;

        private readonly IFilesService filesService;

        private readonly IExternalItemsService externalItemsService;

        private readonly ICategoriesService categoriesService;


        public ExternalResourcesController(IExternalItemsService externalItemsService, ICategoriesService categoriesService, IFilesService filesService)
        {
            this.filesService = filesService;
            this.externalItemsService = externalItemsService;
            this.categoriesService = categoriesService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.Categories = this.GetCategories();

            var type = ExternalItemType.Site;
            var items = this.externalItemsService.All(type).To<ExternalItemViewModel>();
            var paginatedItems = new PaginatedList<ExternalItemViewModel>(items, 1, ItemsPerPage);
            var indexVm = new IndexViewModel() { Type = ExternalItemType.Site, Items = paginatedItems };

            return this.View(indexVm);
        }

        [HttpPost]
        public ActionResult Index(IndexViewModel ivm, int pageIndex)
        {
            var type = ivm.Type;
            var items = this.externalItemsService.All(type);

            if (ivm.CategoryId != null)
            {
               items = items.Where(x => x.CategoryId == ivm.CategoryId);
            }

            if (!string.IsNullOrWhiteSpace(ivm.Filter))
            {
                items = items.Where(x => x.Description.ToLower().Contains(ivm.Filter));
            }

            var filteredSortedItems = items.To<ExternalItemViewModel>();

            ViewBag.Categories = this.GetCategories();

            var paginatedItems = new PaginatedList<ExternalItemViewModel>(filteredSortedItems, pageIndex, ItemsPerPage);
            ivm.Items = paginatedItems;

            return this.View(ivm);
        }

        [HttpGet]
        public ActionResult Add()
        {
            ViewBag.Categories = this.GetCategories();

            return this.View();
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            ViewBag.Categories = this.GetCategories();

            var item = Mapper.Map<ExternalItemUpdateModel>(externalItemsService.GetById(id));
            return this.View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ExternalItemUpdateModel item, HttpPostedFileBase file)
        {
            if (this.ModelState.IsValid)
            {
                string screenShotName = null;

                var dbItem = externalItemsService.GetById(item.Id);

                if (file != null && file.ContentLength > 0)
                {
                    screenShotName = this.ScreenShotUpload(file);
                }

                if (screenShotName != null)
                {
                    dbItem.ScreenShotName = screenShotName;
                }

                dbItem.Description = item.Description;
                dbItem.Link = item.Link;
                dbItem.Type = item.Type;

                this.externalItemsService.Update(dbItem);
                return this.RedirectToAction("Index");
            }

            return this.View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(ExternalItemUpdateModel item, HttpPostedFileBase file)
        {
            if (this.ModelState.IsValid)
            {
                string screenShotName = null;

                if (file != null && file.ContentLength > 0)
                {
                    screenShotName = this.ScreenShotUpload(file);
                }

                var dbItem = new[] { item }.AsQueryable().To<ExternalItem>().First();
                if (screenShotName != null)
                {
                    dbItem.ScreenShotName = screenShotName;
                }

                dbItem.AuthorId = this.User.Identity.GetUserId();
                this.externalItemsService.Create(dbItem);
                return this.RedirectToAction("Index");
            }

            return this.View(item);
        }

        [ChildActionOnly]
        public string ScreenShotUpload(HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
            {
                if (file.ContentType.StartsWith("image/"))
                {
                    var fileName = this.filesService.SaveScreenShot(this.Server.MapPath(GlobalConstants.UploadsPath), file);
                    this.TempData["Succes"] = "Upload Succes.";
                    return fileName;
                }

                this.TempData["Error"] = "Invalid Image !";
            }
            else
            {
                this.TempData["Error"] = "Invalid File !";
            }

            return null;
        }

        [OutputCache(Duration = 10 * 60)]
        [ChildActionOnly]
        private List<SelectListItem> GetCategories()
        {
            var categories = this.categoriesService.GetAll()
                    .Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name })
                    .ToList();
            return categories;

        } 
    }
}
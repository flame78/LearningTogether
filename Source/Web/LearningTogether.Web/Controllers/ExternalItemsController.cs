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
    using LearningTogether.Web.Infrastructure.Mapping;
    using LearningTogether.Web.ViewModels.ExternalItems;

    using Microsoft.AspNet.Identity;

    [Authorize]
    public class ExternalItemsController : BaseController
    {
        private readonly IFilesService filesService;

        private readonly IGenericItemsService<ExternalItem> externalItemsService;

        public ExternalItemsController(IGenericItemsService<ExternalItem> externalItemsService, IFilesService filesService)
        {
            this.filesService = filesService;
            this.externalItemsService = externalItemsService;
        }

        // GET: ExternalItems
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Add()
        {
            return this.View();
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
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
                var screenShotName = this.ScreenShotUpload(file);
                if (screenShotName != null)
                {
                    var dbItem = new[] { item }.AsQueryable().To<ExternalItem>().First();
                    dbItem.ScreenShotName = screenShotName;
                    dbItem.AuthorId = this.User.Identity.GetUserId();
                    this.externalItemsService.Create(dbItem);
                    return this.RedirectToAction("Index");
                }
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
    }
}
namespace LearningTogether.Web.Controllers
{
    using System;
    using System.Drawing;
    using System.IO;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using LearningTogether.Common;
    using LearningTogether.Data;
    using LearningTogether.Data.Models;
    using LearningTogether.Services.Data;
    using LearningTogether.Services.Web;
    using LearningTogether.Web.Infrastructure.Mapping;
    using LearningTogether.Web.ViewModels;

    public class UploadController : BaseController
    {
        private readonly IFilesService filesService;

        private readonly IGenericItemsService<ExternalItem> externalItemsService;

        public UploadController(IGenericItemsService<ExternalItem> externalItemsService, IFilesService filesService)
        {
            this.filesService = filesService;
            this.externalItemsService = externalItemsService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var item = this.externalItemsService.All().To<ExternalItemViewModel>().First();
            return this.View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SiteScreenshotUpload(HttpPostedFileBase file, int id)
        {
            var item = this.externalItemsService.GetById(id);

            if (file != null && file.ContentLength > 0)
            {
                if (file.ContentType.StartsWith("image/"))
                {
                    var fileName = this.filesService.SaveScreenShot(this.Server.MapPath(GlobalConstants.UploadsPath), file);
                    item.ScreenShotName = fileName;
                    this.externalItemsService.Update(item);
                    this.TempData["Succes"] = "Upload Succes.";
                    return this.RedirectToAction("Index");
                }

                this.TempData["Error"] = "Invalid Image !";
            }
            else
            {
                this.TempData["Error"] = "Invalid File !";
            }

            return this.RedirectToAction("Index");
        }
    }
}
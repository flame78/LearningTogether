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
    using LearningTogether.Services.Web;
    using LearningTogether.Web.Infrastructure.Mapping;
    using LearningTogether.Web.ViewModels;

    public class UploadController : Controller
    {
        private LearningTogetherDbContext db = new LearningTogetherDbContext();

        private IFilesService filesService;

        public UploadController(IFilesService filesService)
        {
            this.filesService = filesService;
        }

        public ActionResult Index()
        {
            var item = db.ExternalItems.To<ItemViewModel>().First();

            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SiteScreenshotUpload(HttpPostedFileBase file, int id)
        {
            var item = db.ExternalItems.Find(id);

            if (file != null && file.ContentLength > 0)
            {
                if (file.ContentType.StartsWith("image/"))
                {
                    var fileName = this.filesService.SaveScreenShot(this.Server.MapPath(GlobalConstants.UploadsPath), file);
                    item.ScreenShotName = fileName;
                    db.SaveChanges();
                    this.TempData["Succes"] = "Upload Succes.";
                    return RedirectToAction("Index");
                }

                this.TempData["Error"] = "Invalid Image !";
            }
            else
            {
                this.TempData["Error"] = "Invalid File !";
            }

            return RedirectToAction("Index");
        }
    }
}
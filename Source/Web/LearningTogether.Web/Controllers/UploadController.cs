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
    using LearningTogether.Web.Infrastructure.Mapping;
    using LearningTogether.Web.ViewModels;

    public class UploadController : Controller
    {
        private const int ScreenShotWidth = 400;

        private const int ScreenShotHeight = 300;

        private LearningTogetherDbContext db = new LearningTogetherDbContext();

        public ActionResult Index()
        {
            var item = db.ExternalItems.To<ItemViewModel>().First();

            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SiteScreenshotUpload(HttpPostedFileBase file, int id)
        {
            var db = new LearningTogetherDbContext();

            var item = db.ExternalItems.Find(id);

            if (file != null && file.ContentLength > 0)
            {

                if (file.ContentType.StartsWith("image/"))
                {
                    try
                    {
                        Size newSize = new Size(ScreenShotWidth, ScreenShotHeight);
                        var bitmap = new Bitmap(file.InputStream);
                        var resizedBitmap = new Bitmap(bitmap, newSize);

                        var fileName = Guid.NewGuid().ToString() + ".png";

                        var pathString = Path.Combine(this.Server.MapPath(GlobalConstants.UploadsPath), fileName.Substring(0, 3));

                        if (!System.IO.Directory.Exists(pathString))
                        {
                            System.IO.Directory.CreateDirectory(pathString);
                        }

                        resizedBitmap.Save(Path.Combine(pathString, fileName),System.Drawing.Imaging.ImageFormat.Png);

                        item.ScreenShotName = fileName;
                        db.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }

            return RedirectToAction("Index");
        }
    }
}
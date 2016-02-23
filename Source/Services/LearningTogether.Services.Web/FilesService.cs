namespace LearningTogether.Services.Web
{
    using System;
    using System.Drawing;
    using System.IO;
    using System.Web;

    public class FilesService : IFilesService
    {
        private const int ScreenShotWidth = 300;

        private const int ScreenShotHeight = 230;

        public string SaveScreenShot(string uploadsPath, HttpPostedFileBase file)
        {
            string fileName;

            Size newSize = new Size(ScreenShotWidth, ScreenShotHeight);
            var bitmap = new Bitmap(file.InputStream);
            var resizedBitmap = new Bitmap(bitmap, newSize);
            string pathString;

            do
            {
                fileName = Guid.NewGuid().ToString() + ".scrsh";
                pathString = Path.Combine(uploadsPath, fileName.Substring(0, 1));
            }
            while (File.Exists(Path.Combine(pathString, fileName)));

            if (!Directory.Exists(pathString))
            {
                Directory.CreateDirectory(pathString);
            }

            resizedBitmap.Save(Path.Combine(pathString, fileName), System.Drawing.Imaging.ImageFormat.Png);
            return fileName;
        }
    }
}
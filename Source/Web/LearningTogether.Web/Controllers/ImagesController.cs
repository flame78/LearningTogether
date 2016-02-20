namespace LearningTogether.Web.Controllers
{
    using System.Drawing;
    using System.IO;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using System.Windows.Forms;

    public class ImagesController : Controller
    {
        public const float ZoomFactor = 0.5f;

        private bool isComplete = false;

        private MemoryStream stream = new MemoryStream();

        [HttpGet]
        public ActionResult Get(string encodedUrl)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(encodedUrl);
            var url = Encoding.UTF8.GetString(base64EncodedBytes);
         //   this.GetPictureStreamAsync(url);
            return new FileStreamResult(this.stream, "image/png");
        }

        private void GetPictureStreamAsync(string url)
        {
            this.MakePicture(url);

            while (isComplete == false)
            {
            }
        }

        private void MakePicture(string url)
        {
            Thread thread = new Thread(
                delegate()
                    {
                        using (WebBrowser browser = new WebBrowser())
                        {
                            browser.ScrollBarsEnabled = false;
                            browser.AllowNavigation = true;
                            browser.Navigate(url);
                            browser.Width = 1366;
                            browser.Height = 1024;
                            browser.ScriptErrorsSuppressed = true;
                            browser.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(DocumentCompleted);
                            while (browser.ReadyState != WebBrowserReadyState.Complete)
                            {
                                System.Windows.Forms.Application.DoEvents();
                            }
                        }
                    });
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
            thread.Join();
        }

        private void DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            WebBrowser browser = sender as WebBrowser;
            using (Bitmap bitmap = new Bitmap(browser.Width, browser.Height))
            {
                browser.DrawToBitmap(bitmap, new System.Drawing.Rectangle(0, 0, browser.Width, browser.Height));
                Size newSize = new Size((int)(browser.Width * ZoomFactor), (int)(browser.Height * ZoomFactor));
                var resizedBitmap = new Bitmap(bitmap, newSize);
                resizedBitmap.Save(this.stream, System.Drawing.Imaging.ImageFormat.Png);
                this.stream.Position = 0;
                this.isComplete = true;
            }
        }
    }
}
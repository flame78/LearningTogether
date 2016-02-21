namespace LearningTogether.Web.Infrastructure.Handlers
{
    using System.IO;
    using System.Web;

    using LearningTogether.Common;

    public class ScreenShotImageHttpHandler : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            var fileName = context.Request.Path;

            fileName = fileName.Substring(1);

            var pathString = Path.Combine(GlobalConstants.UploadsPath, fileName.Substring(0, 1));

            var screenShotFullName = Path.Combine(pathString, fileName);

            context.Response.ContentType = "image/png";
            context.Response.WriteFile(screenShotFullName, false);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
namespace LearningTogether.Services.Web
{
    using System.Web;

    public interface IFilesService
    {
        string SaveScreenShot(string uploadsPath, HttpPostedFileBase file);
    }
}
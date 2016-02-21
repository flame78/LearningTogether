using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningTogether.Services.Web
{
    using System.Web;

    public interface IFilesService
    {
        string SaveScreenShot(string uploadsPath, HttpPostedFileBase file);
    }
}

namespace LearningTogether.Web.Areas.Administration.Controllers
{
    using System.Web.Mvc;

    using LearningTogether.Common;
    using LearningTogether.Web.Controllers;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    public class AdministrationController : BaseController
    {
    }
}

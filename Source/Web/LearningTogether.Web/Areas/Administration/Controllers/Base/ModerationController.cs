namespace LearningTogether.Web.Areas.Administration.Controllers
{
    using System.Web.Mvc;

    using EasyPTC.Web.Areas.Administration.Controllers.Base;

    using LearningTogether.Common;

    [Authorize(Roles = GlobalConstants.ModeratorRoleName)]
    public class ModerationController<T> : KendoGridAbstractionController<T>
    {
    }
}

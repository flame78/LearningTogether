namespace LearningTogether.Web.Areas.Administration.Controllers.Base
{
    using System.Web.Mvc;

    using LearningTogether.Common;

    [Authorize(Roles = GlobalConstants.ModeratorRoleName)]
    public class ModerationController<T> : KendoGridAbstractionController<T>
    {
    }
}

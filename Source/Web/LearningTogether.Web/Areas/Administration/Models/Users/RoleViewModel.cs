namespace LearningTogether.Web.Areas.Administration.Models.Users
{
    using System.Web.Security;

    using LearningTogether.Web.Infrastructure.Mapping;

    using Microsoft.AspNet.Identity.EntityFramework;

    public class RoleViewModel : IMapFrom<IdentityUserRole>
    {
        public string Name { get; set; }

        public string Id { get; set; }
    }
}
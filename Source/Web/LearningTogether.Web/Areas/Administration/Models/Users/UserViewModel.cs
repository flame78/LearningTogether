namespace LearningTogether.Web.Areas.Administration.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web.Security;

    using AutoMapper;

    using LearningTogether.Data.Models;
    using LearningTogether.Web.Areas.Administration.Models.Users;
    using LearningTogether.Web.Infrastructure.Mapping;

    using Microsoft.AspNet.Identity.EntityFramework;

    using WebGrease.Css.Extensions;

    public class UserViewModel : IMapFrom<User>
    {
        public string Id { get; set; }

        [Required]
        public string Email { get; set; }

        public ICollection<string> Roles { get; set; }
    }
}
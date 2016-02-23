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

    public class UserViewModel : IMapFrom<User>, IHaveCustomMappings
    {
        public string Id { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public ICollection<string> Roles { get; set; }

        public void CreateMappings(IMapperConfiguration configuration)
        {
            configuration.CreateMap<User, UserViewModel>()
                .ForMember(x => x.Roles, opt => opt.MapFrom(y => y.Roles.Select(z => z.RoleId).ToList()));
        }
    }
}
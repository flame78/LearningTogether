namespace LearningTogether.Web.Areas.Administration.Models.Users
{
    using System.ComponentModel.DataAnnotations;

    public class UserEditModel : UserViewModel
    {
        [MinLength(5)]
        public string Password { get; set; }
    }
}
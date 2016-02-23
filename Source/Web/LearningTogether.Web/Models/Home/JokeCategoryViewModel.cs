namespace LearningTogether.Web.ViewModels.Home
{
    using LearningTogether.Data.Models;
    using LearningTogether.Web.Infrastructure.Mapping;

    public class JokeCategoryViewModel : IMapFrom<Category>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}

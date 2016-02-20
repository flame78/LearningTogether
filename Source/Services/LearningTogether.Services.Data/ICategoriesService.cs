namespace LearningTogether.Services.Data
{
    using System.Linq;

    using LearningTogether.Data.Models;

    public interface ICategoriesService
    {
        IQueryable<Category> GetAll();

        Category EnsureCategory(string name);
    }
}

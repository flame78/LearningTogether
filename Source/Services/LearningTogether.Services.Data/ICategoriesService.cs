namespace LearningTogether.Services.Data
{
    using System.Linq;

    using LearningTogether.Data.Models;

    public interface ICategoriesService
    {
        IQueryable<JokeCategory> GetAll();

        JokeCategory EnsureCategory(string name);
    }
}

namespace LearningTogether.Services.Data
{
    using System.Linq;

    using LearningTogether.Data.Models;

    public interface IJokesService
    {
        IQueryable<Comment> GetRandomJokes(int count);

        Comment GetById(string id);
    }
}

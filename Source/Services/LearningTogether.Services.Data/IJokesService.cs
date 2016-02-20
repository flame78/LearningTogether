namespace LearningTogether.Services.Data
{
    using System.Linq;

    using LearningTogether.Data.Models;

    public interface IJokesService
    {
        IQueryable<Joke> GetRandomJokes(int count);

        Joke GetById(string id);
    }
}

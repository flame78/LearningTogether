namespace LearningTogether.Services.Data
{
    using System;
    using System.Linq;

    using LearningTogether.Data.Common;
    using LearningTogether.Data.Models;
    using LearningTogether.Services.Web;

    public class JokesService : IJokesService
    {
        private readonly IDbRepository<Comment> jokes;
        private readonly IIdentifierProvider identifierProvider;

        public JokesService(IDbRepository<Comment> jokes, IIdentifierProvider identifierProvider)
        {
            this.jokes = jokes;
            this.identifierProvider = identifierProvider;
        }

        public Comment GetById(string id)
        {
            var intId = this.identifierProvider.DecodeId(id);
            var joke = this.jokes.GetById(intId);
            return joke;
        }

        public IQueryable<Comment> GetRandomJokes(int count)
        {
            return this.jokes.All().OrderBy(x => Guid.NewGuid()).Take(count);
        }
    }
}

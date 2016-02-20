namespace LearningTogether.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using LearningTogether.Data;
    using LearningTogether.Web.Infrastructure.Mapping;
    using LearningTogether.Web.ViewModels;

    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            var db = new LearningTogetherDbContext();

            var items = db.ExternalItems.To<ItemViewModel>().ToList();
            return this.View(items);
        }

        //private readonly IJokesService jokes;
        //private readonly ICategoriesService jokeCategories;

        //public HomeController(
        //    IJokesService jokes,
        //    ICategoriesService jokeCategories)
        //{
        //    this.jokes = jokes;
        //    this.jokeCategories = jokeCategories;
        //}

        //public ActionResult Index()
        //{
        //    var jokes = this.jokes.GetRandomJokes(3).To<JokeViewModel>().ToList();
        //    var categories =
        //        this.Cache.Get(
        //            "categories",
        //            () => this.jokeCategories.GetAll().To<JokeCategoryViewModel>().ToList(),
        //            30 * 60);
        //    var viewModel = new IndexViewModel
        //    {
        //        Jokes = jokes,
        //        Categories = categories
        //    };

        //    return this.View(viewModel);
        //}
    }
}

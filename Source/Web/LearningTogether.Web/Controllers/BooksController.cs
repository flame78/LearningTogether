namespace LearningTogether.Web.Controllers
{
    using System.Web.Mvc;

    using LearningTogether.Data.Models;
    using LearningTogether.Services.Data;
    using LearningTogether.Web.Infrastructure.Extensions;
    using LearningTogether.Web.Models;
    using LearningTogether.Web.Models.Books;

    [Authorize]
    public class BooksController : BaseController
    {
        private const int ItemsPerPage = 5;

        private readonly IGenericItemsService<BookItem> booksService;

        public BooksController(IGenericItemsService<BookItem> booksService)
        {
            this.booksService = booksService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var items = this.booksService.All().To<BookItemViewModel>();
            var paginatedItems = new PaginatedList<BookItemViewModel>(items, 1, ItemsPerPage);

            return this.View(paginatedItems);
        }

        [HttpPost]
        public ActionResult Index(int pageIndex)
        {
            var items = this.booksService.All().To<BookItemViewModel>();
            var paginatedItems = new PaginatedList<BookItemViewModel>(items, pageIndex, ItemsPerPage);

            return this.View(paginatedItems);
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            var book = this.Mapper.Map<BookItem>(this.booksService.GetById(id));

            return this.View(book);
        }
    }
}
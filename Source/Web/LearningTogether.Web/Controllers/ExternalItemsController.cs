using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LearningTogether.Web.Controllers
{
    using LearningTogether.Web.ViewModels.ExternalItems;

    [Authorize]
    public class ExternalItemsController : Controller
    {
        // GET: ExternalItems
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Add()
        {
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(ExternalItemAddModel item)
        {
            if (this.ModelState.IsValid)
            {
                // make magic
            }

            return this.View(item);
        }
    }
}
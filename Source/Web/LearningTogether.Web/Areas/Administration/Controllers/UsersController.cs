using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using LearningTogether.Data.Models;

namespace LearningTogether.Web.Areas.Administration.Controllers
{
    using System.Web.Script.Serialization;
    using System.Web.UI.WebControls;

    using LearningTogether.Data;
    using LearningTogether.Web.Areas.Administration.Models;
    using LearningTogether.Web.Infrastructure.Mapping;

    using Microsoft.AspNet.Identity.EntityFramework;

    public class UsersController : AdministrationController
    {
        private IdentityDbContext<User> db;

        public UsersController(IdentityDbContext<User> db)
        {
            this.db = db;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Users_Read([DataSourceRequest]DataSourceRequest request)
        {
            var users = db.Users.To<UserViewModel>();

            return Json(users.ToDataSourceResult(request));
        }

        public ActionResult Roles()
        {
            if (!Request.IsAjaxRequest())
            {
                return this.RedirectToAction("Index", "Home", new { area = string.Empty });
            }

            var roles = db.Roles.ToDictionary(x => x.Id, y => y.Name);
            return Json(roles, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Users_Destroy([DataSourceRequest]DataSourceRequest request, UserViewModel user)
        {
            if (ModelState.IsValid)
            {
                var dbUser = this.db.Users.Find(user.Id);
                db.Users.Remove(dbUser);
                db.SaveChanges();
            }

            return Json(new[] { user }.ToDataSourceResult(request, ModelState));
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}

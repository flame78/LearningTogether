﻿using System;
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
    using LearningTogether.Data;

    public class CategoriesController : Controller
    {
        private LearningTogetherDbContext db = new LearningTogetherDbContext();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Categories_Read([DataSourceRequest]DataSourceRequest request)
        {
            IQueryable<Category> categories = db.Categories;
            DataSourceResult result = categories.ToDataSourceResult(request, category => new {
                Id = category.Id,
                Name = category.Name,
                CreatedOn = category.CreatedOn,
                ModifiedOn = category.ModifiedOn,
                IsDeleted = category.IsDeleted,
                DeletedOn = category.DeletedOn
            });

            return Json(result);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Categories_Create([DataSourceRequest]DataSourceRequest request, Category category)
        {
            if (ModelState.IsValid)
            {
                var entity = new Category
                {
                    Name = category.Name,
                    CreatedOn = category.CreatedOn,
                    ModifiedOn = category.ModifiedOn,
                    IsDeleted = category.IsDeleted,
                    DeletedOn = category.DeletedOn
                };

                db.Categories.Add(entity);
                db.SaveChanges();
                category.Id = entity.Id;
            }

            return Json(new[] { category }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Categories_Update([DataSourceRequest]DataSourceRequest request, Category category)
        {
            if (ModelState.IsValid)
            {
                var entity = new Category
                {
                    Id = category.Id,
                    Name = category.Name,
                    CreatedOn = category.CreatedOn,
                    ModifiedOn = category.ModifiedOn,
                    IsDeleted = category.IsDeleted,
                    DeletedOn = category.DeletedOn
                };

                db.Categories.Attach(entity);
                db.Entry(entity).State = EntityState.Modified;
                db.SaveChanges();
            }

            return Json(new[] { category }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Categories_Destroy([DataSourceRequest]DataSourceRequest request, Category category)
        {
            if (ModelState.IsValid)
            {
                var entity = new Category
                {
                    Id = category.Id,
                    Name = category.Name,
                    CreatedOn = category.CreatedOn,
                    ModifiedOn = category.ModifiedOn,
                    IsDeleted = category.IsDeleted,
                    DeletedOn = category.DeletedOn
                };

                db.Categories.Attach(entity);
                db.Categories.Remove(entity);
                db.SaveChanges();
            }

            return Json(new[] { category }.ToDataSourceResult(request, ModelState));
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}

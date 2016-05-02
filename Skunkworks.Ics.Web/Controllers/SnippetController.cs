using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Skunkworks.Ics.Web.Models;
using Skunkworks.Ics.Web.Repositories;

namespace Skunkworks.Ics.Web.Controllers
{
    public class SnippetController : Controller
    {
        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }


        // GET: Snippet
        public ActionResult Index()
        {
            

            var rep = new SnippetRepository();
            var model = rep.Read();
            return View(model);
        }

        // GET: Snippet/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Snippet/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Snippet/Create
        [HttpPost]
        public ActionResult Create(SnippetModel model)
        {
            try
            {
                // TODO: Add insert logic here
                var userId = User.Identity.GetUserId<int>();
                var details = UserManager.FindById(userId);
                model.UserName = details.Email;

                var rep = new SnippetRepository();
                rep.Create(model.UserName,model.Title,model.Language,model.Code);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Snippet/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Snippet/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Snippet/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Snippet/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}

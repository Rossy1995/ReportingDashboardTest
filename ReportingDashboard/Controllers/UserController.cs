using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ReportingDashboard.Models;
namespace ReportingDashboard.Controllers
{
	public class UserController:Controller
	{
	   private ReportingDashboard.Models.GC db = new ReportingDashboard.Models.GC();
	   
	     //
        // GET: /User/

        public ActionResult Index()
        {
            return View(db.User.ToList());
        }

        //
        // GET: /Account/Register
        [Authorize]
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                db.User.Add(new Models.User { name = user.name, pass = user.pass });
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }

            return View(user);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
	}
}

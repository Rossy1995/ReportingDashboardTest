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
	    private DbAccessContext db = new DbAccessContext();

        //
        // GET: /User/
        [Auth(Roles = "Admin")]
        public ActionResult Index()
        {
            var report = from r in db.User
                         select r;

            return View(new ReportsViewModel()
            {
                Users = report.ToList()
            });
        }

        //
        // GET: /Account/Register
        [Auth(Roles = "Admin")]
        public ActionResult RegisterUser()
        {
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult RegisterUser(user user)
        { 
            if (ModelState.IsValid)
            {
                db.User.Add(new user { name = user.name, pass = user.pass });
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

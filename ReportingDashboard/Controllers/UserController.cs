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
	    private String hash;

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
                String hash = md5(user.pass);
                db.User.Add(new user { name = user.name, pass = hash });
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

	    private string md5(string sPassword)
	    {
	        System.Security.Cryptography.MD5CryptoServiceProvider x = new System.Security.Cryptography.MD5CryptoServiceProvider();
	        byte[] bs = System.Text.Encoding.UTF8.GetBytes(sPassword);
	        bs = x.ComputeHash(bs);
	        System.Text.StringBuilder s = new System.Text.StringBuilder();
	        foreach (byte b in bs)
	        {
	            s.Append(b.ToString("x2").ToLower());
	        }
	        return s.ToString();
	    }
    }
}

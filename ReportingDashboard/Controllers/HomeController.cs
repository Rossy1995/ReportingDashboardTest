using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ReportingDashboard.Models;
namespace ReportingDashboard.Controllers
{
    [RequireHttps]
    public class HomeController : Controller
    {

        private ReportingDashboard.Models.GC db = new ReportingDashboard.Models.GC();


        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult Reports()
        {
            return View(db.UserTime.ToList());
        }
    }
}
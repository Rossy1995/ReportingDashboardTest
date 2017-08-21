using ClosedXML.Excel;
using ReportingDashboard.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace ReportingDashboard.Controllers
{
    [RequireHttps]
    public class HomeController : Controller
    {
        public List<UserTime> GetReportList()
        {
            DbAccessContext db = new DbAccessContext();

            return (from r in db.UserTime
                     select r).ToList();
        }

        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult Reports()
        {
            return View(this.GetReportList());
        }
    }
}
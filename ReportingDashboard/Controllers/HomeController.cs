using ReportingDashboard.Models;
using System;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI.WebControls;
namespace ReportingDashboard.Controllers
{
    [RequireHttps]
    public class HomeController : Controller
    {
        private DbAccessContext db = new DbAccessContext();

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetAllCheckIns()
        {
            Models.GC obj = new Models.GC();
            var users = obj.UserTime.Select(x => new
            {
                Username = x.username,
                cTime = x.cTime,
                InOrOut = x.InOROut
            }).ToList();
            return Json(users, JsonRequestBehavior.AllowGet);
        }

        [Authorize]       
        public ActionResult Reports(string search, string sortOrder)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "user_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            var report = from r in db.UserTime
                         select r;

            if (!String.IsNullOrEmpty(search))
            {
                report = report.Where(r => r.username.Contains(search));
            }

            switch (sortOrder)
            {
                case "User":
                    report = report.OrderBy(r => r.username);
                    break;
                case "user_desc":
                    report = report.OrderByDescending(r => r.username);
                    break;
                case "Date":
                    report = report.OrderBy(r => r.cDate);
                    break;
                case "date_desc":
                    report = report.OrderByDescending(r => r.cDate);
                    break;
            }

            return View(new ReportsViewModel()
            {
                userTimes = report.ToList(),
                SearchText = search
            });
        }

        [Authorize]
        public ActionResult ExportToCSV(string search)
        {            
            StringWriter sw = new StringWriter();
            var responseList = from r in db.UserTime
                               select r;

            sw.WriteLine("\"Username\",\"Current Day\",\"Current Time\",\"In or Out\",\"Current Date\"");

            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment; filename=Report.csv");
            Response.ContentType = "text/csv";

            if (!String.IsNullOrEmpty(search))
            {
                responseList = responseList.Where(r => r.username.Contains(search)).OrderBy(r => r.cTime);
            }

            foreach (var item in responseList)
            {
                sw.WriteLine($"\"{item.username}\",\"{item.cDay}\",\"{item.cTime}\",\"{item.InOROut}\",\"{item.cDate}\"");
            }

            Response.Write(sw.ToString());

            Response.End();

            return View(new ReportsViewModel()
            {
                userTimes = responseList.ToList(),
                SearchText= search
            });

        }
    }
}
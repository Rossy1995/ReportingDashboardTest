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
        public ActionResult Reports(string searchString)
        {
            DbAccessContext db = new DbAccessContext();
            var report = from r in db.UserTime
                         select r;
            if (!String.IsNullOrEmpty(searchString))
            {
                report = report.Where(r => r.username.Contains(searchString));
            }
            return View(report.ToList());
        }

        [Authorize]
        public ActionResult ExportToCSV(string searchString)
        {
            StringWriter sw = new StringWriter();
            sw.WriteLine("\"Username\",\"Current Day\",\"Current Time\",\"In or Out\",\"Current Date\"");

            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachment; filename=Report.csv");
            Response.ContentType = "text/csv";
            DbAccessContext db = new DbAccessContext();
            var responseList = from r in db.UserTime
                         select r;
            if (!String.IsNullOrEmpty(searchString))
            {
                responseList = responseList.Where(x => x.username.Contains(searchString)).OrderBy(x => x.cTime);
            }
            //responseList.Select(item => sw.WriteLine($"\"{item.username}\",\"Current Day\",\"Current Time\",\"In or Out\",\"Current Date\""));
            foreach(var item in responseList)
            {
                sw.WriteLine($"\"{item.username}\",\"{item.cDay}\",\"{item.cTime}\",\"{item.InOROut}\",\"{item.cDate}\"");
            }

            Response.Write(sw.ToString());

            Response.End();

            return View(responseList);
        }
    }
}
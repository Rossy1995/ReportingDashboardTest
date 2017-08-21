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
        public ReportsViewModel GetReportList()
        {
            DbAccessContext db = new DbAccessContext();
            var responseList = (from r in db.UserTime
                                select r).ToList();
            return new ReportsViewModel()
            {
                userTimes = responseList
            };
        }

        public ActionResult Index()
        {
            return View();
        }

        [Authorize]       
        public ActionResult Reports(string search)
        {
            DbAccessContext db = new DbAccessContext();
            var report = from r in db.UserTime
                         select r;
           
            if (!String.IsNullOrEmpty(search))
                {
                    report = report.Where(r => r.username.Contains(search));
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
            DbAccessContext db = new DbAccessContext();
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
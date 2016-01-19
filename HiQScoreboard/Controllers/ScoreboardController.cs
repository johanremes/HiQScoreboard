using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HiQScoreboard.Controllers
{
    public class ScoreboardController : Controller
    {

        ScoreboardDBEntities db = new ScoreboardDBEntities();

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetScoreboardResultsJSON()
        {
            var data = db.ScoreboardResults.ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        
        public IEnumerable<ScoreboardResults> GetScoreboardResultsIEnum()
        {
            return db.ScoreboardResults;
        }

        [HttpGet]
        public ActionResult List()
        {
            return View(GetScoreboardResultsIEnum());
        }

        [HttpGet]
        public ActionResult Create()
        {
            HttpCookie settingsCookie = HttpContext.Request.Cookies.Get(
                Guid.NewGuid().ToString());

            if (settingsCookie == null)
            {
                return Redirect("Settings/Index");
            }
            

            // Check cookie
            HttpCookie cookie = HttpContext.Request.Cookies.Get("Confirmed");
            if ((cookie != null))
                ViewData["Confirmed"] = "You've already answered";
            else
                ViewData["Confirmed"] = "Not Confirmed";

            var scoreboardResult = new ScoreboardResults();

            ViewBag.Questions = new List<string>()
            {
                { "Uppdraget"},
                { "Ledningen"},
                { "HiQ"},
                { "Kollegorna"}
            };

            //ViewBag.Offices = db.Offices.ToList();

            return View(scoreboardResult);
        }

        [HttpPost]
        public ActionResult Create(ScoreboardResults scoreboardResult)
        {
            // Check if cookie exists
            if (HttpContext.Request.Cookies.Get("Confirmed") != null)
            {
                ViewData["Confirmed"] = "You've already answered";
                return View(scoreboardResult);
            }

            try
            {
                scoreboardResult.Date = DateTime.Now;
                db.ScoreboardResults.Add(scoreboardResult);
                db.SaveChanges();

                ViewData["Confirmed"] = "Your answer has been Confirmed";

                // Create and set cookie
                HttpCookie cookie = new HttpCookie("Confirmed", "Created"); // Set cookie value = "True"
                cookie.Expires = DateTime.Today.AddDays(1); // Expires at midnight
                Response.SetCookie(cookie);
            }
            catch (Exception e)
            {
                ViewData["Confirmed"] = "Not Confirmed " + e.ToString();
            }

            return View(scoreboardResult);
        }

        [HttpGet]
        public void ExportToExcel()
        {
            GridView gv = new GridView();
            gv.DataSource = db.ScoreboardResults.ToList();
            gv.DataBind();
            Response.ClearContent();
            Response.Buffer = true;
            Response.ContentType = "application/ms-excel";
            Response.AddHeader("content-disposition", "attachment; filename=Scorelist.xls");
            Response.Charset = "";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            gv.RenderControl(htw);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }

        [HttpGet]
        public void DeleteCookie()
        {
            HttpCookie cookie = HttpContext.Request.Cookies.Get("Confirmed");
            if (cookie != null)
            {
                cookie.Expires = DateTime.Today.AddDays(-1); // Expires NOW!
                Response.SetCookie(cookie);
            }
        }
    }
}
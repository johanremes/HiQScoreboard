using System;
using System.Collections.Generic;
using System.Data;
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
            var data = db.scoreboardresults.ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public IEnumerable<scoreboardresults> GetScoreboardResultsIEnum()
        {
            return db.scoreboardresults;
        }

        [HttpGet]
        public ActionResult List()
        {
            return View(GetScoreboardResultsIEnum());
        }

        [HttpGet]
        public ActionResult Create()
        {
            // Check cookie
            HttpCookie cookie = HttpContext.Request.Cookies.Get("Confirmed");
            if ((cookie != null))
                ViewData["Confirmed"] = "You've already answered";
            else
                ViewData["Confirmed"] = "Not Confirmed";

            var scoreboardresult = new scoreboardresults();

            // Default data
            scoreboardresult.q1 = 50;
            scoreboardresult.q2 = 50;
            scoreboardresult.q3 = 50;
            scoreboardresult.q4 = 50;
            ViewData["q1"] = scoreboardresult.q1;
            ViewData["q2"] = scoreboardresult.q2;
            ViewData["q3"] = scoreboardresult.q3;
            ViewData["q4"] = scoreboardresult.q4;

            return View(scoreboardresult);
        }

        [HttpPost]
        public ActionResult Create(scoreboardresults scoreboardresult)
        {
            // Check if cookie exists
            if (HttpContext.Request.Cookies.Get("Confirmed") != null)
            {
                ViewData["Confirmed"] = "You've already answered";
                return View(scoreboardresult);
            }

            try
            {
                scoreboardresult.date = DateTime.Now;
                db.scoreboardresults.Add(scoreboardresult);
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

            return View(scoreboardresult);
        }

        [HttpGet]
        public void ExportToExcel()
        {
            GridView gv = new GridView();
            gv.DataSource = db.scoreboardresults.ToList();
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
    }
}
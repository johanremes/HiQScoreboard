using HiQScoreboard.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HiQScoreboard.Controllers
{
    public class SettingsController : Controller
    {
        ScoreboardDBEntities db = new ScoreboardDBEntities();

        // GET: Settings
        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.Offices = db.Offices.ToList();
            SettingsModel s = new SettingsModel();
            return View(s);
        }

        // POST: Settings Save
        [HttpPost]
        public ActionResult Index(SettingsModel settings)
        {
            HttpCookie cookie = new HttpCookie("_Settings", settings.Office.Id + "-" + settings.AllowPush);
            
            cookie.Expires = DateTime.Today.AddDays(1); // Expires at midnight
            Response.SetCookie(cookie);
            return RedirectToAction("Create", "Scoreboard");
        }
    }
}
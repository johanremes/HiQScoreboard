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
            // Create and set cookie
            HttpCookie cookie = new HttpCookie("", "Created"); // Set cookie value = "True"
            cookie.Expires = DateTime.Today.AddDays(1); // Expires at midnight
            Response.SetCookie(cookie);
            ViewBag.Offices = db.Offices.ToList();
            SettingsModel s = new SettingsModel();
            return View();
        }

        // POST: Settings Save
        [HttpPost]
        public ActionResult Index(SettingsModel settings)
        {
            
            return View();
        }
    }
}
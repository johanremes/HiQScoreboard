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
        public ActionResult Index()
        {
            ViewBag.Offices = db.Offices.ToList();
            return View();
        }
    }
}
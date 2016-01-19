using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
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

            ViewBag.Offices = db.Offices.ToList();

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

        public void Push()
        {
            string deviceId = "cyiFuRcpq9c:APA91bE6Iq9PMKALQFs1yu8EulkOPeCl1SoxFb4MVkM0FZROAcy4amLjIbSmQlYcybTdq7MMB14w8hItAWAVFCulTYzZVgC1ztFgZ3jIXv0PSBOyxszShxTf_AScm8B5xrFR2wd4OKK8";
            string postData = "{ \"registration_ids\": [ \"" + deviceId + "\" ]}";

            string response = SendGCMNotification(postData);
        }
        
          
        /// Send a Google Cloud Message. Uses the GCM service and your provided api key.
        private string SendGCMNotification(String data)
        {
            string apiKey = "AIzaSyCEKtCjytfpjYj1lIBBdh4HfmW07fxrhLs";
            string postData = data;
            string postDataContentType = "application/json";
            ServicePointManager.ServerCertificateValidationCallback += new RemoteCertificateValidationCallback(ValidateServerCertificate);

            //
            //  MESSAGE CONTENT
            byte[] byteArray = Encoding.UTF8.GetBytes(postData);

            //
            //  CREATE REQUEST
            HttpWebRequest Request = (HttpWebRequest)WebRequest.Create("https://android.googleapis.com/gcm/send");
            Request.Method = "POST";
            Request.KeepAlive = false;
            Request.ContentType = postDataContentType;
            Request.Headers.Add(string.Format("Authorization: key={0}", apiKey));
            Request.ContentLength = byteArray.Length;

            Stream dataStream = Request.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();

            //
            //  SEND MESSAGE
            try
            {
                WebResponse Response = Request.GetResponse();
                HttpStatusCode ResponseCode = ((HttpWebResponse)Response).StatusCode;
                if (ResponseCode.Equals(HttpStatusCode.Unauthorized) || ResponseCode.Equals(HttpStatusCode.Forbidden))
                {
                    var text = "Unauthorized - need new token";
                }
                else if (!ResponseCode.Equals(HttpStatusCode.OK))
                {
                    var text = "Response from web service isn't OK";
                }

                StreamReader Reader = new StreamReader(Response.GetResponseStream());
                string responseLine = Reader.ReadToEnd();
                Reader.Close();

                return responseLine;
            }
            catch (Exception e)
            {
            }
            return "error";
        }


        public static bool ValidateServerCertificate(
                                                    object sender,
                                                    X509Certificate certificate,
                                                    X509Chain chain,
                                                    SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }
    }
}
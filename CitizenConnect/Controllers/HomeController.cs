using CitizenConnect.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CitizenConnect.Models;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace CitizenConnect.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Meet the Developers of Citizen Connect";

            return View();
        }
        public ActionResult Ward()
        {
            ViewBag.Message = "Your City Council Member.";

            return View();
        }

        public ActionResult Resources()
        {
            ViewBag.Message = "Additional Resourses.";

            return View();
        }
        public ActionResult Lookup()
        {
            ViewBag.Message = "Address Lookup Page.";

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SendEmail(EmailFormModel emailModel, ReportView ReportModel)
        {
            if (ModelState.IsValid)
            {
                var body = "<p>11/28/2016</p><p>Dear Council Person,</p><p>Following are concerns submitted by citizens on citizenconnect.com.</p><p> For the week of November 28 - December 4: </p><p>- There have been 14 reported incidents of street light outtages. Please click *here* to view the location.</p><p>- There have been 19 reports of potholes. Please click *here* to view the location.</p><p></p><p>Thank you for prompt attention to these concerns. We apprecite the time and effort each of you take in making Cleveland a great place to live.</p><p></p><p>Sincerely,</p><p>Citizen Connect Administration</p>";
                var message = new MailMessage();
                message.Bcc.Add(new MailAddress("CitizenConnectCle@gmail.com"));  // replace with valid value 
                message.Bcc.Add(new MailAddress("citizenconnectcounciltest@gmail.com"));
                message.From = new MailAddress("CitizenConnectCle@gmail.com");  // replace with valid value
                message.Subject = "Weekly Reports From Citizen Connect";
                message.Body = string.Format(body);
                message.IsBodyHtml = true;
                using (var smtp = new SmtpClient())
                {
                    var credential = new NetworkCredential
                    {
                        UserName = "CitizenConnectCle@gmail.com",  // replace with valid value
                        Password = "Password.1"  // replace with valid value
                    };
                    smtp.Credentials = credential;
                    smtp.Host = "smtp.gmail.com";
                    smtp.Port = 587;
                    smtp.EnableSsl = true;
                    await smtp.SendMailAsync(message);
                    return RedirectToAction("Sent");
                }
            }
            return View(emailModel);
        }
        public ActionResult Sent()
        {
            return View();
        }
        public ActionResult SendEmail()
        {
            return View();
        }
    }
}
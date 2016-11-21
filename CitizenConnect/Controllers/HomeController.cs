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
                var body = "<p>Email From: {0} ({1})</p><p>Message:</p><p>{2}</p>";
                var message = new MailMessage();
                message.Bcc.Add(new MailAddress("CitizenConnectCle@gmail.com"));  // replace with valid value 
                message.Bcc.Add(new MailAddress("Christopher.M.Betteley@gmail.com"));
                message.From = new MailAddress("CitizenConnectCle@gmail.com");  // replace with valid value
                message.Subject = "Weekly Reports From Citizen Connect";
                message.Body = string.Format(body, emailModel.FromName, emailModel.FromEmail, emailModel.Message);
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
            ViewBag.Message = "Your application description page.";

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
    }
}
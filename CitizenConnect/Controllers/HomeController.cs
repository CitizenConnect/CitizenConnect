using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CitizenConnect.Models;

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

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Lookup()
        {
            ViewBag.Message = "Address Lookup Page.";

            return View();
        }
        public JsonResult GetAddress()
        {
            WardInfo address = new WardInfo();
            return Json(address.getAddress(), JsonRequestBehavior.AllowGet);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CitizenConnect.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace CitizenConnect.Controllers
{
    public class ReportViewsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        private ApplicationUser CurrentUser
        {
            get
            {
                UserManager<ApplicationUser> UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
                ApplicationUser currentUser = UserManager.FindById(User.Identity.GetUserId());
                return currentUser;
            }
        }

        public ActionResult WeeklyReport()
        {
            return View();
        }

        // GET: ReportViews
        [Authorize]
        public ActionResult Index()
        {
            return View(db.ReportViews.ToList());
        }


        // GET: ReportViews/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReportView reportView = db.ReportViews.Find(id);
            if (reportView == null)
            {
                return HttpNotFound();
            }
            return View(reportView);
        }

        // GET: ReportViews/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.ReportTypeID = new SelectList(db.ReportTypes, "ReportTypeID", "ReportTypeName");
            return View();
        }

        // POST: ReportViews/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ReportID,Longitude,AddressString,PlaceID,Latitude,ReportTypeID,TimeStamp")] ReportView reportView)
        {
            reportView.TimeStamp = DateTime.Now;
            reportView.ApplicationUser = CurrentUser;
            if (ModelState.IsValid)
            {
                
                db.ReportViews.Add(reportView);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ReportTypeID = new SelectList(db.ReportTypes, "ReportTypeID", "ReportTypeName", reportView.ReportTypeID);
            return View(reportView);
        }

        // GET: ReportViews/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReportView reportView = db.ReportViews.Find(id);
            if (reportView == null)
            {
                return HttpNotFound();
            }
            return View(reportView);
        }

        // POST: ReportViews/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ReportID,Longitude,Latitude,ReportTypeID")] ReportView reportView)
        {
            if (ModelState.IsValid)
            {
                db.Entry(reportView).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(reportView);
        }

        // GET: ReportViews/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReportView reportView = db.ReportViews.Find(id);
            if (reportView == null)
            {
                return HttpNotFound();
            }
            return View(reportView);
        }

        // POST: ReportViews/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ReportView reportView = db.ReportViews.Find(id);
            db.ReportViews.Remove(reportView);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CitizenConnect.Models;

namespace CitizenConnect.Controllers
{
    public class ReportViewsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ReportViews
        public ActionResult Index()
        {
            return View(db.ReportViews.ToList());
        }

        // GET: ReportViews/Details/5
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
        public ActionResult Create()
        {
            return View();
        }

        // POST: ReportViews/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ReportID,Longitude,Latitude,ReportTypeID")] ReportView reportView)
        {
            if (ModelState.IsValid)
            {
                db.ReportViews.Add(reportView);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(reportView);
        }

        // GET: ReportViews/Edit/5
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

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
    public class InterestedVolunteersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: InterestedVolunteers
        public ActionResult Index()
        {
            var interestedVolunteers = db.InterestedVolunteers.Include(i => i.ProjectView);
            return View(interestedVolunteers.ToList());
        }

        // GET: InterestedVolunteers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InterestedVolunteers interestedVolunteers = db.InterestedVolunteers.Find(id);
            if (interestedVolunteers == null)
            {
                return HttpNotFound();
            }
            return View(interestedVolunteers);
        }

        // GET: InterestedVolunteers/Create
        public ActionResult Create()
        {
            ViewBag.ProjectID = new SelectList(db.ProjectViews, "ProjectID", "ProjectName");
            return View();
        }

        // POST: InterestedVolunteers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "InterestedUserID,IfInterested,ProjectID")] InterestedVolunteers interestedVolunteers)
        {
            if (ModelState.IsValid)
            {
                db.InterestedVolunteers.Add(interestedVolunteers);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProjectID = new SelectList(db.ProjectViews, "ProjectID", "ProjectName", interestedVolunteers.ProjectID);
            return View(interestedVolunteers);
        }

        // GET: InterestedVolunteers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InterestedVolunteers interestedVolunteers = db.InterestedVolunteers.Find(id);
            if (interestedVolunteers == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProjectID = new SelectList(db.ProjectViews, "ProjectID", "ProjectName", interestedVolunteers.ProjectID);
            return View(interestedVolunteers);
        }

        // POST: InterestedVolunteers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "InterestedUserID,IfInterested,ProjectID")] InterestedVolunteers interestedVolunteers)
        {
            if (ModelState.IsValid)
            {
                db.Entry(interestedVolunteers).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProjectID = new SelectList(db.ProjectViews, "ProjectID", "ProjectName", interestedVolunteers.ProjectID);
            return View(interestedVolunteers);
        }

        // GET: InterestedVolunteers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InterestedVolunteers interestedVolunteers = db.InterestedVolunteers.Find(id);
            if (interestedVolunteers == null)
            {
                return HttpNotFound();
            }
            return View(interestedVolunteers);
        }

        // POST: InterestedVolunteers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            InterestedVolunteers interestedVolunteers = db.InterestedVolunteers.Find(id);
            db.InterestedVolunteers.Remove(interestedVolunteers);
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

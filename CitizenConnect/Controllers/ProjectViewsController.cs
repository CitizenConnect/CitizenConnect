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
using CitizenConnect.Migrations;

namespace CitizenConnect.Controllers
{
    public class ProjectViewsController : Controller
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

        // GET: ProjectViews
        [Authorize]
        public ActionResult Index()
        {
            var projectviews = db.ProjectViews.Include(i => i.InterestedVolunteers);
            return View(db.ProjectViews.ToList());
        }

        // GET: ProjectViews/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProjectView projectView = db.ProjectViews.Find(id);
            
            if (projectView == null)
            {
                return HttpNotFound();
            }
            return View(projectView);
        }

        // GET: ProjectViews/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProjectViews/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProjectID,ProjectName,ProjectDescription,CreationDate")] ProjectView projectView)
        {
            projectView.ApplicationUser = CurrentUser;
            projectView.CreationDate = DateTime.Now;
            if (ModelState.IsValid)
            {
                db.ProjectViews.Add(projectView);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(projectView);
        }

        // GET: ProjectViews/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProjectView projectView = db.ProjectViews.Find(id);
            if (projectView == null)
            {
                return HttpNotFound();
            }
            return View(projectView);
        }

        // POST: ProjectViews/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProjectID,ProjectName,ProjectDescription,CreationDate")] ProjectView projectView)
        {
            if (ModelState.IsValid)
            {
                db.Entry(projectView).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(projectView);
        }

        // GET: ProjectViews/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProjectView projectView = db.ProjectViews.Find(id);
            if (projectView == null)
            {
                return HttpNotFound();
            }
            return View(projectView);
        }

        // POST: ProjectViews/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProjectView projectView = db.ProjectViews.Find(id);
            db.ProjectViews.Remove(projectView);
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

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class UserCoursesController : Controller
    {
        private Training_CenterEntities6 db = new Training_CenterEntities6();

        // GET: UserCourses
        public ActionResult Index()
        {
            return View(db.UserCourse.ToList());
        }

        // GET: UserCourses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserCourse userCourse = db.UserCourse.Find(id);
            if (userCourse == null)
            {
                return HttpNotFound();
            }
            return View(userCourse);
        }

        // GET: UserCourses/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserCourses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserId,CourseID,CourseName,CourseImg,CourseDes,CoursePrice")] UserCourse userCourse)
        {
            if (ModelState.IsValid)
            {
                db.UserCourse.Add(userCourse);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(userCourse);
        }

        // GET: UserCourses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserCourse userCourse = db.UserCourse.Find(id);
            if (userCourse == null)
            {
                return HttpNotFound();
            }
            return View(userCourse);
        }

        // POST: UserCourses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserId,CourseID,CourseName,CourseImg,CourseDes,CoursePrice")] UserCourse userCourse)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userCourse).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(userCourse);
        }

        // GET: UserCourses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserCourse userCourse = db.UserCourse.Find(id);
            if (userCourse == null)
            {
                return HttpNotFound();
            }
            return View(userCourse);
        }

        // POST: UserCourses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserCourse userCourse = db.UserCourse.Find(id);
            db.UserCourse.Remove(userCourse);
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

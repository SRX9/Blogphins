using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using tlogNew.Models;

namespace tlogNew.Controllers
{
    public class MicroblogsController : Controller
    {
        private Model1 db = new Model1();

        // GET: Microblogs
        public ActionResult Index()
        {
            return View(db.microblog.ToList());
        }

        // GET: Microblogs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Microblog microblog = db.microblog.Find(id);
            if (microblog == null)
            {
                return HttpNotFound();
            }
            return View(microblog);
        }

        // GET: Microblogs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Microblogs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "bid,uid,uname,time,title,text,reads,tag")] Microblog microblog)
        {
            if (ModelState.IsValid)
            {
                db.microblog.Add(microblog);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(microblog);
        }

        // GET: Microblogs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Microblog microblog = db.microblog.Find(id);
            if (microblog == null)
            {
                return HttpNotFound();
            }
            return View(microblog);
        }

        // POST: Microblogs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "bid,uid,uname,time,title,text,reads,tag")] Microblog microblog)
        {
            if (ModelState.IsValid)
            {
                db.Entry(microblog).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(microblog);
        }

        // GET: Microblogs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Microblog microblog = db.microblog.Find(id);
            if (microblog == null)
            {
                return HttpNotFound();
            }
            return View(microblog);
        }

        // POST: Microblogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Microblog microblog = db.microblog.Find(id);
            db.microblog.Remove(microblog);
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

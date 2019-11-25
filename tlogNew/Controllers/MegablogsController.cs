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
    public class MegablogsController : Controller
    {
        private Model1 db = new Model1();

        // GET: Megablogs
        public ActionResult Index()
        {
            return View(db.megablog.ToList());
        }

        // GET: Megablogs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Megablog megablog = db.megablog.Find(id);
            if (megablog == null)
            {
                return HttpNotFound();
            }
            return View(megablog);
        }

        // GET: Megablogs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Megablogs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "bid,uid,uname,time,title,text,reads,category")] Megablog megablog)
        {
            if (ModelState.IsValid)
            {
                db.megablog.Add(megablog);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(megablog);
        }

        // GET: Megablogs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Megablog megablog = db.megablog.Find(id);
            if (megablog == null)
            {
                return HttpNotFound();
            }
            return View(megablog);
        }

        // POST: Megablogs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "bid,uid,uname,time,title,text,reads,category")] Megablog megablog)
        {
            if (ModelState.IsValid)
            {
                db.Entry(megablog).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(megablog);
        }

        // GET: Megablogs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Megablog megablog = db.megablog.Find(id);
            if (megablog == null)
            {
                return HttpNotFound();
            }
            return View(megablog);
        }

        // POST: Megablogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Megablog megablog = db.megablog.Find(id);
            db.megablog.Remove(megablog);
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

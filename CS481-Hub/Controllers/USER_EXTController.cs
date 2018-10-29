using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CS481_Hub.Models;

namespace CS481_Hub.Controllers
{
    public class USER_EXTController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: USER_EXT
        public ActionResult Index()
        {
            return View(db.User_ext.ToList());
        }

        // GET: USER_EXT/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            USER_EXT uSER_EXT = db.User_ext.Find(id);
            if (uSER_EXT == null)
            {
                return HttpNotFound();
            }
            return View(uSER_EXT);
        }

        // GET: USER_EXT/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: USER_EXT/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "USER_ID,FIRST_NM,LAST_NM,ZIPCODE,void_ind")] USER_EXT uSER_EXT)
        {
            if (ModelState.IsValid)
            {
                db.User_ext.Add(uSER_EXT);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(uSER_EXT);
        }
        
        // GET: USER_EXT/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            USER_EXT uSER_EXT = db.User_ext.Find(id);
            if (uSER_EXT == null)
            {
                return HttpNotFound();
            }
            return View(uSER_EXT);
        }

        // POST: USER_EXT/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "USER_ID,FIRST_NM,LAST_NM,ZIPCODE,void_ind")] USER_EXT uSER_EXT)
        {
            if (ModelState.IsValid)
            {
                db.Entry(uSER_EXT).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(uSER_EXT);
        }

        // GET: USER_EXT/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            USER_EXT uSER_EXT = db.User_ext.Find(id);
            if (uSER_EXT == null)
            {
                return HttpNotFound();
            }
            return View(uSER_EXT);
        }

        // POST: USER_EXT/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            USER_EXT uSER_EXT = db.User_ext.Find(id);
            db.User_ext.Remove(uSER_EXT);
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

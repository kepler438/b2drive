using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Area.Data;

namespace Area.Web.Controllers
{
    public class RentACarsController : Controller
    {
        private B2DriveForPostEntities db = new B2DriveForPostEntities();

        // GET: RentACars
        public ActionResult Index()
        {
            return View(db.RentACars.ToList());
        }

        // GET: RentACars/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RentACar rentACar = db.RentACars.Find(id);
            if (rentACar == null)
            {
                return HttpNotFound();
            }
            return View(rentACar);
        }

        // GET: RentACars/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RentACars/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Address,IsActive")] RentACar rentACar)
        {
            if (ModelState.IsValid)
            {
                db.RentACars.Add(rentACar);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(rentACar);
        }

        // GET: RentACars/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RentACar rentACar = db.RentACars.Find(id);
            if (rentACar == null)
            {
                return HttpNotFound();
            }
            return View(rentACar);
        }

        // POST: RentACars/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Address,IsActive")] RentACar rentACar)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rentACar).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(rentACar);
        }

        // GET: RentACars/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RentACar rentACar = db.RentACars.Find(id);
            if (rentACar == null)
            {
                return HttpNotFound();
            }
            return View(rentACar);
        }

        // POST: RentACars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RentACar rentACar = db.RentACars.Find(id);
            db.RentACars.Remove(rentACar);
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

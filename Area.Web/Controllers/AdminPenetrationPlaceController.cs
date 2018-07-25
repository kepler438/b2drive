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
    public class AdminPenetrationPlaceController : Controller
    {
        private B2DriveForPostEntities db = new B2DriveForPostEntities();

        // GET: AdminPenetrationPlace
        public ActionResult Index()
        {
            return View(db.PenetrationPlaces.ToList());
        }
         
        // GET: AdminPenetrationPlace/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AdminPenetrationPlace/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost] 
        public ActionResult Create(PenetrationPlace penetrationPlace)
        {
            if (ModelState.IsValid)
            {
                db.PenetrationPlaces.Add(penetrationPlace);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(penetrationPlace);
        }

        // GET: AdminPenetrationPlace/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PenetrationPlace penetrationPlace = db.PenetrationPlaces.Find(id);
            if (penetrationPlace == null)
            {
                return HttpNotFound();
            }
            return View(penetrationPlace);
        }

 
        [HttpPost] 
        public ActionResult Edit(PenetrationPlace penetrationPlace)
        {
            if (ModelState.IsValid)
            {
                db.Entry(penetrationPlace).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(penetrationPlace);
        }

        // GET: AdminPenetrationPlace/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PenetrationPlace penetrationPlace = db.PenetrationPlaces.Find(id);
            if (penetrationPlace == null)
            {
                return HttpNotFound();
            }
            return View(penetrationPlace);
        }

         
        public ActionResult DeleteConfirmed(int id)
        {
            PenetrationPlace penetrationPlace = db.PenetrationPlaces.Find(id);
            db.PenetrationPlaces.Remove(penetrationPlace);
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

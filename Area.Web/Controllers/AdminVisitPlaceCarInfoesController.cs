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
    public class AdminVisitPlaceCarInfoesController : Controller
    {
        private B2DriveForPostEntities db = new B2DriveForPostEntities();

        // GET: AdminVisitPlaceCarInfoes
        public ActionResult Index()
        {
            var visitPlaceCarInfoes = db.VisitPlaceCarInfoes.Include(v => v.RentACar).Include(v => v.VisitPlace);
            return View(visitPlaceCarInfoes.ToList());
        }

        // GET: AdminVisitPlaceCarInfoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VisitPlaceCarInfo visitPlaceCarInfo = db.VisitPlaceCarInfoes.Find(id);
            if (visitPlaceCarInfo == null)
            {
                return HttpNotFound();
            }
            return View(visitPlaceCarInfo);
        }

        // GET: AdminVisitPlaceCarInfoes/Create
        public ActionResult Create()
        {
            ViewBag.RentACarID = new SelectList(db.RentACars, "ID", "Name");
            ViewBag.VisitPlaceID = new SelectList(db.VisitPlaces, "ID", "ID");
            return View();
        }

        // POST: AdminVisitPlaceCarInfoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,VisitPlaceID,RentACarID,CarReceivePoint,CarDeliveryPoint,CarReceiveDate,CarDeliveryDate,CarBrandName,CarModelName,CheckinDate,CheckinLatitude,CheckinLongitude,CheckoutDate,CreateDate,IsActive")] VisitPlaceCarInfo visitPlaceCarInfo)
        {
            if (ModelState.IsValid)
            {
                db.VisitPlaceCarInfoes.Add(visitPlaceCarInfo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.RentACarID = new SelectList(db.RentACars, "ID", "Name", visitPlaceCarInfo.RentACarID);
            ViewBag.VisitPlaceID = new SelectList(db.VisitPlaces, "ID", "ID", visitPlaceCarInfo.VisitPlaceID);
            return View(visitPlaceCarInfo);
        }

        // GET: AdminVisitPlaceCarInfoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VisitPlaceCarInfo visitPlaceCarInfo = db.VisitPlaceCarInfoes.Find(id);
            if (visitPlaceCarInfo == null)
            {
                return HttpNotFound();
            }
            ViewBag.RentACarID = new SelectList(db.RentACars, "ID", "Name", visitPlaceCarInfo.RentACarID);
            ViewBag.VisitPlaceID = new SelectList(db.VisitPlaces, "ID", "ID", visitPlaceCarInfo.VisitPlaceID);
            return View(visitPlaceCarInfo);
        }

        // POST: AdminVisitPlaceCarInfoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,VisitPlaceID,RentACarID,CarReceivePoint,CarDeliveryPoint,CarReceiveDate,CarDeliveryDate,CarBrandName,CarModelName,CheckinDate,CheckinLatitude,CheckinLongitude,CheckoutDate,CreateDate,IsActive")] VisitPlaceCarInfo visitPlaceCarInfo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(visitPlaceCarInfo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RentACarID = new SelectList(db.RentACars, "ID", "Name", visitPlaceCarInfo.RentACarID);
            ViewBag.VisitPlaceID = new SelectList(db.VisitPlaces, "ID", "ID", visitPlaceCarInfo.VisitPlaceID);
            return View(visitPlaceCarInfo);
        }

        // GET: AdminVisitPlaceCarInfoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VisitPlaceCarInfo visitPlaceCarInfo = db.VisitPlaceCarInfoes.Find(id);
            if (visitPlaceCarInfo == null)
            {
                return HttpNotFound();
            }
            return View(visitPlaceCarInfo);
        }

        // POST: AdminVisitPlaceCarInfoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            VisitPlaceCarInfo visitPlaceCarInfo = db.VisitPlaceCarInfoes.Find(id);
            db.VisitPlaceCarInfoes.Remove(visitPlaceCarInfo);
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

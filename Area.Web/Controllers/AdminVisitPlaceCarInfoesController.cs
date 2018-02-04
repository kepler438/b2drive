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
         
         
        // GET: AdminVisitPlaceCarInfoes/Create
        public ActionResult Create(int id)
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

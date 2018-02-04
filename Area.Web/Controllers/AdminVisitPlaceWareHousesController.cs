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
    public class AdminVisitPlaceWareHousesController : Controller
    {
        private B2DriveForPostEntities db = new B2DriveForPostEntities();
         
        

        // GET: AdminVisitPlaceWareHouses/Create
        public ActionResult Create(int id)
        {
            ViewBag.VisitPlaceID = new SelectList(db.VisitPlaces, "ID", "ID");
            ViewBag.WareHouseID = new SelectList(db.WareHouses, "ID", "Name");
            return View();
        }

        // POST: AdminVisitPlaceWareHouses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,VisitPlaceID,WareHouseID,CheckinDate,CheckinLatitude,CheckinLongitude,CheckoutDate,CheckoutLatitude,CheckoutLongitude,CreateDate,IsActive")] VisitPlaceWareHouse visitPlaceWareHouse)
        {
            if (ModelState.IsValid)
            {
                db.VisitPlaceWareHouses.Add(visitPlaceWareHouse);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.VisitPlaceID = new SelectList(db.VisitPlaces, "ID", "ID", visitPlaceWareHouse.VisitPlaceID);
            ViewBag.WareHouseID = new SelectList(db.WareHouses, "ID", "Name", visitPlaceWareHouse.WareHouseID);
            return View(visitPlaceWareHouse);
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

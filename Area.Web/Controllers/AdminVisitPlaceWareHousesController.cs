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

        // GET: AdminVisitPlaceWareHouses
        public ActionResult Index()
        {
            var visitPlaceWareHouses = db.VisitPlaceWareHouses.Include(v => v.VisitPlace).Include(v => v.WareHouse);
            return View(visitPlaceWareHouses.ToList());
        }

        // GET: AdminVisitPlaceWareHouses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VisitPlaceWareHouse visitPlaceWareHouse = db.VisitPlaceWareHouses.Find(id);
            if (visitPlaceWareHouse == null)
            {
                return HttpNotFound();
            }
            return View(visitPlaceWareHouse);
        }

        // GET: AdminVisitPlaceWareHouses/Create
        public ActionResult Create()
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

        // GET: AdminVisitPlaceWareHouses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VisitPlaceWareHouse visitPlaceWareHouse = db.VisitPlaceWareHouses.Find(id);
            if (visitPlaceWareHouse == null)
            {
                return HttpNotFound();
            }
            ViewBag.VisitPlaceID = new SelectList(db.VisitPlaces, "ID", "ID", visitPlaceWareHouse.VisitPlaceID);
            ViewBag.WareHouseID = new SelectList(db.WareHouses, "ID", "Name", visitPlaceWareHouse.WareHouseID);
            return View(visitPlaceWareHouse);
        }

        // POST: AdminVisitPlaceWareHouses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,VisitPlaceID,WareHouseID,CheckinDate,CheckinLatitude,CheckinLongitude,CheckoutDate,CheckoutLatitude,CheckoutLongitude,CreateDate,IsActive")] VisitPlaceWareHouse visitPlaceWareHouse)
        {
            if (ModelState.IsValid)
            {
                db.Entry(visitPlaceWareHouse).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.VisitPlaceID = new SelectList(db.VisitPlaces, "ID", "ID", visitPlaceWareHouse.VisitPlaceID);
            ViewBag.WareHouseID = new SelectList(db.WareHouses, "ID", "Name", visitPlaceWareHouse.WareHouseID);
            return View(visitPlaceWareHouse);
        }

        // GET: AdminVisitPlaceWareHouses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VisitPlaceWareHouse visitPlaceWareHouse = db.VisitPlaceWareHouses.Find(id);
            if (visitPlaceWareHouse == null)
            {
                return HttpNotFound();
            }
            return View(visitPlaceWareHouse);
        }

        // POST: AdminVisitPlaceWareHouses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            VisitPlaceWareHouse visitPlaceWareHouse = db.VisitPlaceWareHouses.Find(id);
            db.VisitPlaceWareHouses.Remove(visitPlaceWareHouse);
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

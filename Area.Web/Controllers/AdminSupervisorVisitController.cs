using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Area.Data;
using Area.Data.Enums;

namespace Area.Web.Controllers
{
    public class AdminSupervisorVisitController : Controller
    {
        private B2DriveForPostEntities db = new B2DriveForPostEntities();

        // GET: AdminSupervisorVisit
        public ActionResult Index()
        {
            var visitPlaces = db.VisitPlaces.Include(v => v.Place).Include(v => v.PlaceCheckInfo).Include(v => v.Region).Include(v => v.User);
            List<VisitPlace> resultPlace = new List<VisitPlace>();
            foreach (var item in visitPlaces)
            {
                if (item.User.Permissions.FirstOrDefault().Id == (int)EnumUserType.Supervisor)
                {
                    resultPlace.Add(item);
                }
            }
            return View(resultPlace);
        }

        // GET: AdminSupervisorVisit/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VisitPlace visitPlace = db.VisitPlaces.Find(id);
            if (visitPlace == null)
            {
                return HttpNotFound();
            }
            return View(visitPlace);
        }

        // GET: AdminSupervisorVisit/Create
        public ActionResult Create()
        {
            ViewBag.PlaceID = new SelectList(db.Places, "ID", "Name");
            ViewBag.CheckInfoID = new SelectList(db.PlaceCheckInfoes, "ID", "CheckinLatitude");
            ViewBag.RegionID = new SelectList(db.Regions, "ID", "Name");
            ViewBag.UserID = new SelectList(db.Users, "ID", "FirstName");
            return View();
        }

        // POST: AdminSupervisorVisit/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,UserID,StartDate,EndDate,PlaceID,RegionID,CheckInfoID,IsApproved,CreateDate,IsActive")] VisitPlace visitPlace)
        {
            if (ModelState.IsValid)
            {
                db.VisitPlaces.Add(visitPlace);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PlaceID = new SelectList(db.Places, "ID", "Name", visitPlace.PlaceID);
            ViewBag.CheckInfoID = new SelectList(db.PlaceCheckInfoes, "ID", "CheckinLatitude", visitPlace.CheckInfoID);
            ViewBag.RegionID = new SelectList(db.Regions, "ID", "Name", visitPlace.RegionID);
            ViewBag.UserID = new SelectList(db.Users, "ID", "FirstName", visitPlace.UserID);
            return View(visitPlace);
        }

        // GET: AdminSupervisorVisit/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VisitPlace visitPlace = db.VisitPlaces.Find(id);
            if (visitPlace == null)
            {
                return HttpNotFound();
            }
            ViewBag.PlaceID = new SelectList(db.Places, "ID", "Name", visitPlace.PlaceID);
            ViewBag.CheckInfoID = new SelectList(db.PlaceCheckInfoes, "ID", "CheckinLatitude", visitPlace.CheckInfoID);
            ViewBag.RegionID = new SelectList(db.Regions, "ID", "Name", visitPlace.RegionID);
            ViewBag.UserID = new SelectList(db.Users, "ID", "FirstName", visitPlace.UserID);
            return View(visitPlace);
        }

        // POST: AdminSupervisorVisit/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,UserID,StartDate,EndDate,PlaceID,RegionID,CheckInfoID,IsApproved,CreateDate,IsActive")] VisitPlace visitPlace)
        {
            if (ModelState.IsValid)
            {
                db.Entry(visitPlace).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PlaceID = new SelectList(db.Places, "ID", "Name", visitPlace.PlaceID);
            ViewBag.CheckInfoID = new SelectList(db.PlaceCheckInfoes, "ID", "CheckinLatitude", visitPlace.CheckInfoID);
            ViewBag.RegionID = new SelectList(db.Regions, "ID", "Name", visitPlace.RegionID);
            ViewBag.UserID = new SelectList(db.Users, "ID", "FirstName", visitPlace.UserID);
            return View(visitPlace);
        }

        // GET: AdminSupervisorVisit/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VisitPlace visitPlace = db.VisitPlaces.Find(id);
            if (visitPlace == null)
            {
                return HttpNotFound();
            }
            return View(visitPlace);
        }

        // POST: AdminSupervisorVisit/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            VisitPlace visitPlace = db.VisitPlaces.Find(id);
            db.VisitPlaces.Remove(visitPlace);
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

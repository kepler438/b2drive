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
    public class AdminSupervisorVisitPlacesController : Controller
    {
        private B2DriveForPostEntities db = new B2DriveForPostEntities();

         
        // GET: AdminSupervisorVisitPlaces/Create
        public ActionResult Create(int id)
        {
            ViewBag.PlaceID = new SelectList(db.Places, "ID", "Name");
            ViewBag.PersonID = new SelectList(db.Users, "ID", "FirstName");
            ViewBag.VisitPlaceID = new SelectList(db.VisitPlaces, "ID", "ID");
            return View();
        }

        // POST: AdminSupervisorVisitPlaces/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,VisitPlaceID,ParentVisitPlaceID,PersonID,PlaceID,StartDate,EndDate,CheckinDate,CheckinLatitude,CheckinLongitude,PlacePositiveComment,PlaceNegativeComment,IsApproved,CreateDate,IsActive")] SupervisorVisitPlace supervisorVisitPlace)
        {
            if (ModelState.IsValid)
            {
                db.SupervisorVisitPlaces.Add(supervisorVisitPlace);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PlaceID = new SelectList(db.Places, "ID", "Name", supervisorVisitPlace.PlaceID);
            ViewBag.PersonID = new SelectList(db.Users, "ID", "FirstName", supervisorVisitPlace.PersonID);
            ViewBag.VisitPlaceID = new SelectList(db.VisitPlaces, "ID", "ID", supervisorVisitPlace.VisitPlaceID);
            return View(supervisorVisitPlace);
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

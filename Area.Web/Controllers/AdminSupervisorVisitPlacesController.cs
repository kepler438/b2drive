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

        public class ItemDrp
        {
            public int ID { get; set; }
            public string Name { get; set; }
        }

        // GET: AdminSupervisorVisitPlaces/Create
        public ActionResult Create(int id)
        {
            List<ItemDrp> visitList = new List<ItemDrp>();
            foreach (var item in db.VisitPlaces.Where(p=>p.StartDate > DateTime.Now && p.Place != null))
            {
                visitList.Add(new ItemDrp { Name = item.Place.Name + " - " + item.User.UserName + " - " + item.StartDate.ToString(), ID = item.ID });
            }

            ViewBag.PlaceID = new SelectList(db.Places, "ID", "Name");
            ViewBag.PersonID = new SelectList(db.Users, "ID", "UserName");
            ViewBag.ParentVisitPlaceID = new SelectList(visitList, "ID", "Name");

            ViewData["visitPlaceList"] = db.SupervisorVisitPlaces.Where(p => p.VisitPlaceID == id).Include(p=>p.VisitPlace1).ToList();
            return View();
        }

        [HttpPost]
        public ActionResult Create(SupervisorVisitPlace supervisorVisitPlace)
        {
            var place = db.VisitPlaces.Where(p => p.ID == supervisorVisitPlace.ParentVisitPlaceID).FirstOrDefault();
            supervisorVisitPlace.CreateDate = DateTime.Now;
            supervisorVisitPlace.IsApproved = false;
            supervisorVisitPlace.PlaceID = place.Place.ID;
            db.SupervisorVisitPlaces.Add(supervisorVisitPlace);
            db.SaveChanges();


            //SupervisorVisitPlace visitPlace = db.SupervisorVisitPlaces.Where(p => p.VisitPlaceID == supervisorVisitPlace.VisitPlaceID).FirstOrDefault(); 
            //if (ModelState.IsValid)
            //{
            //    if (visitPlace == null)
            //    {
            //        supervisorVisitPlace.CreateDate = DateTime.Now;
            //        supervisorVisitPlace.IsApproved = false;
            //        db.SupervisorVisitPlaces.Add(supervisorVisitPlace);
            //        db.SaveChanges();
            //    }
            //    else
            //    {
            //        visitPlace.ParentVisitPlaceID = supervisorVisitPlace.ParentVisitPlaceID;
            //        visitPlace.StartDate = supervisorVisitPlace.StartDate;
            //        visitPlace.EndDate = supervisorVisitPlace.EndDate;
            //        visitPlace.IsActive = supervisorVisitPlace.IsActive;
            //        visitPlace.IsApproved = false;

            //        db.Entry(visitPlace).State = EntityState.Modified; 
            //        db.SaveChanges();
            //    }


            //    return RedirectToAction("Index");
            //}
            //List<ItemDrp> visitList = new List<ItemDrp>();
            //foreach (var item in db.VisitPlaces.Where(p => p.StartDate > DateTime.Now && p.Place != null))
            //{
            //    visitList.Add(new ItemDrp { Name = item.Place.Name + " - " + item.User.UserName, ID = item.ID });
            //}
            //ViewBag.PlaceID = new SelectList(db.Places, "ID", "Name", supervisorVisitPlace.PlaceID);
            //ViewBag.PersonID = new SelectList(db.Users, "ID", "FirstName", supervisorVisitPlace.PersonID);
            //ViewBag.ParentVisitPlaceID = new SelectList(visitList, "ID", "Name");
            //return View(supervisorVisitPlace);
            return RedirectToAction("/Create/"+ supervisorVisitPlace.VisitPlaceID);
        }

        public ActionResult DeleteSpVisit(int id)
        {
            SupervisorVisitPlace visit = db.SupervisorVisitPlaces.Find(id);
            int visitPlaceID = visit.VisitPlaceID;
            db.SupervisorVisitPlaces.Remove(visit);
            db.SaveChanges();
            return RedirectToAction("/Create/" + visitPlaceID);
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

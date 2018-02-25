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
    public class UserAssignedVisitController : Controller
    {
        private B2DriveForPostEntities db = new B2DriveForPostEntities();

        // GET: UserAssignedVisit
        public ActionResult Index()
        {
            var visitPlaces = db.VisitPlaces.Include(v => v.Place).Include(v => v.PlaceCheckInfo).Include(v => v.Region).Include(v => v.User);
            List<VisitPlace> resultPlace = new List<VisitPlace>();
            foreach (var item in visitPlaces)
            {
                if (item.User.Permissions.FirstOrDefault().Id == (int)EnumUserType.Personnel)
                {
                    resultPlace.Add(item);
                }
            }
            return View(resultPlace.OrderByDescending(p=>p.StartDate));
        }

        // GET: UserAssignedVisit/Details/5
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

        // GET: UserAssignedVisit/Create
        public ActionResult Create()
        {
            ViewBag.PlaceID = new SelectList(db.Places, "ID", "Name");
            ViewBag.CheckInfoID = new SelectList(db.PlaceCheckInfoes, "ID", "CheckinLatitude");
            ViewBag.RegionID = new SelectList(db.Regions, "ID", "Name");
            ViewBag.UserID = new SelectList(GetTypedUserList(EnumUserType.Personnel), "ID", "UserName");
            return View();
        }
         
        [HttpPost] 
        public ActionResult Create(VisitPlace visitPlace)
        {
            if (ModelState.IsValid)
            {
                visitPlace.CreateDate = DateTime.Now;
                visitPlace.IsApproved = false;
                db.VisitPlaces.Add(visitPlace);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PlaceID = new SelectList(db.Places, "ID", "Name", visitPlace.PlaceID);
            ViewBag.CheckInfoID = new SelectList(db.PlaceCheckInfoes, "ID", "CheckinLatitude", visitPlace.CheckInfoID);
            ViewBag.RegionID = new SelectList(db.Regions, "ID", "Name", visitPlace.RegionID);
            ViewBag.UserID = new SelectList(GetTypedUserList(EnumUserType.Personnel), "ID", "FirstName", visitPlace.UserID);
            return View(visitPlace);
        }

        private List<User> GetTypedUserList(EnumUserType usertype)
        {
            List<User> result = new List<User>();
            var userList = db.Users;
            foreach (var item in userList)
            {
                if (item.Permissions.FirstOrDefault().Id == (int)usertype)
                {
                    result.Add(item);
                }
            }
            return result;
        }

        // GET: UserAssignedVisit/Edit/5
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
            ViewBag.UserID = new SelectList(GetTypedUserList(EnumUserType.Personnel), "ID", "FirstName", visitPlace.UserID);
            return View(visitPlace);
        }

        // POST: UserAssignedVisit/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost] 
        public ActionResult Edit(VisitPlace visitPlace)
        {
            if (ModelState.IsValid)
            {
                VisitPlace vst = db.VisitPlaces.Find(visitPlace.ID);
                vst.StartDate = visitPlace.StartDate;
                vst.EndDate = visitPlace.EndDate;
                vst.PlaceID = visitPlace.PlaceID; 
                vst.IsActive = visitPlace.IsActive;
                db.Entry(vst).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PlaceID = new SelectList(db.Places, "ID", "Name", visitPlace.PlaceID);
            ViewBag.CheckInfoID = new SelectList(db.PlaceCheckInfoes, "ID", "CheckinLatitude", visitPlace.CheckInfoID);
            ViewBag.RegionID = new SelectList(db.Regions, "ID", "Name", visitPlace.RegionID);
            ViewBag.UserID = new SelectList(GetTypedUserList(EnumUserType.Personnel), "ID", "FirstName", visitPlace.UserID);
            return View(visitPlace);
        }

        // GET: UserAssignedVisit/Delete/5
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

        // POST: UserAssignedVisit/Delete/5
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

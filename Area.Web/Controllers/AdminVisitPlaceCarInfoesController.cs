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
            var carinfo = db.VisitPlaceCarInfoes.Where(p => p.VisitPlaceID == id).FirstOrDefault();
            if (carinfo != null)
            {
                return View(carinfo);
            }
            else
            {
                return View();
            } 
        }

        // POST: AdminVisitPlaceCarInfoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create(VisitPlaceCarInfo visitPlaceCarInfo)
        {
            if (visitPlaceCarInfo.VisitPlaceID > 0)
            {
                var carinfo = db.VisitPlaceCarInfoes.Where(p => p.VisitPlaceID == visitPlaceCarInfo.VisitPlaceID).FirstOrDefault();
                if (carinfo != null)
                {
                    carinfo.RentACarID = visitPlaceCarInfo.RentACarID;
                    carinfo.CarBrandName = visitPlaceCarInfo.CarBrandName;
                    carinfo.CarDeliveryDate = visitPlaceCarInfo.CarDeliveryDate;
                    carinfo.CarDeliveryPoint = visitPlaceCarInfo.CarDeliveryPoint;
                    carinfo.CarModelName = visitPlaceCarInfo.CarModelName;
                    carinfo.CarReceiveDate = visitPlaceCarInfo.CarReceiveDate;
                    carinfo.CarReceivePoint = visitPlaceCarInfo.CarReceivePoint;
                    carinfo.PnrNo = visitPlaceCarInfo.PnrNo;
                    db.Entry(carinfo).State = EntityState.Modified;
                    db.SaveChanges();
                    return Redirect("/adminsupervisorvisit");
                }
                else
                {
                    visitPlaceCarInfo.ID = 0;
                    visitPlaceCarInfo.CreateDate = DateTime.Now;
                    db.VisitPlaceCarInfoes.Add(visitPlaceCarInfo);
                    db.SaveChanges();
                    return Redirect("/adminsupervisorvisit");
                }
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

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
            var warehouse = db.VisitPlaceWareHouses.Where(p => p.VisitPlaceID == id).Include(p=>p.WareHouse).FirstOrDefault();
            if (warehouse != null)
            {
                return View(warehouse);
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult Create(VisitPlaceWareHouse visitPlaceWareHouse)
        {
            if (visitPlaceWareHouse.VisitPlaceID > 0)
            {
                var warehouse = db.VisitPlaceWareHouses.Where(p => p.VisitPlaceID == visitPlaceWareHouse.VisitPlaceID).FirstOrDefault();
                if (warehouse != null)
                {
                    warehouse.WareHouseID = visitPlaceWareHouse.WareHouseID;
                    warehouse.IsActive = visitPlaceWareHouse.IsActive;
                    db.Entry(warehouse).State = EntityState.Modified;
                    db.SaveChanges();
                    return Redirect("/adminsupervisorvisit");
                }
                else
                {
                    visitPlaceWareHouse.ID = 0;
                    visitPlaceWareHouse.CreateDate = DateTime.Now;
                    db.VisitPlaceWareHouses.Add(visitPlaceWareHouse);
                    db.SaveChanges();
                    return Redirect("/adminsupervisorvisit");
                }
            } 
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

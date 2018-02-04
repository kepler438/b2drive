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
    public class AdminSPPlaceDeliveryProductsController : Controller
    {
        private B2DriveForPostEntities db = new B2DriveForPostEntities();

         
        // GET: AdminSPPlaceDeliveryProducts/Create
        public ActionResult Create(int id)
        {
            ViewBag.ProductID = new SelectList(db.Products, "ID", "Name");
            ViewBag.SupervisorVisitPlaceID = new SelectList(db.SupervisorVisitPlaces, "ID", "CheckinLatitude");
            return View();
        }

        // POST: AdminSPPlaceDeliveryProducts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,SupervisorVisitPlaceID,ProductID,Quantity,Note,CreateDate,IsActive")] SPPlaceDeliveryProduct sPPlaceDeliveryProduct)
        {
            if (ModelState.IsValid)
            {
                db.SPPlaceDeliveryProducts.Add(sPPlaceDeliveryProduct);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProductID = new SelectList(db.Products, "ID", "Name", sPPlaceDeliveryProduct.ProductID);
            ViewBag.SupervisorVisitPlaceID = new SelectList(db.SupervisorVisitPlaces, "ID", "CheckinLatitude", sPPlaceDeliveryProduct.SupervisorVisitPlaceID);
            return View(sPPlaceDeliveryProduct);
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

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

        // GET: AdminSPPlaceDeliveryProducts
        public ActionResult Index()
        {
            var sPPlaceDeliveryProducts = db.SPPlaceDeliveryProducts.Include(s => s.Product).Include(s => s.SupervisorVisitPlace);
            return View(sPPlaceDeliveryProducts.ToList());
        }

        // GET: AdminSPPlaceDeliveryProducts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SPPlaceDeliveryProduct sPPlaceDeliveryProduct = db.SPPlaceDeliveryProducts.Find(id);
            if (sPPlaceDeliveryProduct == null)
            {
                return HttpNotFound();
            }
            return View(sPPlaceDeliveryProduct);
        }

        // GET: AdminSPPlaceDeliveryProducts/Create
        public ActionResult Create()
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

        // GET: AdminSPPlaceDeliveryProducts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SPPlaceDeliveryProduct sPPlaceDeliveryProduct = db.SPPlaceDeliveryProducts.Find(id);
            if (sPPlaceDeliveryProduct == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductID = new SelectList(db.Products, "ID", "Name", sPPlaceDeliveryProduct.ProductID);
            ViewBag.SupervisorVisitPlaceID = new SelectList(db.SupervisorVisitPlaces, "ID", "CheckinLatitude", sPPlaceDeliveryProduct.SupervisorVisitPlaceID);
            return View(sPPlaceDeliveryProduct);
        }

        // POST: AdminSPPlaceDeliveryProducts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,SupervisorVisitPlaceID,ProductID,Quantity,Note,CreateDate,IsActive")] SPPlaceDeliveryProduct sPPlaceDeliveryProduct)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sPPlaceDeliveryProduct).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProductID = new SelectList(db.Products, "ID", "Name", sPPlaceDeliveryProduct.ProductID);
            ViewBag.SupervisorVisitPlaceID = new SelectList(db.SupervisorVisitPlaces, "ID", "CheckinLatitude", sPPlaceDeliveryProduct.SupervisorVisitPlaceID);
            return View(sPPlaceDeliveryProduct);
        }

        // GET: AdminSPPlaceDeliveryProducts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SPPlaceDeliveryProduct sPPlaceDeliveryProduct = db.SPPlaceDeliveryProducts.Find(id);
            if (sPPlaceDeliveryProduct == null)
            {
                return HttpNotFound();
            }
            return View(sPPlaceDeliveryProduct);
        }

        // POST: AdminSPPlaceDeliveryProducts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SPPlaceDeliveryProduct sPPlaceDeliveryProduct = db.SPPlaceDeliveryProducts.Find(id);
            db.SPPlaceDeliveryProducts.Remove(sPPlaceDeliveryProduct);
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

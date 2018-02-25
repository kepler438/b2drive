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
            var categoryList = db.ProductCategories.Where(p => p.IsActive == true).ToList();
            categoryList.Add(new ProductCategory()
            { ID = 0, Name = "Lütfen Bir Kategori Seçiniz." });
            ViewData["productCategory"] = new SelectList(categoryList.OrderBy(p => p.ID), "ID", "Name"); 
            var spvisitPlacelist = db.SupervisorVisitPlaces.Where(p => p.VisitPlaceID == id).Include(p => p.Place).ToList();
            List<Place> placeList = new List<Place>();
            foreach (var item in spvisitPlacelist)
            {
                placeList.Add(new Place()
                { ID = item.ID, Name = item.Place.Name });
            }
             
            List<SPPlaceDeliveryProduct> resultList = new List<SPPlaceDeliveryProduct>();
            foreach (var item in spvisitPlacelist)
            {
                var products = db.SPPlaceDeliveryProducts.Where(p => p.IsActive == true && p.SupervisorVisitPlaceID == item.ID).ToList();
                foreach (var pro in products)
                {
                    resultList.Add(pro);
                }
            }

            ViewBag.SupervisorVisitPlaceID = new SelectList(placeList, "ID", "Name");
            return View(resultList); 
        }
         
        [HttpPost] 
        public ActionResult Create(SPPlaceDeliveryProduct sPPlaceDeliveryProduct)
        {
            if (ModelState.IsValid)
            {
                sPPlaceDeliveryProduct.CreateDate = DateTime.Now;
                sPPlaceDeliveryProduct.IsActive = true;
                db.SPPlaceDeliveryProducts.Add(sPPlaceDeliveryProduct);
                db.SaveChanges(); 
            }
           var spvisit =  db.SupervisorVisitPlaces.Where(p => p.ID == sPPlaceDeliveryProduct.SupervisorVisitPlaceID).FirstOrDefault();
            //var categoryList = db.ProductCategories.Where(p => p.IsActive == true).ToList();
            //categoryList.Add(new ProductCategory()
            //{ ID = 0, Name = "Lütfen Bir Kategori Seçiniz." });
            //ViewData["productCategory"] = new SelectList(categoryList.OrderBy(p => p.ID), "ID", "Name");

            //List<SPPlaceDeliveryProduct> resultList = new List<SPPlaceDeliveryProduct>(); 
            //List<Place> placeList = new List<Place>();
            //foreach (var item in spvisitPlacelist)
            //{
            //    placeList.Add(new Place()
            //    { ID = item.ID, Name = item.Place.Name });
            //}
            //ViewBag.SupervisorVisitPlaceID = new SelectList(placeList, "ID", "Name");
            return Redirect("/AdminSPPlaceDeliveryProducts/Create/"+ spvisit.VisitPlaceID);
        }

        [Route("adminspplacedeliveryproducts/getsubcategory/{id}")]
        public JsonResult GetSubCategory(int id)
        {
            var subcategories = db.ProductSubCategories.Where(p => p.CategoryID == id).ToList();
            return Json(new SelectList(subcategories, "ID", "Name"));
        }

        [Route("adminspplacedeliveryproducts/getproduct/{id}")]
        public JsonResult GetProduct(int id)
        {
            var products = db.Products.Where(p => p.SubCategoryID == id).ToList();
            return Json(new SelectList(products, "ID", "Name"));
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

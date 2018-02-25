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
            var spplaceList = db.SPPlaceDeliveryProducts.Where(p => p.IsActive == true && p.SupervisorVisitPlaceID == id ).ToList();
            return View(spplaceList); 
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

            var categoryList = db.ProductCategories.Where(p => p.IsActive == true).ToList();
            categoryList.Add(new ProductCategory()
            { ID = 0, Name = "Lütfen Bir Kategori Seçiniz." });
            ViewData["productCategory"] = new SelectList(categoryList.OrderBy(p => p.ID), "ID", "Name");
            var spplaceList = db.SPPlaceDeliveryProducts.Where(p => p.IsActive == true && p.SupervisorVisitPlaceID == sPPlaceDeliveryProduct.SupervisorVisitPlaceID).ToList();
            return View(spplaceList);
        }

        [Route("adminspplacedeliveryproducts/GetSubCategories/{categoryid?}")]
        public JsonResult GetSubCategories(string categoryid)
        {
            int catId = string.IsNullOrEmpty(categoryid) ? 0 : Convert.ToInt32(categoryid);
            var list = db.ProductSubCategories.Where(x => x.CategoryID == catId).ToList();
            return Json(new SelectList(list, "ID", "Name"));
        }

        [Route("adminspplacedeliveryproducts/GetProducts/{subcategoryid?}")]
        public JsonResult GetProducts(string subcategoryid)
        {
            int subCatId = string.IsNullOrEmpty(subcategoryid) ? 0 : Convert.ToInt32(subcategoryid);
            var list = db.Products.Where(x => x.SubCategoryID == subCatId).ToList();
            return Json(new SelectList(list, "ID", "Name"));
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

using Area.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Area.Web.Controllers
{
    public class AdminPhotosController : Controller
    {
        // GET: AdminPhotos
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PersonelPhoto(int id)
        {
            using (B2DriveForPostEntities db = new B2DriveForPostEntities())
            { 
                var result = db.PlacePhotoes.Where(p => p.VisitPlaceID == id && p.IsActive == true).ToList();
                return View(result);
            }
        }

        public ActionResult SupervisorPhotoList()
        {
            return View();
        }
         

    }
}
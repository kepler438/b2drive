using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Area.Data;
using Area.Web.Attributes;

namespace Area.Web.Controllers
{
    public class AdminPlaceControlController : Controller
    {
        private B2DriveForPostEntities db = new B2DriveForPostEntities();
        [VerifyUser]
        public ActionResult Index()
        {
            var visitPlaces = db.VisitPlaces.Include(v => v.Place).Include(v => v.PlaceCheckInfo).Include(v => v.Region).Include(v => v.User);
            return View(visitPlaces.ToList()); 
        }


        public ActionResult ApprovedVisit(int id)
        {
            VisitPlace visitPlace = db.VisitPlaces.Find(id);
            visitPlace.IsApproved = true;
            db.Entry(visitPlace);
            db.SaveChanges();
            Session["AdminControl"] = false;
            Session["UserId"] = Session["FirstAdminId"];
            Session["FirstAdminId"] = null;
            return RedirectToAction("Index");
        }

        public ActionResult GotoPlace(int id)
        {
            VisitPlace visitPlace = db.VisitPlaces.Find(id);
            Session["AdminControl"] = true;
            Session["FirstAdminId"] = Convert.ToInt32(Session["UserId"]);
            Session["UserId"] = visitPlace.UserID;
            return Redirect("/UploadPhotos/" + id);
        }
        

    }
}
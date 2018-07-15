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
    public class RaitingQuestionsController : Controller
    {
        private B2DriveForPostEntities db = new B2DriveForPostEntities();

        // GET: RaitingQuestions
        public ActionResult Index()
        {
            return View(db.RaitingQuestions.ToList());
        }
         
        // GET: RaitingQuestions/Create
        public ActionResult Create()
        {
            return View();
        }
 
        [HttpPost] 
        public ActionResult Create(RaitingQuestion raitingQuestion)
        {
            if (ModelState.IsValid)
            {
                db.RaitingQuestions.Add(raitingQuestion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(raitingQuestion);
        }
         
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RaitingQuestion raitingQuestion = db.RaitingQuestions.Find(id);
            if (raitingQuestion == null)
            {
                return HttpNotFound();
            }
            return View(raitingQuestion);
        }
 
        [HttpPost] 
        public ActionResult Edit(RaitingQuestion raitingQuestion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(raitingQuestion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(raitingQuestion);
        }
           
        [HttpPost, ActionName("Delete")] 
        public ActionResult DeleteConfirmed(int id)
        {
            RaitingQuestion raitingQuestion = db.RaitingQuestions.Find(id);
            db.RaitingQuestions.Remove(raitingQuestion);
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

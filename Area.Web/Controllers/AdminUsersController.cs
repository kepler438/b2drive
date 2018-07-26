using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Area.Data;
using Area.Data.CustomEntity;
using Area.Web.Helper;

namespace Area.Web.Controllers
{
    public class AdminUsersController : Controller
    {
        private B2DriveForPostEntities db = new B2DriveForPostEntities();

        // GET: AdminUsers
        public ActionResult Index()
        {
            return View(db.Users.ToList().OrderByDescending(p=>p.ID));
        }
         
        // GET: AdminUsers/Create
        public ActionResult Create()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult Create(InputUser user)
        {
            if (ModelState.IsValid)
            {
                User saveUser = new User();
                Permission perm = db.Permissions.Where(x => x.Id ==user.permission).FirstOrDefault();
                if (perm != null)
                {
                    saveUser.Permissions.Add(perm);
                }
                saveUser.FirstName = user.FirstName;
                saveUser.LastName = user.LastName;
                saveUser.Phone = user.Phone;
                saveUser.MailAddress = user.MailAddress;
                saveUser.UserName = user.UserName;
                saveUser.IsActive = user.IsActive;
                saveUser.CreateDate = DateTime.Now;
                db.Users.Add(saveUser);
                db.SaveChanges(); 

                UserPassword newUserPassword = new UserPassword();
                var keyNew = LogHelper.GeneratePassword(10);
                var password = LogHelper.EncodePassword(user.Password, keyNew);
                newUserPassword.CreatedDate = DateTime.Now;
                newUserPassword.IsActive = true;
                newUserPassword.UserId = saveUser.ID;
                newUserPassword.Password = password;
                newUserPassword.Vcode = keyNew;
                db.UserPasswords.Add(newUserPassword);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(user);
        }

        // GET: AdminUsers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }
        
        [HttpPost]
        public ActionResult Edit(InputUser user)
        {
            if (ModelState.IsValid)
            { 
                User saveUser = new User();
                saveUser.ID = user.ID;
                saveUser.FirstName = user.FirstName;
                saveUser.LastName = user.LastName;
                saveUser.Phone = user.Phone;
                saveUser.MailAddress = user.MailAddress;
                saveUser.UserName = user.UserName; 
                saveUser.CreateDate = DateTime.Now;
                saveUser.IsActive = user.IsActive;
                db.Entry(saveUser).State = EntityState.Modified;
                db.SaveChanges();

                if (!String.IsNullOrEmpty(user.Password))
                {
                    if (db.UserPasswords.Where(p => p.UserId == user.ID).FirstOrDefault() != null)
                    {
                        db.UserPasswords.Remove(db.UserPasswords.Where(p => p.UserId == user.ID).FirstOrDefault());
                    } 
                    UserPassword newUserPassword = new UserPassword();
                    var keyNew = LogHelper.GeneratePassword(10);
                    var password = LogHelper.EncodePassword(user.Password, keyNew);
                    newUserPassword.CreatedDate = DateTime.Now;
                    newUserPassword.IsActive = true;
                    newUserPassword.UserId = saveUser.ID;
                    newUserPassword.Password = password;
                    newUserPassword.Vcode = keyNew;
                    db.UserPasswords.Add(newUserPassword);
                    db.SaveChanges();
                } 
                return RedirectToAction("Index");
            }
            return View(user);
        }
        
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            user.IsActive = false;
            db.Entry(user).State = EntityState.Modified;
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

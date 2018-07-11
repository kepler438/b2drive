﻿using Area.Data;
using Area.Web.Attributes;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Area.Web.Controllers
{
    [VerifyUser]
    public class AdminController : Controller
    {
        /* esad */
        public ActionResult Index()
        {
            return View();
        }
    }
}
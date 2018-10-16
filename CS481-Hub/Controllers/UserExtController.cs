﻿using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using CS481_Hub.Models;

namespace CS481_Hub.Controllers
{
    public class UserExtController : Controller
    {
        private ApplicationDbContext db;

        public UserExtController()
        {
            //Initialize our database connection. NOTE: ApplicationDbContext can be edited in Models/IndentityModels.cs/ApplicationDbContext.
            //may want to look into the best practices to go about the database connection but for now it works fine.
            
            db = new ApplicationDbContext();
        }
        // Returns the view for the page "UserExt/UserExtCreate.cshtml"
        public ActionResult UserExtCreate()
        {
            return View();
        }
        // POST:
        //This method gets passed the UserExt model from the html page and adds it into the database. 
        //We may need to add some field validation later.
        [HttpPost]
        public ActionResult PostExtInfo (USER_EXT userExtModel)
        {
            userExtModel.USER_ID = User.Identity.GetUserId();
            userExtModel.void_ind = "n";
            db.User_ext.Add(userExtModel);
            db.SaveChanges();
            return RedirectToAction("Index", "Home");
            
        }

    }
}
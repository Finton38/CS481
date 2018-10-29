using System;
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
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Net;
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
            if (Request.IsAuthenticated)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        // POST:
        //This method gets passed the UserExt model from the html page and adds it into the database. 
        //We may need to add some field validation later.
        //this method is an "async" method, this allows for the thread to be freed up until this is ready to happen
        //may not be very necessary for this but its probably good practice to do so.
        [HttpPost]
        public async Task<ActionResult> PostExtInfo (USER_EXT userExtModel)
        {
            if (Request.IsAuthenticated)
            {
                
                userExtModel.USER_ID = User.Identity.GetUserId();
                userExtModel.void_ind = "n";
                db.User_ext.Add(userExtModel);
                await db.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
            
        }


        public ActionResult UserExtEdit()
        {
            if (Request.IsAuthenticated)
            {
                String currentUserId = User.Identity.GetUserId();
                var userInfo = db.User_ext.SingleOrDefault(v => v.USER_ID == currentUserId);

                if (userInfo == null)
                {
                    return HttpNotFound();
                }
                else
                {
                    return View(userInfo);
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        //need to return to this and make async****
        [HttpPost]
        public ActionResult UpdateUserInfo(USER_EXT userInfo)
        {
            if (Request.IsAuthenticated)
            {
                var previousInfo = db.User_ext.SingleOrDefault(v => v.USER_ID == userInfo.USER_ID);
                if(previousInfo == null)
                {
                    return HttpNotFound();
                }
                else
                {
                    previousInfo.FIRST_NM = userInfo.FIRST_NM;
                    previousInfo.LAST_NM = userInfo.LAST_NM;
                    previousInfo.ZIPCODE = userInfo.ZIPCODE;
                    previousInfo.USER_ID = userInfo.USER_ID;

                    db.SaveChanges();

                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
    
    }
}
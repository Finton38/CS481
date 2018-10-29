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


        // GET: USER_EXT/Edit/5
        //*************We Need current user ID to pass through this************

        public ActionResult Edit(string id)
        {
            id = User.Identity.GetUserId();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            USER_EXT uSER_EXT = db.User_ext.Find(id);
            if (uSER_EXT == null)
            {
                return HttpNotFound();
            }
            return View(uSER_EXT);
        }

        // POST: USER_EXT/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "USER_ID,FIRST_NM,LAST_NM,ZIPCODE,void_ind")] USER_EXT uSER_EXT)
        {
            if (ModelState.IsValid)
            {
                db.Entry(uSER_EXT).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(uSER_EXT);
        }

        // GET: USER_EXT/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            USER_EXT uSER_EXT = db.User_ext.Find(id);
            if (uSER_EXT == null)
            {
                return HttpNotFound();
            }
            return View(uSER_EXT);
        }



    }
}
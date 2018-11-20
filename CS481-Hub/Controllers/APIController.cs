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
    public class APIController : Controller
    {
        private ApplicationDbContext db;

        public APIController()
        {
            db = new ApplicationDbContext();
        }

        // GET: API
        //get the all APIS for the current user in USER_API_XREF where void_ind = n
        public ActionResult Index()
        {
            string userId = User.Identity.GetUserId();
            var usersAPIs = db.USER_APIs.Where(a => a.USER_ID == userId && a.void_ind == "n").ToList();
            if (Request.IsAuthenticated)
            {
                return View(usersAPIs);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        //turn on the selected API by setting void ind to N
        public ActionResult TurnOn(int id)
        {
            var apiEntry = db.USER_APIs.SingleOrDefault(a => a.xref_id == id);
            if (Request.IsAuthenticated)
            {
                if (apiEntry == null)
                {
                    return HttpNotFound();
                }
                else
                {
                    apiEntry.void_ind = "n";
                    db.SaveChanges();
                    return RedirectToAction("Index", "API");
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }


        }

        //turn off the selected API by setting void ind to Y
        public ActionResult TurnOff(int id)
        {
            var apiEntry = db.USER_APIs.SingleOrDefault(a => a.xref_id == id);
            if (Request.IsAuthenticated)
            {
                if (apiEntry == null)
                {
                    return HttpNotFound();
                }
                else
                {
                    apiEntry.void_ind = "y";
                    db.SaveChanges();
                    return RedirectToAction("Index", "API");
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }


        }




    }
}
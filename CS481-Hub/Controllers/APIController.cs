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
        //If we select where void is n it will remove the api completly from the users api selection page.
        public ActionResult Index()
        {
            ViewData["apiList"] = getActiveAPIs();
            string userId = User.Identity.GetUserId();
            var usersAPIs = db.USER_APIs.Where(a => a.USER_ID == userId /*&& a.void_ind == "n"*/).ToList();
            var AllAPIs = db.Available_APIs;

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
        public List<String> getActiveAPIs()
        {
            List<String> activeList = new List<String>();
            var userId = User.Identity.GetUserId();
            var active = db.USER_APIs.Where(u => u.USER_ID == userId && u.void_ind == "n").ToList();

            foreach (var api in active)
            {
                var APIName = db.Available_APIs.Where(a => a.API_ID == api.API_ID).SingleOrDefault();
                activeList.Add(APIName.API_Name);
            }

            return activeList;
        }



    }
}
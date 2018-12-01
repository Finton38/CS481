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

namespace CS481_Hub.Controllers
{
    public class AdminController : Controller
    {
        private ApplicationDbContext db;

        public AdminController()
        {
            db = new ApplicationDbContext();
        }

        // Show the Admin Screen
        public ActionResult Index()
        {
            return View();
        }

        //Show the Admin update screen and pull in all APIs that have not been "deleted"
        public ActionResult Update()
        {
            if (Request.IsAuthenticated)
            {
                return View(db.Available_APIs.ToList().Where(a => a.void_ind == "n"));
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }



        //Method to add in a new API through the Admin Screen
        [HttpPost]
        public async Task<ActionResult> CreateNewAPI(Available_API api)
        {
            if (Request.IsAuthenticated)
            {
                api.void_ind = "n";
                db.Available_APIs.Add(api);
                await db.SaveChangesAsync();
                await AddNewAPIToUsers(api.API_Name);
                return RedirectToAction("Index", "Admin");
            }else
            {
                return RedirectToAction("Index", "Home");
            }
        }


        //"Delete" selected API from being used
        
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var API = db.Available_APIs.SingleOrDefault(a => a.API_ID == id);
            if (Request.IsAuthenticated)
            {
                if (API == null)
                {
                    return HttpNotFound();
                }
                else
                {
                    API.void_ind = "y";
                    await DeleteAPIFromUsers(id);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        //When an Admin adds a new API the API must be added to all existing users.
        [HttpPost]
        public async Task<ActionResult> AddNewAPIToUsers(String apiName)
        {
            var allUsers = db.Users.ToList();
            USER_API_XREF userApi = new USER_API_XREF();
            var newAPI = db.Available_APIs.SingleOrDefault(v => v.API_Name == apiName);
            int apiID = newAPI.API_ID;

            foreach (var user in allUsers)
            {
                userApi.API_ID = apiID;
                userApi.USER_ID = user.Id;
                userApi.void_ind = "n";
                db.USER_APIs.Add(userApi);
                await db.SaveChangesAsync();
            }
            return null;
        }

        //When an admin "deletes" an API it is deleted from all current user accounts
        [HttpDelete]
        public async Task<ActionResult> DeleteAPIFromUsers(int id)
        {
            var allUserAPI = db.USER_APIs.ToList().Where(a => a.API_ID == id);

            foreach(var entry in allUserAPI)
            {
                entry.void_ind = "y";
                await db.SaveChangesAsync();
            }
            return null;
        }

        //Returns view
        public ActionResult AdminHome()
        {
            if (Request.IsAuthenticated)
            {
                return View();
            }
            else
            {
                return RedirectToAction("AdminHome", "Admin");
            }
        }
    }
}
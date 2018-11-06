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

        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Update()
        {
            if (Request.IsAuthenticated)
            {
                return View(db.Available_APIs.ToList());
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }



        [HttpPost]
        public async Task<ActionResult> CreateNewAPI(Available_API api)
        {
            if (Request.IsAuthenticated)
            {
                api.void_ind = "n";
                db.Available_APIs.Add(api);
                await db.SaveChangesAsync();
                AddNewAPIToUsers(api.API_Name);
                return RedirectToAction("Index", "Home");
            }else
            {
                return RedirectToAction("Index", "Home");
            }
        }


        public ActionResult AddNewAPIToUsers(String apiName)
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
                db.SaveChanges();
            }
            return null;
        }
    }
}
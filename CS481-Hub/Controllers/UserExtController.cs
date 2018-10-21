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
        //test comment
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

    }
}
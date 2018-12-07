using CS481_Hub.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CS481_Hub.Models;
using Microsoft.AspNet.Identity;

namespace CS481_Hub.Controllers
{
    public class HomeController : Controller
    {

        private ApplicationDbContext db;

        public HomeController()
        {
            db = new ApplicationDbContext();
        }

        public ActionResult Index()
        {
            return View();
        }

        //Returns view
        public ActionResult Weather()
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
        //GetWeather method for the veiw that reutrns the users zipcode
        public JsonResult GetWeather()
        {

            var userId = User.Identity.GetUserId();
            USER_EXT ext = db.User_ext.SingleOrDefault(u => u.USER_ID == userId);
            WeatherAPI weather = new WeatherAPI();
            //return json of getweatherforcast method
            //AllowGet
            return Json(weather.getWeatherForcast(ext.ZIPCODE), JsonRequestBehavior.AllowGet);
        }
        
        public ActionResult News()
        {
            List<NewsAPI> newsList = new List<NewsAPI>();
            int totalResults = new NewsAPI(0).returnTotalResults();

            for(int i = 0; i < totalResults-1; i++)
            {
                newsList.Add(new NewsAPI(i));
            }
            ViewData["newsList"] = newsList;
            return View();
        }

        //Returns view
        public ActionResult RandomFact()
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

        //Returns view
        public ActionResult Translation()
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
    }
}
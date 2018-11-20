using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CS481_Hub.Models;
using Microsoft.AspNet.Identity;

namespace CS481_Hub.Controllers
{
    public class BlogController : Controller
    {


        private ApplicationDbContext db;

        public BlogController()
        {
            db = new ApplicationDbContext();
        }

        // GET: Blog
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Create()
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
        [HttpPost]
        public ActionResult PostBlog(BLOGS blog)
        {
            if (Request.IsAuthenticated)
            {
                DateTime now = DateTime.Now;
                blog.USER_ID = User.Identity.GetUserId();
                blog.VOID_IND = "n";           
                blog.CREATE_DT = now;
                db.Blog.Add(blog);
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            else
            {
               return RedirectToAction("Index", "Home");
            }

        }
    }
}
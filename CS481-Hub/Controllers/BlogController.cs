using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        //public ActionResult Index()
        //{
        //    return View();
        //}

        //Pull up the create page to make a new blog
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
        //Function to post the new blog
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
        //Get all blogs for the current user
        [HttpGet]
        public ActionResult ListView()
        {
            ViewData["apiList"] = getActiveAPIs();
            if (Request.IsAuthenticated)
            {
                String userID = User.Identity.GetUserId();
                var blogList = db.Blog.ToList().Where(b => b.USER_ID == userID);
                var blogList2 = db.Blog.ToList();
                return View(blogList2);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        

        //Function to "delete" a blog
        [HttpPost]
        public async Task<ActionResult> Delete(int id)
        {
            var blogToDelete = db.Blog.SingleOrDefault(b => b.BLOG_ID == id);
            String userID = User.Identity.GetUserId();
            if (Request.IsAuthenticated && blogToDelete.USER_ID == userID)
            {
                if (blogToDelete == null)
                {
                    return HttpNotFound();
                }
                else
                {
                    blogToDelete.VOID_IND = "y";
                    await db.SaveChangesAsync();
                    return RedirectToAction("ListView", "Blog");
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        //Create page to edit specific blog
        [HttpGet]
        public ActionResult Update(int id)
        {
            var blogToEdit = db.Blog.SingleOrDefault(b => b.BLOG_ID == id);
            String userID = User.Identity.GetUserId();
            if (Request.IsAuthenticated && blogToEdit.USER_ID == userID)
            {
                return View(blogToEdit);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        //Function to update the blog
        [HttpPost]
        public async Task<ActionResult> doEdit(BLOGS blog)
        {
            int blogID = blog.BLOG_ID;
            DateTime now = DateTime.Now;
            var previousBlog = db.Blog.SingleOrDefault(b => b.BLOG_ID == blogID);
            if (Request.IsAuthenticated)
            {
                previousBlog.BLOG_TEXT = blog.BLOG_TEXT;
                previousBlog.TITLE = blog.TITLE;
                previousBlog.UPDATE_DT = now;
                previousBlog.CREATE_DT = blog.CREATE_DT;
                previousBlog.VOID_IND = "n";
                await db.SaveChangesAsync();
                return RedirectToAction("ListView", "Blog");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        //Get Current blog
        [HttpGet]
        public ActionResult ShowBlog(int id)
        {
            var blogToShow = db.Blog.SingleOrDefault(b => b.BLOG_ID == id);
            String userID = User.Identity.GetUserId();
            if (Request.IsAuthenticated)
            {
                return View(blogToShow);
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHangOnline.Models;
using WebBanHangOnline.Models.EF;

namespace WebBanHangOnline.Controllers
{
    public class BlogController : Controller
    {
        public ApplicationDbContext db;

        public BlogController()
        {
            db = new ApplicationDbContext();
        }
        // GET: Blog
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Lester_Blog()
        {
            List<Post> item = db.Posts.Where(x=>x.IsActive).ToList();
            return PartialView(item);
           
        }
        public ActionResult Delai_blog(int id)
        {
            var item = db.Posts.Find(id);
            return View(item);
        }
        public ActionResult New_hot()
        {
            var item = db.Posts.ToList();
            return PartialView(item);
        }
                
    }
}
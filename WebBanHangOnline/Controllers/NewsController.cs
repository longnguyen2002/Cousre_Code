using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHangOnline.Models;
using WebBanHangOnline.Models.EF;

namespace WebBanHangOnline.Controllers
{
    public class NewsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: News
        public ActionResult Index()
        {
           var items = db.News.Where(x=>x.IsActive).ToList();
            return View(items);
        }
        public ActionResult Menu_right()
        {
            var item = db.Categories.ToList();
            return PartialView("Menu_right", item);
        }
        public ActionResult New_hot()
        {
            var item = db.News.Where(x => x.IsActive).ToList();
            return PartialView(item);
        }
        public ActionResult Detai_New(int id)
        {
            var item = db.News.Find(id);
            return View(item);
        }
    }
}
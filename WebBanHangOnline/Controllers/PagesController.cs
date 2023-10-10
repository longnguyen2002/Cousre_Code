using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHangOnline.Models;

namespace WebBanHangOnline.Controllers
{
    public class PagesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Pages
        public ActionResult Index()
        {
            var item = db.News.Where(x=>x.IsNew).ToList();
            return View(item);
        }
      
    }
}
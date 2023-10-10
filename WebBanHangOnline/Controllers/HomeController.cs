using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHangOnline.Models;

namespace WebBanHangOnline.Controllers
{
   
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: HomeMaster
        public ActionResult Index()
        {
          
            return View();
        }
        public ActionResult Displayleft()
        {
            var item = db.Advs.Where(x => x.DisplayHomeLeft).ToList();
            return PartialView(item);
        }
        public ActionResult Displayright()
        {
            var items= db.Advs.Where(x=>x.DisplayHomeRight).ToList();
            return PartialView(items);
        }
    }
}
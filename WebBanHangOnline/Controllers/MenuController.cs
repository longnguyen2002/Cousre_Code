using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHangOnline.Models;

namespace WebBanHangOnline.Controllers
{
    public class MenuController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Menu
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult MenuTop()
        {
            var item = db.Categories.OrderBy(x => x.Position).ToList();
            return PartialView("_MenuTop",item);
        }

        public ActionResult MenuProductCategory()
        {
            var item = db.productscategories.ToList();
            return PartialView("_MenuProductCategory", item);
        }
        public ActionResult MenuLeft()
        {
            var item = db.productscategories.ToList();
            return PartialView("_MenuLeft",item);
        }
        public ActionResult Menuarrivals()
        {
            var item = db.productscategories.ToList();
           
            return PartialView("_Menuarrivals", item);
        }
        
       
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using WebBanHangOnline.Migrations;
using WebBanHangOnline.Models;
using WebBanHangOnline.Models.EF;

namespace WebBanHangOnline.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin,Employee")]
    public class SubscribeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Admin/Subscribe
        public ActionResult Index()
        {
            var item = db.subscribes.OrderByDescending(x => x.id).ToList();
            return View(item);
        }
        public ActionResult showContent()
        {
            var items = db.subscribes.ToList();
            return PartialView(items);
        }
    }
}
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHangOnline.Models;
using WebBanHangOnline.Models.EF;

namespace WebBanHangOnline.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin,Employee")]
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Admin/Home
        public ActionResult Index()
        {
            return View();
        }
        // partiview đơn hàng
        public ActionResult ShoppingCart()
        {
            var item = db.Orders.OrderByDescending(x => x.id).ToList();
            return PartialView(item);
        }
        
        public ActionResult List()
        {
           var item = db.Products.Where(x=>x.IsHome).ToList();
            return PartialView(item);
        }
        public ActionResult View_Online()
        {
            var item = new ThongKeModel();
            ViewBag.visitors_online = HttpContext.Application["visitors_online"];
            return PartialView(item);
        }
        public ActionResult View_Online1()
        {
            var item = new ThongKeModel();
            var ts = HttpContext.Application["TatCa"];
            item.TatCa = HttpContext.Application["TatCa"].ToString();
            return PartialView(item);
        }
        public ActionResult View_Onlinehomnay()
        {
            var item = new ThongKeModel();
            var hn = HttpContext.Application["HomNay"];
            item.HomNay = HttpContext.Application["HomNay"].ToString();
            var hq = HttpContext.Application["HomQua"];
            item.HomQua = HttpContext.Application["HomQua"].ToString();
            return PartialView(item);
        }
        
        public ActionResult View_User()
        {
            var item = new ThongKeModel();
            var ts = HttpContext.Application["TatCa"];
            item.TatCa = HttpContext.Application["TatCa"].ToString();
            return PartialView(item);
        }
        
        
        public ActionResult Thong_ke()
        {
            var item = new ThongKeModel();
            ViewBag.visitors_online = HttpContext.Application["visitors_online"];
            var hn = HttpContext.Application["HomNay"];
            item.HomNay = HttpContext.Application["HomNay"].ToString();
            var hq = HttpContext.Application["HomQua"];
            item.HomQua = HttpContext.Application["HomQua"].ToString();
            var tn = HttpContext.Application["TuanTruoc"];
            item.TuanNay = HttpContext.Application["TuanNay"].ToString();
            var tt = HttpContext.Application["TuanTruoc"];
            item.TuanTruoc = HttpContext.Application["TuanTruoc"].ToString();
            var thn = HttpContext.Application["ThangNay"];
            item.ThangNay = HttpContext.Application["ThangNay"].ToString();
            var tht = HttpContext.Application["ThangTruoc"];
            item.ThangTruoc = HttpContext.Application["ThangTruoc"].ToString();
            var ts = HttpContext.Application["TatCa"];
            item.TatCa = HttpContext.Application["TatCa"].ToString();
            return PartialView(item);
        }
        public ActionResult Refresh()
        {
            var item = new ThongKeModel();
            ViewBag.visitors_online = HttpContext.Application["visitors_online"];
            var hn = HttpContext.Application["HomNay"];
            item.HomNay = HttpContext.Application["HomNay"].ToString();
            var hq = HttpContext.Application["HomQua"];
            item.HomQua = HttpContext.Application["HomQua"].ToString();
            var tn = HttpContext.Application["TuanTruoc"];
            item.TuanNay = HttpContext.Application["TuanNay"].ToString();
            var tt = HttpContext.Application["TuanTruoc"];
            item.TuanTruoc = HttpContext.Application["TuanTruoc"].ToString();
            var thn = HttpContext.Application["ThangNay"];
            item.ThangNay = HttpContext.Application["ThangNay"].ToString();
            var tht = HttpContext.Application["ThangTruoc"];
            item.ThangTruoc = HttpContext.Application["ThangTruoc"].ToString();
            var ts = HttpContext.Application["TatCa"];
            item.TatCa = HttpContext.Application["TatCa"].ToString();
            return PartialView(item);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;   
using System.Web.Mvc;
using WebBanHangOnline.Models;

namespace WebBanHangOnline.Controllers
{
    public class ThongkeController : Controller
    {
        // GET: Thongke
        public ActionResult Index()
        {
            return View();
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
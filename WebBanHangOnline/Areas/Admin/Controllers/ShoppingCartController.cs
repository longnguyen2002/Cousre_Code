using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using Rotativa;
using System.Web.Mvc;
using System.Web.UI;
using System.Xml;
using WebBanHangOnline.Models;
using WebBanHangOnline.Models.EF;

namespace WebBanHangOnline.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin,Employee")]
    public class ShoppingCartController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Admin/ShoppingCart
        public ActionResult Index(string SearchText, int? page)
        {
            var pageSize = 6;
            if (page == null)
            {
                page = 1;
            }
            IEnumerable<Order> items = db.Orders.OrderByDescending(x=>x.CreatedDate).ToList();
            if (!string.IsNullOrEmpty(SearchText))
            {
                items = items.Where(x => x.CustomerName.Contains(SearchText));
            }
            var pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            items = items.ToPagedList(pageIndex, pageSize);

            return View(items);
        }
        public ActionResult View_Cart()
        {
            var item = db.Orders.Where(x=>x.IsHome).OrderByDescending(x=>x.id).ToList();
            return View(item);
        }
        [HttpPost]
        public ActionResult UpdateTT(int id , int trangthai)
        {
            var item = db.Orders.Find(id);
            if (item != null)
            {
                db.Orders.Attach(item);
                item.TypePayment = trangthai;
                db.Entry(item).Property(x=>x.TypePayment).IsModified = true;
                db.SaveChanges();
                return Json(new {message="Thành công",Success = true});
            }
            return Json(new { message = "Thất bại!!", Success = false });
        }
        //export PDF
        public ActionResult ExportPDF()
        {
            return new ActionAsPdf("View_Cart")
            {
                FileName =Server.MapPath("~/Content/PDF/ListCart.pdf")
            };
        }
       
        //Chi tiết giỏ hàng
        public ActionResult ViewDetail(int id)
        {
            var item = db.Orders.Find(id);
            return View(item);
        }
        //partiview danh sách sản phẩm chi tiết giỏ hàng
        public ActionResult Partial_Sanpham(int id)
        {
            var item = db.OrderDetails.Where(x => x.OrderId == id).ToList();
            return PartialView(item);
        }
        public ActionResult Delete(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }  
            Order order = db.Orders.Find(id);
            db.Orders.Remove(order);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed (int id)
        {
            Order order = db.Orders.Find(id);
            db.Orders.Remove(order);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult IsHome (int id)
        {
            var item = db.Orders.Find(id);
            if(item!=null)
            {
                item.IsHome = !item.IsHome;
                db.Entry(item).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return Json(new {success = true , ishome = item.IsHome});   
            }
            return Json(new { success = false });
        }
        [HttpPost]
        public ActionResult DeleteAll(string ids)
        {
            if(!string.IsNullOrEmpty(ids))
            {
                var items = ids.Split(',');
                if(items!=null && items.Any())
                {
                    foreach(var item in items)
                    {
                        var item2 = db.Orders.Find(Convert.ToInt32(item));
                        db.Orders.Remove(item2);
                        db.SaveChanges();
                    }
                }
                return Json(new { success = true});
            }
            return Json(new {success= false});
        }
       
    }
}
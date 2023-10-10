using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebBanHangOnline.Models;
using WebBanHangOnline.Models.EF;

namespace WebBanHangOnline.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin,Employee")]
    public class ImagesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Admin/Images
        //Display information Images
        public ActionResult Index(int? page)
        {
            var pageSize = 5;
            if (page == null)
            {
                page = 1;
            }
            IEnumerable<Adv> item = db.Advs.OrderByDescending(x => x.id);
            var pageIndex = page.HasValue ? Convert.ToInt32(page): 1;
            item = item.ToPagedList(pageIndex, pageSize);
            return View(item);
        }
        //chức năng Create infomation
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(Adv model)
        {
            if(ModelState.IsValid)
            {
                model.CreatedDate = DateTime.Now;
                model.ModifiedrDate = DateTime.Now;
                db.Advs.Add(model);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }
        //chức năng Sửa
        public ActionResult Edit(int id)
        {
            var item = db.Advs.Find(id);
            return View(item);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Adv model)
        {
            if (ModelState.IsValid)
            {
                model.ModifiedrDate = DateTime.Now;
                db.Advs.Attach(model);
                db.Entry(model).State=System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }
        //chức năng delete one information in table
        public ActionResult Delete(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Adv adv = db.Advs.Find(id);
            db.Advs.Remove(adv);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Adv adv = db.Advs.Find(id);
            db.Advs.Remove(adv);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult DisplayLeft(int id)
        {
            var item = db.Advs.Find(id);
            if(item !=null)
            {
                item.DisplayHomeLeft = !item.DisplayHomeLeft;
                db.Entry(item).State= System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return Json(new {Success = true,displayleft = item.DisplayHomeLeft});
            }
            return Json(new { Success = false });
        }
        [HttpPost]
        public ActionResult DisplayRight(int id)
        {
            var item = db.Advs.Find(id);
            if (item != null)
            {
                item.DisplayHomeRight = !item.DisplayHomeRight;
                db.Entry(item).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return Json(new { Success = true, displayright = item.DisplayHomeRight });
            }
            return Json(new { Success = false });
        }   
        [HttpPost]
        public ActionResult DeleleAll(string ids)
        {
            if (!string.IsNullOrEmpty(ids))
            {
                var item =ids.Split(',');
                if(item !=null && item.Any())
                {
                    foreach(var items in item)
                    {
                        var obj = db.Advs.Find(Convert.ToInt32(items));
                        db.Advs.Remove(obj);
                        db.SaveChanges();
                    }
                } 
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }
    }
}
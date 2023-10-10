using Microsoft.Owin.Security.DataHandler.Encoder;
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
    public class NewsController : Controller
    {
        private ApplicationDbContext _dbConect = new ApplicationDbContext();
        // GET: Admin/Newsn
        public ActionResult Index(string SearchText,int? page)
        {
            var pageSize = 6;
            if (page == null)
            {
                page = 1;
            }
            IEnumerable<New> items = _dbConect.News.OrderBy(x => x.id);
            if(!string.IsNullOrEmpty(SearchText))
            {
                items=items.Where(x=>x.Title.Contains(SearchText));
            }
            var pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            items = items.ToPagedList(pageIndex,pageSize);
         
            return View(items);
        }
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(New model)
        {
            if(ModelState.IsValid)
            {
                model.CreatedDate = DateTime.Now;
                model.CategoryId = 10;
                model.ModifiedrDate = DateTime.Now;
               _dbConect.News.Add(model);
                _dbConect.SaveChanges();
                 return RedirectToAction("Index");
            }
            return View(model);
        }
        public ActionResult Edit(int id)
        {
            var item = _dbConect.News.Find(id);
            return View(item);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(New model)
        {
            if (ModelState.IsValid)
            {
               
                model.ModifiedrDate = DateTime.Now;
                _dbConect.News.Attach(model);
                _dbConect.Entry(model).State=System.Data.Entity.EntityState.Modified;
                _dbConect.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }
        //Chức Năng xóa
        //[HttpPost]
        //public ActionResult Delete(int id)
        //{
        //    var item = _dbConect.News.Find(id);
        //    if(item != null)
        //    {
        //        _dbConect.News.Remove(item);
        //        _dbConect.SaveChanges();
        //        return Json(new { success = true });
        //    }    
        //    return Json(new {success=false} );
        //}
        //Chức Năng xóa
        public ActionResult Delete(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            New news = _dbConect.News.Find(id);
            _dbConect.News.Remove(news);
            _dbConect.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed (int id)
        {
            New news = _dbConect.News.Find(id);
            _dbConect.News.Remove(news);
            _dbConect.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult IsNew(int id)
        {
            var item = _dbConect.News.Find(id);
            if (item != null)
            {
                item.IsNew = !item.IsNew;
                _dbConect.Entry(item).State = System.Data.Entity.EntityState.Modified;
                _dbConect.SaveChanges();
                return Json(new { Success = true, isNew = item.IsNew });
            }
            return Json(new { Success = false });
        }
        [HttpPost]
        public ActionResult IsActive (int id)
        {
            var item = _dbConect.News.Find(id);
            if (item != null)
            {
                item.IsActive=!item.IsActive;
                _dbConect.Entry(item).State = System.Data.Entity.EntityState.Modified;
                _dbConect.SaveChanges();
                return Json(new { success = true ,isActive = item.IsActive });
            }
            return Json(new { success = false });
        }
        [HttpPost]
        public ActionResult DeleteAll(string ids)
        {
            if (!string.IsNullOrEmpty(ids))
            {
                var items=ids.Split(',');
                if(items !=null && items.Any())
                {
                    foreach(var item in items)
                    {
                        var obj = _dbConect.News.Find(Convert.ToInt32(item));
                        _dbConect.News.Remove(obj);
                        _dbConect.SaveChanges();
                    }
                }
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }
    }
}
using Microsoft.Ajax.Utilities;
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
    public class CategoryController : Controller
    {
       private ApplicationDbContext _dbConect = new ApplicationDbContext();
        // GET: Admin/Category
        public ActionResult Index(string SearchText, int? page)
        { 
            var pageSize = 6;
            if(page == null)
            {
                page = 1;
            }
            IEnumerable<Category> items = _dbConect.Categories.OrderBy(x => x.id);
            if (!string.IsNullOrEmpty(SearchText))
            {
                items = items.Where(x=>x.Title.Contains(SearchText));
            }
            var pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            items =items.ToPagedList(pageIndex, pageSize);
            return View(items);
        }
        public ActionResult Add()
        {
              
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(Category model)
        {
            if (ModelState.IsValid)
            {
                model.CreatedDate = DateTime.Now;
                model.ModifiedrDate = DateTime.Now;
                _dbConect.Categories.Add(model);
                _dbConect.SaveChanges();
                return RedirectToAction("Index");
                
            }
            return View(model);
        }
        public ActionResult Edit(int id)
        {
            var item = _dbConect.Categories.Find(id);
            return View(item); 
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Category model)
        {
            if (ModelState.IsValid)
            {
                _dbConect.Categories.Attach(model);
                model.ModifiedrDate = DateTime.Now;
                _dbConect.Entry(model).Property(x => x.Title).IsModified = true;
                _dbConect.Entry(model).Property(x => x.Description).IsModified = true;
                _dbConect.Entry(model).Property(x => x.SeoDescription).IsModified = true;
                _dbConect.Entry(model).Property(x => x.Alias).IsModified = true;
                _dbConect.Entry(model).Property(x => x.SeoTitle).IsModified = true;
                _dbConect.Entry(model).Property(x => x.SeoKeywords).IsModified = true;
                _dbConect.Entry(model).Property(x => x.Position).IsModified = true;
                _dbConect.Entry(model).Property(x => x.ModifiedrDate).IsModified = true;
                _dbConect.Entry(model).Property(x => x.ModifierBy).IsModified = true;
                
                _dbConect.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }
        //[HttpPost]
        //public ActionResult Delete(int id)
        //{
        //    var item = _dbConect.Categories.Find(id);
        //    if(item != null)
        //    {
        //        //var DeleteItem = _dbConect.Categories.Attach(item);
        //        _dbConect.Categories.Remove(item);  
        //        _dbConect.SaveChanges();
        //        return Json(new {success=true});
        //    }
        //    return Json(new { success = false });
        //}
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = _dbConect.Categories.Find(id);
            _dbConect.Categories.Remove(category);
            _dbConect.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Category category = _dbConect.Categories.Find(id);
            _dbConect.Categories.Remove(category);
            _dbConect.SaveChanges();
            return RedirectToAction("Index");
        }
        //phương thức xóa
        [HttpPost]
        public ActionResult DeleteAll(string ids)
        {
            if(!string.IsNullOrEmpty(ids))
            {
                var item = ids.Split(',');
                if(item !=null && item.Any())
                {
                    foreach(var items in item)
                    {
                        var obj = _dbConect.Categories.Find(Convert.ToInt32(items));
                        _dbConect.Categories.Remove(obj);
                        _dbConect.SaveChanges();
                    }
                }
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }
    }
}
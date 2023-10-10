using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations.Model;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebBanHangOnline.Models;
using WebBanHangOnline.Models.EF;

namespace WebBanHangOnline.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin,Employee")]
    public class PostController : Controller
    {
        private ApplicationDbContext _dbConect = new ApplicationDbContext();
        // GET: Admin/Post
        public ActionResult Index(string SearchText,int? page)
        {
            var pageSize = 6;
            if(page == null)
            {
                page = 1;
            }
            IEnumerable<Post> items = _dbConect.Posts.OrderByDescending(x => x.id);
            if(!string.IsNullOrEmpty(SearchText))
            {
                items = items.Where(x => x.Title.Contains(SearchText));
            }    
            var pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            items=items.ToPagedList(pageIndex, pageSize);
            return View(items);
        }
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(Post model)
        {
            if (ModelState.IsValid)
            {
                model.CreatedDate = DateTime.Now;
                model.CategoryId = 10;
                model.ModifiedrDate = DateTime.Now;
                _dbConect.Posts.Add(model);
                _dbConect.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }
        public ActionResult Edit(int id)
        {
            var item = _dbConect.Posts.Find(id);
            return View(item);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Post model)
        {
            if (ModelState.IsValid)
            {

                model.ModifiedrDate = DateTime.Now;
                _dbConect.Posts.Attach(model);
                _dbConect.Entry(model).State = System.Data.Entity.EntityState.Modified;
                _dbConect.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }
        
        //Chức Năng xóa
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post posts = _dbConect.Posts.Find(id);
            _dbConect.Posts.Remove(posts);
            _dbConect.SaveChanges();
            return RedirectToAction("Index");
        } 
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Post posts = _dbConect.Posts.Find(id);
            _dbConect.Posts.Remove(posts);
            _dbConect.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult IsActive(int id)
        {
            var item = _dbConect.Posts.Find(id);
            if (item != null)
            {
                item.IsActive = !item.IsActive;
                _dbConect.Entry(item).State = System.Data.Entity.EntityState.Modified;
                _dbConect.SaveChanges();
                return Json(new { success = true, isActive = item.IsActive });
            }
            return Json(new { success = false });
        }
        [HttpPost]
        public ActionResult DeleteAll(string ids)
        {
            if (!string.IsNullOrEmpty(ids))
            {
                var items = ids.Split(',');
                if (items != null && items.Any())
                {
                    foreach (var item in items)
                    {
                        var obj = _dbConect.Posts.Find(Convert.ToInt32(item));
                        _dbConect.Posts.Remove(obj);
                        _dbConect.SaveChanges();
                    }
                }
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }
    }
}
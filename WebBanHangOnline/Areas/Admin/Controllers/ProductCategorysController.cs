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
    public class ProductCategorysController : Controller
    {
        private ApplicationDbContext _dbConect = new ApplicationDbContext();
        // GET: Admin/ProductCategorys
        public ActionResult Index(string SearchText, int? page)
        {
            var pageSize = 6;
            if (page == null)
            {
                page = 1;
            }
            IEnumerable<productscategory> items = _dbConect.productscategories.OrderBy(x => x.id);
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
        public ActionResult Add(productscategory model)
        {
            if (ModelState.IsValid)
            {
                model.CreatedDate = DateTime.Now;
                model.ModifiedrDate = DateTime.Now;
                _dbConect.productscategories.Add(model);
                _dbConect.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }
        public ActionResult Edit(int id)
        {
            var item = _dbConect.productscategories.Find(id);
            return View(item);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(productscategory model)
        {
            if(ModelState.IsValid)
            {
                
                _dbConect.productscategories.Attach(model);
                model.ModifiedrDate = DateTime.Now;
                _dbConect.Entry(model).Property(x => x.Title).IsModified = true;
                _dbConect.Entry(model).Property(x => x.Description).IsModified = true;
                _dbConect.Entry(model).Property(x => x.Icon).IsModified = true;
                _dbConect.Entry(model).Property(x=>x.Alias).IsModified = true;
                _dbConect.Entry(model).Property(x => x.SeoTitle).IsModified = true;
                _dbConect.Entry(model).Property(x => x.SeoDescription).IsModified = true;
                _dbConect.Entry(model).Property(x => x.SeoKeywords).IsModified = true;
                _dbConect.Entry(model).Property(x => x.ModifiedrDate).IsModified = true;
                _dbConect.Entry(model).Property(x => x.ModifierBy).IsModified = true;
                _dbConect.SaveChanges();
                return RedirectToAction("Index");
            }    
            return View(model);
        }
        public ActionResult Delete(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            productscategory productscategory = _dbConect.productscategories.Find(id);
            _dbConect.productscategories.Remove(productscategory);
            _dbConect.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult DeleteConfirmed(int id)
        {
            productscategory productscategory = _dbConect.productscategories.Find(id);
            _dbConect.productscategories.Remove(productscategory);
            _dbConect.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
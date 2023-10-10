using PagedList;
using Rotativa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Mvc;
using WebBanHangOnline.Models;
using WebBanHangOnline.Models.EF;

namespace WebBanHangOnline.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin,Employee")]   
    public class ProductsController : Controller
    {
        private ApplicationDbContext _dbConect = new ApplicationDbContext();
       
        // GET: Admin/Products
        public ActionResult Index(string SearchText,int? page)
        {
           
            var pageSize = 6;
            if(page == null)
            {
                page = 1;
            }
            IEnumerable<Product> product = _dbConect.Products.OrderBy(x => x.id);
            if(!string.IsNullOrEmpty(SearchText))
            {
                product = product.Where(x=>x.Title.Contains(SearchText));
            }    
            var pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            product=product.ToPagedList(pageIndex, pageSize);
            return View(product);
        }
        //export PDF
        public ActionResult ExportPDF()
        {
            return new ActionAsPdf("Index")
            {
                FileName = Server.MapPath("~/Content/PDF/DanhSach.pdf")
            };
        }
        public ActionResult Add() 
        {
            ViewBag.ProductCategory = new SelectList(_dbConect.productscategories.ToList(),"id","Title");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken] 
        public ActionResult Add(Product model)
        {
            if(ModelState.IsValid)
            {   
                
                model.CreatedDate = DateTime.Now;
                model.ModifiedrDate = DateTime.Now;

                _dbConect.Products.Add(model);
                _dbConect.SaveChanges();
                return RedirectToAction("Index");
              
            }
            ViewBag.ProductCategory = new SelectList(_dbConect.productscategories.ToList(), "id", "Title");
            return View(model);
        }
        public ActionResult Edit(int id)
        {
            var item = _dbConect.Products.Find(id);
            ViewBag.ProductCategory = new SelectList(_dbConect.productscategories.ToList(),"id","Title");
            return View(item);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product model)
        {
            if (ModelState.IsValid)
            {
                model.ModifiedrDate = DateTime.Now;
                _dbConect.Products.Attach(model);
                _dbConect.Entry(model).Property(x => x.ProductCategoryId).IsModified = true;
                _dbConect.Entry(model).State = System.Data.Entity.EntityState.Modified;
                _dbConect.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }
        //chức năng xóa sản phẩm
        public ActionResult Delete(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = _dbConect.Products.Find(id);
            _dbConect.Products.Remove(product);
            _dbConect.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = _dbConect.Products.Find(id);
            _dbConect.Products.Remove(product);
            _dbConect.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult IsActive(int id)
        {
            var item = _dbConect.Products.Find(id);
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
        public ActionResult IsHome(int id)
        {
            var item = _dbConect.Products.Find(id);
            if (item != null)
            {
                item.IsHome = !item.IsHome;
                _dbConect.Entry(item).State = System.Data.Entity.EntityState.Modified;
                _dbConect.SaveChanges();
                return Json(new { success = true, isHome = item.IsHome });
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
                        var obj = _dbConect.Products.Find(Convert.ToInt32(item));
                        _dbConect.Products.Remove(obj);
                        _dbConect.SaveChanges();
                    }
                }
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }

    }
}
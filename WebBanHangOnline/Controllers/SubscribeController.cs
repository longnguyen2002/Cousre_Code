using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebBanHangOnline.Models;
using WebBanHangOnline.Models.EF;

namespace WebBanHangOnline.Controllers
{
    public class SubscribeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Subscribe
       
        public ActionResult Index()
        {
            var item = db.subscribes.ToList();
            return PartialView("Index",item);
        }
       
        public ActionResult Add()
        {
            return PartialView();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(Subscribe model)
        {
            if (ModelState.IsValid)
            {
                model.CreatedDate = DateTime.Now;
                db.subscribes.Add(model);
                db.SaveChanges();
                return RedirectToAction("HomeList");
            }
            return PartialView("Add",model);
        }
        public ActionResult HomeList(string SearchText, int? page)
        {
            var pageSize = 12;
            if (page == null)
            {
                page = 1;
            }

            IEnumerable<Product> items = db.Products.OrderBy(x => x.id);
            if (!string.IsNullOrEmpty(SearchText))
            {
                items = items.Where(x => x.Title.Contains(SearchText));
            }
            var pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            items = items.ToPagedList(pageIndex, pageSize);
            return View(items);
        }
        public ActionResult Delete(int? id)
        {
             if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subscribe sub = db.subscribes.Find(id);
            db.subscribes.Remove(sub);
            db.SaveChanges();
            return RedirectToAction("HomeList");
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Subscribe sub = db.subscribes.Find(id);
            db.subscribes.Remove(sub);
            db.SaveChanges();
            return RedirectToAction("HomeList");
        }
    }
}
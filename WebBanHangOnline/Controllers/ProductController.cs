using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebBanHangOnline.Models;
using WebBanHangOnline.Models.EF;

namespace WebBanHangOnline.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        
        public ApplicationDbContext db; 
      
        public ProductController()
        {
            db = new ApplicationDbContext();
        }
        public ActionResult HomeList(string SearchText,int? page)
        {
            var pageSize = 12;
            if (page == null)
            {
                page = 1;
            }
            
            IEnumerable<Product> items = db.Products.OrderBy(x => x.id);
            if (!string.IsNullOrEmpty(SearchText))
            {
                items = items.Where(x=>x.Title.Contains(SearchText));
            }
            var pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            items = items.ToPagedList(pageIndex,pageSize);
            return View(items);
        }
        //public ActionResult ProductCategory(int? id)
        //{
        //    var item = db.Products.ToList();
        //    if (id > 0)
        //    {
        //        item = item.Where(x => x.ProductCategoryId == id).ToList();
        //    }
        //    return View(item);
        //}

        public ActionResult Index()
        {
            List<Product> item = db.Products.Where(x => x.IsHome).ToList();
            return PartialView(item);
        }
        //sản phẩm nổi bật
       
        public ActionResult BetSell()
        {
            List<Product> items = db.Products.ToList();
            return PartialView(items);
        }
        public ActionResult ProductDetail(int id)
        {
            var sp = db.Products.FirstOrDefault(x=>x.id == id);
            if(sp !=null)
            { 
                db.Products.Attach(sp);
                sp.ViewCount = sp.ViewCount + 1;
                db.Entry(sp).Property(x=>x.ViewCount).IsModified=true; 
                db.SaveChanges();
            }
            return View(sp);
        }
       
    }
}
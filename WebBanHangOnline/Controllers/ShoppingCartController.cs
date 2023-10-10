using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations.Sql;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHangOnline.Models;
using WebBanHangOnline.Models.EF;

namespace WebBanHangOnline.Controllers
{

    public class ShoppingCartController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: ShoppingCart
        //GIỏ Hàng
        public ActionResult Index()
        {
            ShoppingCart cart = (ShoppingCart)Session["Cart"];
            if (cart != null && cart.Items.Any())
            {
                ViewBag.CheckCart = cart;
                
            }
            return View();
        }
        //thanh toán
        public ActionResult Checkout()
        {
            ShoppingCart cart = (ShoppingCart)Session["Cart"];
            if (cart != null && cart.Items.Any())
            {
                ViewBag.CheckCart= cart;
            }
            return View();
        }
        //thanh toán thành công
        public ActionResult CheckoutSuccess()
        {
           
            return View();
        }
        //thông tin khách hàng
        public ActionResult Partial_Checkout()
        {
            return PartialView();
        }
        //submit đặt hàng, thanh toán, nút thanh toán
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Checkout(OrderViewModel req)
        {
            var code = new { Success = false, Code = -1 };
            if(ModelState .IsValid)
            {
                ShoppingCart cart = (ShoppingCart)Session["Cart"];
                if(cart != null)
                { 
                    Order order = new Order();
                    order.CustomerName = req.CustomerName;
                    order.Phone = req.Phone;
                    order.Address = req.Address;
                    
                    cart.Items.ForEach(x => order.OrderDetails.Add(new OrderDetail
                    {
                        ProductId = x.productId,
                        Quantity = x.Quantity,
                        Price = x.Price,
                    }));
                    order.TotalAmount = cart.Items.Sum(x => (x.Price * x.Quantity));
                    order.TypePayment = req.TypePayment;
                    order.CreatedDate = DateTime.Now;
                    order.CreatedBy = req.Phone;
                    order.ModifiedrDate = DateTime.Now;
                    Random rd = new Random();
                    order.Code = "DH" + rd.Next(0, 9) + rd.Next(0, 9) + rd.Next(0, 9) + rd.Next(0, 9);
                    db.Orders.Add(order);
                    db.SaveChanges();
                    cart.ClearCart();
                    return RedirectToAction("CheckoutSuccess");
                }
            }
            return Json(code);
        }
        //Hiển thị số lượng mặt hàng mua count
        public ActionResult ShowCount()
        {
            ShoppingCart cart = (ShoppingCart)Session["Cart"];
            if(cart !=null)
            {
                return Json(new { Counts = cart.Items.Count },JsonRequestBehavior.AllowGet);
            }    
            return Json(new { Counts = 0 },JsonRequestBehavior.AllowGet); 
        }

        //phương thức thêm sản phẩm vào giỏ hàng
        [HttpPost]
        public ActionResult AddToCart(int id,int quantity)
        {
            var code = new { Success = false, tbm = " ", code = -1,Counts = 0 };
            var db = new ApplicationDbContext();
            var checkProduct = db.Products.FirstOrDefault(x => x.id == id);
            if(checkProduct != null)
            {
                ShoppingCart cart = (ShoppingCart)Session["Cart"];
                if(cart == null)
                {
                    cart = new ShoppingCart(); 
                }
                ShoppingCartItem items = new ShoppingCartItem
                {
                    productId = checkProduct.id,
                    ProductImage = checkProduct.Image,
                    Alias = checkProduct.Alias,
                    productName = checkProduct.Title,
                    Quantity = quantity
                };
                items.Price = checkProduct.Price;
                if (checkProduct.PriceSale > 0)
                {
                    items.Price = (decimal)checkProduct.Price;
                    
                }
                items.TotalPrice = items.Quantity * items.Price;
                cart.AddToCart(items, quantity);
                Session["Cart"] = cart;
                code = new { Success = true, tbm = "Thêm sản phẩm vào giỏ hàng thành công!!!", code = 1, Counts = cart.Items.Count };
            }
            return Json(code);
        }
        //Cập nhập sản phẩm ở giỏ hàng
        [HttpPost] 
        public ActionResult Update(int id, int quantity)
        {
            ShoppingCart cart = (ShoppingCart)Session["Cart"];
            if (cart != null)
            {
                cart.UpdateQuantity(id,quantity);
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }

        //sản phẩm ở trang thông tin khách hàng
        public ActionResult Partial_Item_ThanhToan()
        {
            ShoppingCart cart = (ShoppingCart)Session["Cart"];
            if (cart != null && cart.Items.Any())
            {
                return PartialView(cart.Items);
            }
            return PartialView();
        }

        //Show sản phẩm ở trang giỏ hàng
        public ActionResult Partial_Item_Cart()
        {
            ShoppingCart cart = (ShoppingCart)Session["Cart"];
            if (cart != null && cart.Items.Any())
            {
                return PartialView(cart.Items);
            }
            return PartialView();
        }
        //xóa sản phẩm
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var code = new { Success = false, tb = " ", code = -1, Counts = 0 };
            ShoppingCart cart = (ShoppingCart)Session["Cart"];
            if(cart != null)
            {
                var checkProduct = cart.Items.FirstOrDefault(x => x.productId == id);
                if(checkProduct != null)
                {
                    cart.Remove(id);
                    code = new { Success = true, tb = " ", code = -1, Counts = cart.Items.Count };
                }
            }    
            return Json(code); 
        }
        //xóa toàn bộ sản phẩm
        [HttpPost]
        public ActionResult DeleteAll()
        {
            ShoppingCart cart = (ShoppingCart)Session["Cart"];
            if (cart != null)
            {
                cart.ClearCart();
                return Json(new {success=true});
            }
            return Json(new {success=false});
        }
    }
}
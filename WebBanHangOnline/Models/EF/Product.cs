using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebBanHangOnline.Models.EF
{
    [Table("tb_Product")]
    public class Product : CommonAbstract 
    {   
        public Product() { 
           
            this.OrderDetails = new HashSet<OrderDetail>();
        }
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        
        //các thuộc tính
        public int id { get; set; }
        [Required(ErrorMessage = " Bạn không được bỏ trống tên sản phẩm!!")]
        [StringLength(250)]
        public string Title { get; set; }
        public string Alias { get; set; }
        public string ProductCode { get; set; }
        public string Description { get; set; }
        [AllowHtml]
        public string Detail { get; set; }
        public string Image { get; set; }
        public decimal OriginalPrice { get; set; }
        public decimal Price { get; set; }
        public decimal PriceSale { get; set; }
        public int Quantity { get; set; }
        public int ViewCount { get; set; }
        public bool IsHome { get; set; }
        //các trạng thái
        public bool IsSale { get; set; }
        public bool IsActive { get; set; }
        public bool IsFeature { get; set; }
        public bool IsHot { get; set; }
        //khóa ngoại của bảng ProductCategory
        [Required(ErrorMessage = " Bạn hãy chọn danh mục sản phẩm tương ứng với mỗi sản phẩm!!")]
        public int ProductCategoryId { get; set; }
        public string SeoTile { get; set; }
        public string SeoDescription { get; set; }
        public string SeoKeywords { get; set; }

        public virtual productscategory Productscategory { get; set; }
       
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
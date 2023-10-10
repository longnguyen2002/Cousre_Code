using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebBanHangOnline.Models.EF
{
    [Table ("tb_Subscribe")]
    public class Subscribe
    {
        [Key]
       [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
       
        public string Email { get; set; }
        
        public string Name { get; set; }
       
        public string Detail { get; set; }  
        public DateTime CreatedDate { get; set; }

        
    }
}
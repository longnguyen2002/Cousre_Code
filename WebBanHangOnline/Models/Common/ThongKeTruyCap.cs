using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WebBanHangOnline.Models.EF;

namespace WebBanHangOnline.Models.Common
{
    public class ThongKeTruyCap
    {
        public static string strconnect = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();
        public static ThongKeViewModel ThongKe()
        {
            using (var connect = new SqlConnection(strconnect))
            {
                var item = connect.QueryFirstOrDefault<ThongKeViewModel>("sp_thongke", commandType: CommandType.StoredProcedure);
                return item;
            }
        }
       
    }

}
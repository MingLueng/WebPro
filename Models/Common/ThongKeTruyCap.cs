using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using System.Web;
using System.Data.SqlClient;
using System.Data;

namespace WebBanHangOnline.Models.Common
{
    public class ThongKeTruyCap
    {
        private static string strConnect = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();

        public static ThongKeViewModel ThongKe()
        {
            using(var connect = new SqlConnection(strConnect))
            {
                var item = connect.QueryFirstOrDefault<ThongKeViewModel>("sp_ThongKe",commandType:CommandType.StoredProcedure);
                return item;
            }
        }
    }
}
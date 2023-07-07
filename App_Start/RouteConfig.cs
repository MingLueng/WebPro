using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebBanHangOnline
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                name: "HomePage",
                url: "trang-chu",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "WebBanHangOnline.Controllers" }
            );
            routes.MapRoute(
              name: "Product",
              url: "san-pham",
              defaults: new { controller = "Products", action = "Index", alias = UrlParameter.Optional },
              namespaces: new[] { "WebBanHangOnline.Controllers" }
          );
            routes.MapRoute(
              name: "Contact",
              url: "lien-lac",
              defaults: new { controller = "Contact", action = "Index", alias = UrlParameter.Optional },
              namespaces: new[] { "WebBanHangOnline.Controllers" }
          );
            routes.MapRoute(
             name: "GioHang",
             url: "gio-hang",
             defaults: new { controller = "ShoppingCart", action = "Index", alias = UrlParameter.Optional },
             namespaces: new[] { "WebBanHangOnline.Controllers" }
         );
            routes.MapRoute(
                name: "ThanhToan",
                url: "thanh-toan",
                defaults: new { controller = "ShoppingCart", action = "CheckOut", alias = UrlParameter.Optional },
                namespaces: new[] { "WebBanHangOnline.Controllers" }
            );
            routes.MapRoute(
               name: "ProductDetails",
               url: "chi-tiet/{alias}-{id}",
               defaults: new { controller = "Products", action = "ViewDetails", alias = UrlParameter.Optional },
               namespaces: new[] { "WebBanHangOnline.Controllers" }
           );
            routes.MapRoute(
               name: "ProductCategory",
               url: "danh-muc-san-pham/{alias}-{id}",
               defaults: new { controller = "Products", action = "Index", alias = UrlParameter.Optional },
               namespaces: new[] { "WebBanHangOnline.Controllers" }
           );

          
            routes.MapRoute(
               name: "Default",
               url: "{controller}/{action}/{id}",
               defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
               namespaces: new[] { "WebBanHangOnline.Controllers" }
           );
      
        }
    }
}

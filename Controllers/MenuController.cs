using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHangOnline.Models;

namespace WebBanHangOnline.Controllers
{
    public class MenuController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Menu
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult MenuTop()
        {
            var items = db.Category.OrderBy(x => x.Position).ToList();
            return PartialView("_MenuTop", items);

        }

        public ActionResult MenuProductCategory()
        {
            var items = db.ProductCategory.ToList();
            return PartialView("_MenuProductCategory", items);
        }

        public ActionResult MenuLeft(int? id)
        {
            if(id != null)
            {
                ViewBag.CateId = id;
            }
            
            var items = db.ProductCategory.ToList();
            return PartialView("_MenuLeft", items);
        }
        public ActionResult MenuArrivals() 
        {
            var items = db.ProductCategory.ToList();
            return PartialView("_MenuArrivals", items);
        }

     /*   public ActionResult GetIdCategory()
        {
            try
            {
                var items = (from p in db.ProductCategory

                             select new
                             {

                                 Id = p.Id,
                                 TenDMSanPham = p.Title,
                                 Alias = p.Alias

                             }).OrderByDescending(p => p.Id).ToList();
                return Json(new { data = items, TotalItems = items.Count, success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, msg = "Bạn đã lấy dữ liệu thất bại" + ex.Message });
            }

        }
        public ActionResult ShowMenuArrivals()
        {
            return View();
        }*/

    }
}
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHangOnline.Models;

namespace WebBanHangOnline.Controllers
{
    public class ProductsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Products
     /*   public ActionResult Index(int? id)
        {
            var items = db.Product.OrderByDescending(n => n.Id).Where(n => n.IsActive).ToList();
            if (id != null)
            {
                items = items.Where(n => n.ProductCategoryId == id ).ToList();
               
            }
            
            
            return View(items);
            
        }*/
        public ActionResult Index(int? id, string alias)
        {
            var items = db.Product.OrderByDescending(n => n.Id == id).Where(n => n.IsActive).ToList();
            if (id > 0)
            {
                items = items.Where(n => n.ProductCategoryId == id).ToList();

            }
            ViewBag.product = items;
            var cate = db.ProductCategory.Find(id);
            if (cate != null)
            {
                ViewBag.CateName = cate.Title;

            }
            ViewBag.CateId = id;
     
         
            return View(items);

        }
        public ActionResult ViewDetails(string alias,int id,int? procateid)
        {
           var items = db.Product.Find(id);
            ViewBag.prodetails = items;
            if (ViewBag.prodetails != null)
            {
                db.Product.Attach(items);
                items.ViewCount = items.ViewCount + 1;
                db.Entry(items).Property(x => x.ViewCount).IsModified = true;
                db.SaveChanges();
            }
           
            
            return View();
            
        }
      
        public ActionResult Partial_ItemCateId()
        {
            var items = db.Product.Where(x => x.IsActive).ToList();
            return PartialView(items);
        }
        public ActionResult Partial_ProductSales()
        {
            var items = db.Product.Where(x => x.IsActive).ToList();
            return PartialView(items);
        }
  /*      public JsonResult GetDataProCateId()
        {


            try
            {

                var items = (from n in db.Product
                             join p in db.ProductCategory on n.ProductCategoryId equals p.Id
                             join m in db.ProductImage on n.Id equals m.ProductId

                             select new
                             {

                                 Id = n.Id,
                                 TenDMSanPham = p.Title,
                                 TenSanPham = n.Title,
                                 Price = n.Price,
                                 PriceSale = n.PriceSale,
                                 HinhAnh = n.Image,
                                 ThuMucAnh = m.Image,
                                 Quantity = n.Quantity,
                                 CreatedDate = n.CreatedDate,
                                 Alias = n.Alias,
                                 Detail = n.Detail,
                                 IsHot = n.IsHot,
                                 IsActive = n.IsActive,
                                 IsSale = n.IsSale,
                                 IsDefault = m.IsDefault


                             }).OrderByDescending(n => n.Id).Where(n => n.IsDefault).ToList();
                return Json(new { data = items, TotalItems = items.Count, success = true }, JsonRequestBehavior.AllowGet);
            }

            catch (Exception ex)
            {
                return Json(new { success = false, msg = "Bạn đã lấy dữ liệu thất bại" + ex.Message });

            }



        }*/
    }
}
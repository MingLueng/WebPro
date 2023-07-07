using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHangOnline.Models;
using WebBanHangOnline.Models.EF;

namespace WebBanHangOnline.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProductCategoryController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Admin/ProductCategory
        public ActionResult Index()
        {
            var items = db.ProductCategory.ToList();
            return View(items);
        }
       /* public ActionResult GetProductCategory()
        {
            try
            {
                var items = (from c in db.ProductCategory
                             select new
                             {
                                 ProCateId = c.Id,
                                 TenDMSanPham = c.Title
                            

                             }).ToList();
                return Json(new { data = items, TotalItems = items.Count, success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                return Json(new { success = false, msg = "Bạn đã lấy dữ liệu thất bại" + ex.Message });
            }
        }*/
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(ProductCategory model)
        {
            if (ModelState.IsValid)
            {
                model.CreatedDate = DateTime.Now;
                model.ModifiedDate = DateTime.Now;      
                model.Alias = WebBanHangOnline.Models.Common.Filter.FilterChar(model.Title);
                db.ProductCategory.Add(model);
                db.SaveChanges();
                return RedirectToAction("Index");

            }
            return View(model);
        }
        
    }
}
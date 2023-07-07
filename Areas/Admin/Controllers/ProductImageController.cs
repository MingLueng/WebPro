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
    public class ProductImageController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Admin/ProductImage
        public ActionResult Index(int id)
        {
            ViewBag.ProductId = id;
            var items = db.ProductImage.Where(n => n.ProductId == id).ToList();
            return View(items);
        }
        public ActionResult AddImage(string url,int productId)
        {
            db.ProductImage.Add(new ProductImage
            {
                ProductId = productId,
                Image = url,
                IsDefault = false

            });
            db.SaveChanges();
            return Json(new { success = true});
        }
        public ActionResult Delete(int id) {
            var items = db.ProductImage.Find(id);
            if(items != null)
            {
                db.ProductImage.Remove(items);
                db.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false });

        }
        
    }
}
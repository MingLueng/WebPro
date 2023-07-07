using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHangOnline.Models;
using WebBanHangOnline.Models.EF;


namespace WebBanHangOnline.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CategoryController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Admin/Category
        public ActionResult Index()
        {
           /* var items = db.Category.ToList();*/
            return View();
        }
        [HttpGet]
        public ActionResult GetCategory()
        {
            try
            {
                var items = (from c in db.Category
                             select new
                             {
                                Id = c.Id,
                                 Title = c.Title,
                                 Position = c.Position
                                 
                             }).ToList();
                return Json(new { data = items, TotalItems = items.Count, success = true }, JsonRequestBehavior.AllowGet);
            }
            catch(Exception ex)
            {
                
                return Json(new { success = false, msg = "Bạn đã lấy dữ liệu thất bại" + ex.Message });
            }
        }
        public ActionResult Add()
        {
            /* var items = db.Category.ToList();*/
            return View();
        }
        [HttpPost]
     
        public ActionResult AddCategory(Category category)
        {
            try
            {
                category.CreatedDate = DateTime.Now;
                category.ModifiedDate = DateTime.Now;
                category.Alias = WebBanHangOnline.Models.Common.Filter.FilterChar(category.Title);
                db.Category.Add(category);
                db.SaveChanges();
                return Json(new { success = true, msg="Bạn đã lưu thành công bản ghi này" });
            }
            catch (Exception ex)
            {
                
                return Json(new { success = false, msg = "Bạn đã lưu thất bại" + ex.Message });
            }
       


        }
        public ActionResult Edit(int id)
        {
            var item = db.Category.Find(id);
            return View(item);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                db.Category.Attach(category);
                category.ModifiedDate = DateTime.Now;
                category.Alias = WebBanHangOnline.Models.Common.Filter.ChuyenCoDauThanhKhongDau(category.Title);
                db.Entry(category).Property(x => x.Title).IsModified = true;
                db.Entry(category).Property(x => x.Description).IsModified = true;
                db.Entry(category).Property(x => x.Alias).IsModified = true;
                db.Entry(category).Property(x => x.SeoDescription).IsModified = true;
                db.Entry(category).Property(x => x.SeoKeywords).IsModified = true;
                db.Entry(category).Property(x => x.SeoTitle).IsModified = true;
                db.Entry(category).Property(x => x.Position).IsModified = true;
                db.Entry(category).Property(x => x.ModifiedBy).IsModified = true;
                db.Entry(category).Property(x => x.ModifiedDate).IsModified = true;
                db.SaveChanges();
                return RedirectToAction("Index");

            }
            return View(category); 
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var item = db.Category.Find(id);
            if (item != null)
            {
                db.Category.Remove(item);
                db.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }

    }
}
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHangOnline.Models;
using WebBanHangOnline.Models.EF;


namespace WebBanHangOnline.Areas.Admin.Controllers
{
    public class NewsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Admin/News
     
        public ActionResult Index()
        {
            
            return View();
        }
        [HttpGet]
        public JsonResult GetNews(string searchName,int? page,int? pageSize)
        {
            try
                
            {
                var items = (from c in db.Category 
                             join n in db.News on c.Id equals n.CategoryId
                                           select new
                                           {
                                               Id =n.Id,
                                               TenDanhMuc = c.Title,
                                               TenTinTuc = n.Title,
                                               Image = n.Image,
                                               CategoryId = n.CategoryId,
                                               Alias = n.Alias,
                                               CreatedDate = n.CreatedDate,
                                               IsActive = n.IsActive,
                                           }).OrderByDescending(n => n.Id).ToList();

                if (!string.IsNullOrEmpty(searchName))
                {
                    items = items.Where(n => n.TenTinTuc.Contains(searchName.Trim().ToLower()) || n.Alias.ToLower().Contains(searchName.Trim().ToLower())).ToList(); 
                }

                var _pageSize = pageSize ?? 5;
                var pageIndex = page ?? 1;
                var totalPage = items.Count;
                var numberPage = Math.Ceiling((float)totalPage / _pageSize);
                var start = (pageIndex - 1) * _pageSize;
                items = items.Skip(start).Take(_pageSize).OrderByDescending(n => n.Id).ToList();
                

                return Json(new { data = items ,CurrentPage=pageIndex, NumberPage = numberPage, TotalItems = items.Count, success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, msg = "Bạn đã lấy dữ liệu thất bại" + ex.Message });
            }
           
        }
        [HttpGet] 
        public ActionResult Add()
        {
            
            return View();

        }

        [HttpPost]
        public ActionResult AddNews(News model)
        {
            if (ModelState.IsValid) {


                model.CreatedDate = DateTime.Now;
                model.ModifiedDate = DateTime.Now;
                model.Alias = WebBanHangOnline.Models.Common.Filter.FilterChar(model.Title);

                
                db.News.Add(model);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            
            return View(model);

        }
        public ActionResult Edit()
        {
            
            return View();
        }
        [HttpPost]
        public JsonResult EditNews(int id)
        {
            var items = (from c in db.Category
                         join n in db.News on c.Id equals n.CategoryId
                         select new
                         {
                             Id = n.Id,
                             TenDanhMuc = c.Title,
                             TenTinTuc = n.Title,
                             Image = n.Image,
                             CategoryId = n.CategoryId,
                             Alias = n.Alias,
                             CreatedDate = n.CreatedDate,
                             IsActive = n.IsActive,
                         }).Where(n => n.Id == id).SingleOrDefault();

            return Json(new { data = items,success = true }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Update(News request)
        {

            var items = db.News.Find(request.Id);
            db.News.Attach(items);
            db.Entry(items).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();


            return Json(new { success = false });


        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var item = db.News.Find(id);
            if (item != null)
            {
                db.News.Remove(item);
                db.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }
        [HttpPost]
        public ActionResult IsActive(int id)
        {
            var item = db.News.Find(id);
            if (item != null)
            {
                item.IsActive = !item.IsActive;
                db.Entry(item).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return Json(new { success = true, isActive = item.IsActive });
            }
            return Json(new { success = false });
        }

        [HttpPost]
        public ActionResult DeleteAll(string str)
        {
            if(!string.IsNullOrEmpty(str))
            {
                var items = str.Split(',');
                if(items != null && items.Any())
                {
                    foreach( var item in items) 
                    {
                        var obj = db.News.Find(Convert.ToInt32(item));
                        db.News.Remove(obj);
                        db.SaveChanges();
                    }
                }
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }
    }   
        
}
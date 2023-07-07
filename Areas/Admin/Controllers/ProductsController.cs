using Microsoft.Ajax.Utilities;
using PagedList;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHangOnline.Models;
using WebBanHangOnline.Models.EF;
using WebBanHangOnline.Models.Common;
using System.Drawing.Printing;
using System.Web.UI;
using CKFinder.Settings;

namespace WebBanHangOnline.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProductsController : Controller
    {
        // GET: Admin/Products
        private ApplicationDbContext db = new ApplicationDbContext();

        ProductDAO _repository = null;
    

        public ProductsController()
        {
            _repository = new ProductDAO();
        }



        public ActionResult Index()
        {

            ViewBag.lstCategorys = _repository.GetItemCategory();
            return View();
        }

        [HttpGet]
        public JsonResult GetProducts(string searchName, int? procateid,int? page,int? pageSize)
        {
            
            var items = _repository.GetItemFull(searchName, procateid);
            var _pageSize = pageSize ?? 5;
            var pageIndex = page ?? 1;
            var totalPage = items.Count;
            var numberPage = (int)Math.Ceiling((float)totalPage / _pageSize);
            var start = (pageIndex - 1) * _pageSize;
            items = items.Skip(start).Take(_pageSize).ToList();







            if (items != null && items.Any())
            {
                return Json(new { data = items, CurrentPage = pageIndex, NumberPage = numberPage, TotalItems = totalPage, success = true }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { success = false, TotalItems = 0 },JsonRequestBehavior.AllowGet );

        }

        public ActionResult Add()
        {
            ViewBag.ProductCategory = new SelectList(db.ProductCategory.ToList(), "Id", "Title");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(Product model, List<string> Images, List<int> rDefault)
        {
            if (ModelState.IsValid)
            {
                if(Images != null && Images.Count > 0)
                {
                    for(int i = 0; i < Images.Count; i++)
                    {
                        if(i + 1 == rDefault[0])
                        {
                            model.Image = Images[i];
                            model.ProductImage.Add(new ProductImage
                            {
                               ProductId = model.Id,
                               Image = Images[i],
                               IsDefault =true
                            
                            });
                        }
                        else
                        {
                            model.ProductImage.Add(new ProductImage
                            {
                                ProductId = model.Id,
                                Image = Images[i],
                                IsDefault = false

                            });
                        }
                    }
                }
                model.CreatedDate = DateTime.Now;
                model.ModifiedDate = DateTime.Now;
              

                if (string.IsNullOrEmpty(model.SeoTitle))
                {
                    model.SeoTitle = model.Title;
                }
                if (string.IsNullOrEmpty(model.SeoDescription))
                {
                    model.SeoDescription = model.Description;
                }
                if (string.IsNullOrEmpty(model.Alias))
                {
                    model.Alias = WebBanHangOnline.Models.Common.Filter.FilterChar(model.Title);
                }

                db.Product.Add(model);
                db.SaveChanges();
                return RedirectToAction("Index");


            }
            ViewBag.ProductCategory = new SelectList(db.ProductCategory.ToList(), "Id", "Title");
            return View(model);
        }
        [HttpPost]
        public ActionResult IsActive(int id)
        {
            var item = db.Product.Find(id);
            if (item != null)
            {
                item.IsActive = !item.IsActive;
                db.Entry(item).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return Json(new { success = true, isActive = item.IsActive });
            }
            return Json(new { success = false });
        }
        public ActionResult IsHot(int id)
        {
            var item = db.Product.Find(id);
            if (item != null)
            {
                item.IsHot = !item.IsHot;
                db.Entry(item).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return Json(new { success = true, isHot = item.IsHot });
            }
            return Json(new { success = false });
        }
        public ActionResult IsSale(int id)
        {
            var item = db.Product.Find(id);
            if (item != null)
            {
                item.IsSale = !item.IsSale;
                db.Entry(item).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return Json(new { success = true, isSale = item.IsSale });
            }
            return Json(new { success = false });
        }
        public ActionResult Delete(int id)
        {
            var item = db.Product.Find(id);
            if (item != null)
            {
                db.Product.Remove(item);
                db.SaveChanges();
                return Json(new { success = true, JsonRequestBehavior.AllowGet });
            }
            return Json(new { success = false, JsonRequestBehavior.AllowGet });
        }
        public ActionResult DeleteAll(string str)
        {
            if (!string.IsNullOrEmpty(str))
            {
                var items = str.Split(',');
                if (items != null && items.Any())
                {
                    foreach (var item in items)
                    {
                        var obj = db.Product.Find(Convert.ToInt32(item));
                        db.Product.Remove(obj);
                        db.SaveChanges();
                    }
                }
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }
        public ActionResult Edit(int id)
        {
            ViewBag.ProductCategory = new SelectList(db.ProductCategory.ToList(), "Id", "Title");
            var items = db.Product.Find(id);
            return View(items);
            
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product model, List<string> Images, List<int> rDefault)
        {
            if (ModelState.IsValid)
            {

                if (Images != null && Images.Count > 0)
                {
                    for (int i = 0; i < Images.Count; i++)
                    {
                        if (i + 1 == rDefault[0])
                        {
                            model.Image = Images[i];
                            model.ProductImage.Add(new ProductImage
                            {
                                ProductId = model.Id,
                                Image = Images[i],
                                IsDefault = true

                            });
                        }
                        else
                        {
                            model.ProductImage.Add(new ProductImage
                            {
                                ProductId = model.Id,
                                Image = Images[i],
                                IsDefault = false

                            });
                        }
                    }
                }
                model.CreatedDate = DateTime.Now;
                model.ModifiedDate = DateTime.Now;


                if (string.IsNullOrEmpty(model.SeoTitle))
                {
                    model.SeoTitle = model.Title;
                }
                if (string.IsNullOrEmpty(model.SeoDescription))
                {
                    model.SeoDescription = model.Description;
                }
                if (string.IsNullOrEmpty(model.Alias))
                {
                    model.Alias = WebBanHangOnline.Models.Common.Filter.FilterChar(model.Title);
                }
                db.Product.Attach(model);
                db.Entry(model).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");

            }
            return View(model);
        }
    }
}
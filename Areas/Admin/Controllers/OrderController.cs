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
using System.Web.Services.Description;

namespace WebBanHangOnline.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class OrderController : Controller
    {
        OrderDAO _repository = new OrderDAO();
       


        // GET: Admin/Order
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult GetOrderItem(string searchName, int? page, int? pageSize)
        {
            var items = _repository.getListFull();
            if (!string.IsNullOrEmpty(searchName))
            {
                items = items.Where(n => n.CustomerName.ToLower().Contains(searchName.Trim().ToLower())).ToList();
            }
            var _pageSize = pageSize ?? 5;
            var pageIndex = page ?? 1;
            var totalPage = items.Count;
            var numberPage = Math.Ceiling((float)totalPage / _pageSize);
            var start = (pageIndex - 1) * _pageSize;
            items = items.Skip(start).Take(_pageSize).ToList();
            if (items != null && items.Any())
            {
                return Json(new { data = items, CurrentPage = pageIndex, NumberPage = numberPage, TotalItems = totalPage, success = true }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { success = false, TotalItems = 0 }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ViewOrder(int id)
        {

            var items = _repository.getItem(id);
            ViewBag.OrderDetail = items;
            return View(items);
        }
     
        public ActionResult Partial_OrderSP(int id)
        {
            var items = _repository.getItemDetail(id);
            ViewBag.OrderDetail = items;
            return PartialView(items);
        }

        public ActionResult Update(int id,int trangthai)
        {
            var items = _repository.UpdateTT(id, trangthai);
            if (items != null)
            {
                return Json(new { message = "Success", success = true }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { message = "Unsuccess", success = false }, JsonRequestBehavior.AllowGet);

           /* var items = db.Order.Find(id);
            if(items != null)
            {
                db.Order.Attach(items);
                items.TypePayment = trangthai;
                db.Entry(items).Property(x => x.TypePayment).IsModified = true;
                db.SaveChanges();
                return Json(new { message = "Success", success = true }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { message = "Unsuccess", success = false }, JsonRequestBehavior.AllowGet);*/
        }
    }
}
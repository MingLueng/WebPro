using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHangOnline.Models;
using WebBanHangOnline.Models.EF;

namespace WebBanHangOnline.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Partial_Subscribe()
        {
            return PartialView();
        }
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Subscribe(Subscribe rey)
        {
            if (ModelState.IsValid)
            {
                db.Subscribe.Add(new Subscribe { Email=rey.Email,CreateDate = DateTime.Now});
                db.SaveChanges();
                return Json(new {Success=true});
            }
            return RedirectToAction("Partial_Subscribe");
        }

        public ActionResult Refresh()
        {
            var item = new ThongKeModel();
            ViewBag.Visitors_online = HttpContext.Application["visitors_online"].ToString();
            item.HomNay = HttpContext.Application["HomNay"].ToString();
            item.HomQua = HttpContext.Application["HomQua"].ToString();
            item.TuanNay = HttpContext.Application["TuanNay"].ToString();
            item.TuanTruoc = HttpContext.Application["TuanTruoc"].ToString();
            item.ThangNay = HttpContext.Application["ThangNay"].ToString();
            item.ThangTruoc = HttpContext.Application["ThangTruoc"].ToString();
            item.TatCa = HttpContext.Application["TatCa"].ToString();
            return PartialView(item);
        }



    }
}
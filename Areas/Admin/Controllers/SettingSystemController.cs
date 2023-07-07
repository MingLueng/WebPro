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
   
    public class SettingSystemController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Admin/SettingSystem
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Partial_SettingSystem()
        {
            return PartialView();
        }
        public ActionResult AddSetting(SettingSystemViewModel reg)
        {
           SystemSetting set = null;
           var checkTitle = db.SystemSetting.FirstOrDefault(x => x.SettingKey.Contains("SettingTitle"));
           if(checkTitle == null || string.IsNullOrEmpty(checkTitle.SettingValue)) 
           {
                set = new SystemSetting();
                set.SettingKey = "SettingTitle";
                set.SettingValue = reg.SettingTitle;
                db.SystemSetting.Add(set);
                db.SaveChanges();

           }
            else
            {
                checkTitle.SettingValue = reg.SettingTitle;
                db.Entry(checkTitle).State = System.Data.Entity.EntityState.Modified;


            }
            //logo
            var checkLogo = db.SystemSetting.FirstOrDefault(x => x.SettingKey.Contains("SettingLogo"));
            if (checkLogo == null || string.IsNullOrEmpty(checkLogo.SettingValue))
            {
                set = new SystemSetting();
                set.SettingKey = "SettingLogo";
                set.SettingValue = reg.SettingLogo;
                db.SystemSetting.Add(set);
                db.SaveChanges();

            }
            else
            {
                checkLogo.SettingValue = reg.SettingLogo;
                db.Entry(checkLogo).State = System.Data.Entity.EntityState.Modified;


            }
            //email
            var checkEmail = db.SystemSetting.FirstOrDefault(x => x.SettingKey.Contains("SettingEmail"));
            if (checkEmail == null || string.IsNullOrEmpty(checkEmail.SettingValue))
            {
                set = new SystemSetting();
                set.SettingKey = "SettingEmail";
                set.SettingValue = reg.SettingEmail;
                db.SystemSetting.Add(set);
                db.SaveChanges();

            }
            else
            {
                checkEmail.SettingValue = reg.SettingEmail;
                db.Entry(checkEmail).State = System.Data.Entity.EntityState.Modified;


            }
            //hotline
            var checkHotline = db.SystemSetting.FirstOrDefault(x => x.SettingKey.Contains("SettingHotline"));
            if (checkHotline == null || string.IsNullOrEmpty(checkHotline.SettingValue))
            {
                set = new SystemSetting();
                set.SettingKey = "SettingHotline";
                set.SettingValue = reg.SettingHotline;
                db.SystemSetting.Add(set);
                db.SaveChanges();

            }
            else
            {
                checkHotline.SettingValue = reg.SettingHotline;
                db.Entry(checkHotline).State = System.Data.Entity.EntityState.Modified;


            }
            //titleseo
            var checkTitleSeo = db.SystemSetting.FirstOrDefault(x => x.SettingKey.Contains("SettingTitleSeo"));
            if (checkTitleSeo == null || string.IsNullOrEmpty(checkTitleSeo.SettingValue))
            {
                set = new SystemSetting();
                set.SettingKey = "SettingTitleSeo";
                set.SettingValue = reg.SettingTitleSeo;
                db.SystemSetting.Add(set);
                db.SaveChanges();

            }
            else
            {
                checkTitleSeo.SettingValue = reg.SettingTitleSeo;
                db.Entry(checkTitleSeo).State = System.Data.Entity.EntityState.Modified;


            }
            //descriptionseo
            var checkDesSeo = db.SystemSetting.FirstOrDefault(x => x.SettingKey.Contains("SettingDesSeo"));
            if (checkDesSeo == null || string.IsNullOrEmpty(checkDesSeo.SettingValue))
            {
                set = new SystemSetting();
                set.SettingKey = "SettingDesSeo";
                set.SettingValue = reg.SettingDesSeo;
                db.SystemSetting.Add(set);
                db.SaveChanges();

            }
            else
            {
                checkDesSeo.SettingValue = reg.SettingDesSeo;
                db.Entry(checkDesSeo).State = System.Data.Entity.EntityState.Modified;


            }
            //keyseo
            var checkKeySeo = db.SystemSetting.FirstOrDefault(x => x.SettingKey.Contains("SettingKeySeo"));
            if (checkKeySeo == null || string.IsNullOrEmpty(checkKeySeo.SettingValue))
            {
                set = new SystemSetting();
                set.SettingKey = "SettingKeySeo";
                set.SettingValue = reg.SettingKeySeo;
                db.SystemSetting.Add(set);
                db.SaveChanges();

            }
            else
            {
                checkKeySeo.SettingValue = reg.SettingKeySeo;
                db.Entry(checkKeySeo).State = System.Data.Entity.EntityState.Modified;


            }
            return View("Partial_SettingSystem");
        }
        
    }
}
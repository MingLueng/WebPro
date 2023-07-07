using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Web;
using System.Web.UI;
using WebBanHangOnline.Models.EF;
using WebBanHangOnline.Models;
using System.Web.Mvc;

namespace WebBanHangOnline.Models.Common
{
    public class ProductDAO
    {
        private static ApplicationDbContext db = new ApplicationDbContext();
       
        public List<ProViewModel> GetItemFull(string searchName, int? procateid)
        {

            /* var lstpros = db.Product.OrderBy(n => n.Id).ToList();
             List<ProViewModel> lstviewpro = new List<ProViewModel>();
             ProViewModel nv;*/
            
           var item = (
                           from n in db.Product
                           join c in db.ProductCategory on n.ProductCategoryId equals c.Id
                           join m in db.ProductImage on n.Id equals m.ProductId
                           select new ProViewModel
                           {

                               Id = n.Id,
                               ProCategoryName = c.Title,
                               Title = n.Title,
                               Price = n.Price,
                               PriceSale = n.PriceSale,
                               Image = m.Image,
                               Quantity = n.Quantity,
                               CreatedDate = n.CreatedDate,
                               Alias = n.Alias,
                               Detail = n.Detail,
                               IsHot = n.IsHot,
                               IsActive = n.IsActive,
                               IsSale = n.IsSale,
                               ProductCategoryId = c.Id,
                               IsDefault = m.IsDefault,
                              


                           }).OrderByDescending(n => n.Id).Where(m => m.IsDefault).ToList();
            if (!string.IsNullOrEmpty(searchName))
            {
                item = item.Where(n => n.Title.ToLower().Contains(searchName.Trim().ToLower()) || n.Alias.ToLower().Contains(searchName.Trim().ToLower())).ToList();
            }
            if (procateid > 0)
            {
                item = item.Where(n => n.ProductCategoryId == procateid).ToList();
            }
            return item.ToList();
            /* foreach (var phong in lstpros)
             {
                 nv = new ProViewModel();
                 nv.Id = phong.Id;
                 nv.Title = phong.Title;
                 nv.PriceSale = phong.PriceSale;
                 nv.Quantity = phong.Quantity;
                 nv.Price = phong.Price;
                 nv.Alias = phong.Alias;
                 var ct = db.ProductCategory.FirstOrDefault(x => x.Id == phong.ProductCategoryId);
                 nv.ProductCategoryId = ct.Id;
                 nv.ProCategoryName = ct.Title;
                 nv.Detail = phong.Detail;
                 nv.IsHot = phong.IsHot;
                 nv.IsActive = phong.IsActive;
                 nv.IsSale = phong.IsSale;
                 var img = db.ProductImage.FirstOrDefault(x => x.ProductId == phong.Id && x.IsDefault == true);
                 nv.IsDefault = img.IsDefault;
                 nv.Image = img.Image;
                 nv.CreatedDate = phong.CreatedDate;
                 nv.ModifiedDate = phong.ModifiedDate;

                 lstviewpro.Add(nv);


             }*/

        }
        public List<ProductCategory> GetItemCategory()
        {
            return db.ProductCategory.ToList();
        }


    }
}
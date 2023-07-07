using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;
using System.Web;
using WebBanHangOnline.Models.EF;

namespace WebBanHangOnline.Models.Common
{
    public class OrderDAO
    {
        ApplicationDbContext db = new ApplicationDbContext();
        public Order getItem(int id)
        {
            return db.Order.FirstOrDefault(x => x.Id == id);
        }
        public List<OrderDetail> getItemDetail(int id)
        {
            var items= db.OrderDetail.Where(x => x.OrderId == id).ToList();
            return items;
        }
        public List<OrderView> getListFull()
        {
           var lst = db.Order.OrderBy(n => n.Id).ToList();
            List<OrderView> lstDTO = new List<OrderView>();
      
            
            foreach (var item in lst)
            {
                OrderView orq = new OrderView();
                orq.Id = item.Id;
                orq.CustomerName = item.CustomerName;
                orq.Address = item.Address;
                orq.Phone = item.Phone;
                orq.Email = item.Email; 
                orq.TypePayment = item.TypePayment;
                var lstdefq = db.OrderDetail.Where(x => x.OrderId == item.Id).ToList(); 
                foreach(var dev in lstdefq)
                {
                    orq.ProductId = dev.ProductId;
                    orq.Quantity = dev.Quantity;
                    orq.Price = dev.Price;
                
                }; 
                orq.Code = item.Code;
                orq.ZipCode = item.ZipCode;
                orq.CreatedDate = item.CreatedDate;
                orq.ModifiedDate = item.ModifiedDate;
                orq.TotalAmount = item.TotalAmount;
            lstDTO.Add(orq);
            }
            return lstDTO;
        }

        public Order UpdateTT(int id, int trangthai)
        { 
                var _tt = db.Order.FirstOrDefault(x => x.Id == id);
               
                    db.Order.Attach(_tt);
                    _tt.TypePayment = trangthai;
                    db.Entry(_tt).Property(x => x.TypePayment).IsModified = true;
                    db.SaveChanges();
                    return _tt;
        }
       
    }
}
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHangOnline.Models;
using WebBanHangOnline.Models.EF;

namespace WebBanHangOnline.Controllers
{
    public class ShoppingCartController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: ShoppingCart
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult CheckOut()
        {
            ShoppingCart cart = (ShoppingCart)Session["Cart"];

            if (cart != null && cart.Items.Any())
            {
                ViewBag.CheckCart = cart;
                
            }
            return View();
        }
        public ActionResult Partial_Item_CheckOut()
        {
            ShoppingCart cart = (ShoppingCart)Session["Cart"];

            if (cart != null && cart.Items.Any())
            {
              
                return Json(new { success = true, data = cart.Items, TotalItems = cart.Items.Count }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { success = false, TotalItems = 0 }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetDataJSON()
        {
            ShoppingCart cart = (ShoppingCart)Session["Cart"];
            
            if (cart != null && cart.Items.Any())
            {
                
                return Json(new { success = true, data=cart.Items, TotalItems = cart.Items.Count }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { success = false, TotalItems = 0 }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult CheckOutSuccess()
        {
            return View();
        }
        public ActionResult Partial_CheckOut()
        {
            return PartialView();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CheckOut(OrderView rpg)
        {
            var code = new { success = false, msg = "", code = -1 };

            if(ModelState.IsValid)
            {

                ShoppingCart cart = (ShoppingCart) Session["Cart"];
                if(cart != null)
                {
                    Order order = new Order();

                    order.CustomerName = rpg.CustomerName;
                    order.Phone = rpg.Phone;
                    order.Address = rpg.Address;
                    order.ZipCode = rpg.ZipCode;
               

                    cart.Items.ForEach(x => order.OrderDetail.Add(new OrderDetail
                    {
                        ProductId = x.ProductId,

                        Quantity = x.Quantity,
                        Price = x.Price
                    }));
                    Random rd = new Random();
                    order.Code = "DH" + rd.Next(0, 9) + rd.Next(0, 9) + rd.Next(0, 9) + rd.Next(0, 9) + rd.Next(0, 9);
                    order.TotalAmount = cart.Items.Sum(x => (x.Price * x.Quantity));
                    order.TypePayment = rpg.TypePayment;
                    order.CreatedDate = DateTime.Now;
                    order.ModifiedDate = DateTime.Now;
                    order.CreatedBy = rpg.Phone;

                    db.Order.Add(order);
                    db.SaveChanges();

                    //send mail cho khách hàng

                    var strSanPham = "";
                    var thanhtien = decimal.Zero;
                    var tongtien = decimal.Zero;
                    foreach (var sp in cart.Items)
                    {
                        strSanPham += "<tr>";
                        strSanPham += "<td>" + sp.ProductName + "</td>";
                        strSanPham += "<td>" + sp.Quantity + "</td>";
                        strSanPham += "<td>" + WebBanHangOnline.Common.Common.FormatNumber(sp.TotalPrice, 0) + "</td>";
                        strSanPham += "</tr>";
                        thanhtien += sp.Quantity * sp.Price;

                    }
                    tongtien = thanhtien;
                    string contentCustom = System.IO.File.ReadAllText(Server.MapPath("~/Content/template/send2.html"));
                    contentCustom = contentCustom.Replace("{{MaDon}}", order.Code);
                    contentCustom = contentCustom.Replace("{{NgayDat}}", DateTime.Now.ToString("dd/MM/yyyy"));
                    contentCustom = contentCustom.Replace("{{SanPham}}", strSanPham);
                    contentCustom = contentCustom.Replace("{{ThanhTien}}", WebBanHangOnline.Common.Common.FormatNumber(thanhtien, 0));
                    contentCustom = contentCustom.Replace("{{TongTien}}", WebBanHangOnline.Common.Common.FormatNumber(tongtien, 0));
                    contentCustom = contentCustom.Replace("{{TenKhachHang}}", order.CustomerName);
                    contentCustom = contentCustom.Replace("{{Phone}}", order.Phone);
                    contentCustom = contentCustom.Replace("{{Email}}", rpg.Email);
                    contentCustom = contentCustom.Replace("{{DiaChi}}", order.Address);
                    WebBanHangOnline.Common.Common.SendMail("ShopOnline", "Đơn Hàng#" + order.Code, contentCustom.ToString(), rpg.Email);

                    string contentAdmin = System.IO.File.ReadAllText(Server.MapPath("~/Content/template/send1.html"));
                    contentAdmin = contentAdmin.Replace("{{MaDon}}", order.Code);
                    contentAdmin = contentAdmin.Replace("{{NgayDat}}", DateTime.Now.ToString("dd/MM/yyyy"));
                    contentAdmin = contentAdmin.Replace("{{SanPham}}", strSanPham);
                    contentAdmin = contentAdmin.Replace("{{ThanhTien}}", WebBanHangOnline.Common.Common.FormatNumber(thanhtien, 0));
                    contentAdmin = contentAdmin.Replace("{{TongTien}}", WebBanHangOnline.Common.Common.FormatNumber(tongtien, 0));
                    contentAdmin = contentAdmin.Replace("{{TenKhachHang}}", order.CustomerName);
                    contentAdmin = contentAdmin.Replace("{{Phone}}", order.Phone);
                    contentAdmin = contentAdmin.Replace("{{Email}}", rpg.Email);
                    contentAdmin = contentAdmin.Replace("{{DiaChi}}", order.Address);
                    WebBanHangOnline.Common.Common.SendMail("ShopOnline", "Đơn Hàng Mới" + order.Code, contentAdmin.ToString(), ConfigurationManager.AppSettings["EmailAdmin"]);
                    code = new { success = true, msg = "", code = 1 };
                    cart.ClearCart();
                    return RedirectToAction("CheckOutSuccess");
                }

            }
            return Json(code);
        }
        public ActionResult ShowCount()
        {

            ShoppingCart cart =(ShoppingCart) Session["Cart"];
            if (cart != null)
            {
                return Json( new { success = true, data = cart.Items, TotalItems = cart.Items.Count },JsonRequestBehavior.AllowGet);
            }
            return Json(new { success = false, TotalItems = 0 },JsonRequestBehavior.AllowGet);
        }
        public ActionResult AddToCart(int id,int quantity)
        {
            var code = new { success = false, msg = "", code = -1 , TotalItems = 0};
            var checkProduct = db.Product.FirstOrDefault(x => x.Id == id);
            if (checkProduct != null)
            {
                ShoppingCart cart = (ShoppingCart)Session["Cart"];
                if(cart == null)
                {
                    cart = new ShoppingCart();
                }
               
                ShoppingCartItem item = new ShoppingCartItem();
                
                    item.ProductId = checkProduct.Id;
                    item.Quantity = checkProduct.Quantity;
                    item.CategoryName = checkProduct.ProductCategory.Title;
                    item.ProductName = checkProduct.Title;
                    item.AliasName = checkProduct.Alias;
                    item.Price = checkProduct.Price;

                
                if (checkProduct.ProductImage.FirstOrDefault(x => x.IsDefault) != null)
                {
                    item.ProductImg = checkProduct.ProductImage.FirstOrDefault(x => x.IsDefault).Image;

                }
                
                if (checkProduct.PriceSale > 0)
                {
                    item.Price = (decimal)checkProduct.PriceSale;
                }
                item.TotalPrice = item.Quantity * item.Price;
                cart.AddToCart(item, quantity);
                Session["Cart"] = cart;
                code = new { success = true, msg = "Thêm sản phẩm vào giỏ hàng thành công", code = 1, TotalItems = cart.Items.Count};
            }
            return Json(code);
           
        }
        public ActionResult Delete(int id)
        {
            var code = new { success = false, msg = "", code = -1, TotalItems = 0 };
            ShoppingCart cart = (ShoppingCart)Session["Cart"];
            if (cart != null)
            {
                var checkProduct = cart.Items.FirstOrDefault(x => x.ProductId == id);
                if(checkProduct != null)
                {
                    cart.RemoveCart(id);
                    code = new { success = true, msg = "", code = 1, TotalItems = cart.Items.Count };
                }
                
            }
            return Json(code);
        }
        public ActionResult DeleteAll()
        {
            ShoppingCart cart = (ShoppingCart)Session["Cart"];
            if (cart != null)
            {
                cart.ClearCart();
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }
        public ActionResult Update(int id, int quantity)
        {
            ShoppingCart cart = (ShoppingCart)Session["Cart"];
            if (cart != null)
            {
                cart.UpdateQuantity(id, quantity);
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }
    }
}
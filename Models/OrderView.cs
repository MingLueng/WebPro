using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebBanHangOnline.Models
{
    public class OrderView : CommonAbstract
    {
        public int Id { get; set; }
        public string Code { get; set; }
        [Required(ErrorMessage = "Tên khách hàng không được để trống")]
        public string CustomerName { get; set; }
        [Required(ErrorMessage = "Số điện thoại không để trống")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Địa chỉ không để trống")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Email không để trống")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Zip Code không để trống")]
        public string ZipCode { get; set; }
        public int Quantity { get; set; }
        public int ProductId { get; set; }
        public decimal Price { get; set; }
        public int TypePayment { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
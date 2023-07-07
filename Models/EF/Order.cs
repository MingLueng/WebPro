using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebBanHangOnline.Models.EF
{
    [Table("tb_Order")]
    public class Order : CommonAbstract
    {
        public Order()
        {
            this.OrderDetail = new HashSet<OrderDetail>();
        }
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
    
        public string Code { get; set; }
        [Required(ErrorMessage ="Tên khách hàng không được để trống") ]
        public string CustomerName { get; set; }
        [Required(ErrorMessage = "Số điện thoại không để trống")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Địa chỉ không để trống")]
        public string Address { get; set; }
        public int Quantity { get; set; }
        public int TypePayment { get; set; }
 /*       [Required(ErrorMessage = "Email không để trống")]*/
        public string Email { get; set; }
        [Required(ErrorMessage = "Zip Code không để trống")]
        public string ZipCode { get; set; }
        public decimal TotalAmount { get; set; }
        public virtual ICollection<OrderDetail> OrderDetail { get; set; }
    }
}
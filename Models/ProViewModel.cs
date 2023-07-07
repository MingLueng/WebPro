using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHangOnline.Models.EF;


namespace WebBanHangOnline.Models
{
    public class ProViewModel:CommonAbstract
    {
        public int Id { get; set; }
        [Required]
        [StringLength(250)]
        public string Title { get; set; }
        [StringLength(250)]
        public string Alias { get; set; }
        public string Description { get; set; }
        [AllowHtml]
        public string Detail { get; set; }
        public bool IsDefault { get; set; }
        public string ProCategoryName { get; set; }
        //[StringLength(250)]
        public string Image { get; set; }
        [StringLength(50)]
        public string ProductCode { get; set; }
        [Required(ErrorMessage = "Danh mục tin tức không được để trống")]
        public int ProductCategoryId { get; set; }
        public decimal Price { get; set; }
        public decimal? PriceSale { get; set; }
        public int ViewCount { get; set; }
        public int Quantity { get; set; }
        public bool IsHot { get; set; }
        public bool IsHome { get; set; }
        public bool IsSale { get; set; }
        public bool IsActive { get; set; }
        public bool IsFeature { get; set; }
        [StringLength(250)]
        public string SeoTitle { get; set; }
        [StringLength(500)]

        public string SeoDescription { get; set; }
        [StringLength(250)]

        public string SeoKeywords { get; set; }
        
    }
}
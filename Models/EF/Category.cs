using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebBanHangOnline.Models.EF
{
    [Table("tb_Category")]
    public class Category : CommonAbstract
    {
        public Category()
        {
            
            this.News = new HashSet<News>();
        }
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage = "Tên danh mục không được để trống")]
        [StringLength(150)] 
        public string Title { get; set; }
        [StringLength(150)]
        public string Description { get; set; }
        [StringLength(150)]

        public string SeoTitle { get; set; }
        [StringLength(150)]
        public string SeoDescription { get; set; }
        [StringLength(150)]
        public string SeoKeywords { get; set; }
        public string Alias { get; set; }
        /*[StringLength(150)]*/
      /*  public string TypeCode { get; set; }

        public string Link { get; set; }*/
        public int Position { get; set; }
        public bool IsActive { get; set; }
        public virtual ICollection<News> News { get; set; }
        public virtual ICollection<Post> Post { get; set; }
     

    }
}
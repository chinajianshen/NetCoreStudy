using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Transfer8Pro.ForeignWebAPI.Models
{  
    public class Product
    {
        /// <summary>
        /// 产品名称
        /// </summary>
        [Required]
        [StringLength(20)]
        [Display(Name="产品名称")]
      
        public string Name { get; set; }

        /// <summary>
        /// 价格
        /// </summary>
        [Display(Name = "产品价格")]
        public int Price { get; set; }

        /// <summary>
        /// 是否最新
        /// </summary>
        [Display(Name = "是否最新产品")]
        public bool IsActive { get; set; }
    }
}
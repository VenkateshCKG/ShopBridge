using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShopBridge.Models
{
    public class Product
    {
        [Required]
        public int ProductID { get; set; }
        [MaxLength(100)]
        public string ProductName { get; set; }
        [MaxLength(100)]
        public string Category { get; set; }
        [MaxLength(400)]
        public string Description { get; set; }
        [MaxLength(100)]
        public string Supplier { get; set; }
        public int CostPrice { get; set; }
        public int SellingPrice { get; set; }
        public int Quantity { get; set; }


    }
}
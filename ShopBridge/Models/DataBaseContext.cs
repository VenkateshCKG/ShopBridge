using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ShopBridge.Models
{
    public class DataBaseContext : DbContext
    {
        public DataBaseContext() : base("DefaultConString")
        {

        }
        public DbSet<Product> Products { get; set; }
    }
}
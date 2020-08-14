using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;
namespace WebApplication1.DB
{
    public class ShopContext : DbContext
    {
        public ShopContext(DbContextOptions<ShopContext> options)
         : base(options)
        {

        }
        public virtual DbSet<ProductCategory> ProductCategories { get; set; }
        public virtual DbSet<ProductAttrKey> ProductAttrKeys { get; set; }
        public virtual DbSet<ProductAttrValue> ProductAttrValues { get; set; }
    }
}

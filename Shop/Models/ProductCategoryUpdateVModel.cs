using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shop.Models
{
    public class ProductCategoryUpdateVModel
    {
        public ProductCategory Category { get; set; }
        public List<ProductCategory> Categories { get; set; }
    }
}
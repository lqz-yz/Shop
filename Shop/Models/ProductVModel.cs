using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shop.Models
{
    public class ProductVModel
    {
        public Product Product { get; set; }
        public List<ProductSku> Skus { get; set; }
        public List<ProductAttr> Attrs { get; set; }
        public List<ProductSkuImg> SkuImg { get; set; }
    }
}
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shop.Models
{
    public class ProductAttrKeyVModel: ProductAttrKey
    {
        public List<string> AttrValues { get; set; }
    }
}
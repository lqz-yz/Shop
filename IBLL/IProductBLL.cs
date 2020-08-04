using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBLL
{
    public interface IProductBLL : IBaseBLL<Product>
    {
        int Add(Product Product, List<ProductSku> Skus, List<ProductAttr> Attrs);
        Product Getone(int id, out List<ProductAttr> attrs, out List<ProductSku> skus);

        //Product Getone(int id, out List<ProductAttr> attrs, out List<ProductSku> skus, out List<ProductSkuImg> SkuImg);
        int Update(Product product, List<ProductSku> Skus, List<ProductAttr> Attrs);//, int id
    }
}

using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBLL
{
    public interface IProductAttrKeyBLL : IBaseBLL<ProductAttrKey>
    {
        //void Add(ProductAttrKey attrkey);

        List<ProductAttrKey> GetByCategoryID(int categoryId);
        int Update(ProductAttrKey attrKey, List<ProductAttrValue> attrValues);
       
    }
}

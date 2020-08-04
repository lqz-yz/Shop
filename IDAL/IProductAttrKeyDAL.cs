using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL
{
    public interface IProductAttrKeyDAL:IBaseDAL<ProductAttrKey>
    {
        //void Add(ProductAttrKey attrkey);
        List<ProductAttrKey> GetByCategoryID(int categoryId);
    }
}

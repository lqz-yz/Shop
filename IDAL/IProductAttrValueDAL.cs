using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL
{
    public interface IProductAttrValueDAL:IBaseDAL<ProductAttrValue>
    {
        //void Add(ProductAttrValue attrValue);
        List<ProductAttrValue> GetAllByAttrKeyID(int attrkeyID);
        
    }
}

using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBLL
{
    public interface IProductAttrValueBLL : IBaseBLL<ProductAttrValue>
    {
        //void Add(ProductAttrValue attrValue);

        List<ProductAttrValue> GetAllByAttrKeyID(int attrkeyID);
       
    }
}

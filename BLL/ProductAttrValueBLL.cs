using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using IDAL;
using IBLL;
using DAL;
namespace BLL
{
    public class ProductAttrValueBLL : BaseBLL<ProductAttrValue, ProductAttrValueDAL>, IProductAttrValueBLL
    {
       

        //IProductAttrValueDAL dal =new ProductAttrValueDAL();

        //public void Add(ProductAttrValue attrValue)
        //{
        //    dal.Add(attrValue);
        //}
        public List<ProductAttrValue> GetAllByAttrKeyID(int attrkeyID)
        {
            return dal.GetAllByAttrKeyID(attrkeyID);
        }
    }
}

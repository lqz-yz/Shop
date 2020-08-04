using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL;
using Model;
namespace DAL
{
    public class ProductAttrValueDAL : BaseDAL<ProductAttrValue>, IProductAttrValueDAL
    {
        //ShopEntities entities = new ShopEntities();

        //public void Add(ProductAttrValue attrValue)
        //{
        //    entities.ProductAttrValue.Add(attrValue);
        //    entities.SaveChanges();
        //}
        public List<ProductAttrValue> GetAllByAttrKeyID(int attrkeyID)
        {
            return entities.ProductAttrValue.Where(x => x.ProductAttrKeyID == attrkeyID).ToList();
        }

       
    }
}

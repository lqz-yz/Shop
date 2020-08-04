using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL;
using Model;
namespace DAL
{
    public class ProductAttrKeyDAL :BaseDAL<ProductAttrKey>,IProductAttrKeyDAL
    {
        //ShopEntities entities = new ShopEntities();
        //public void Add(ProductAttrKey attrkey)
        //{
        //    entities.ProductAttrKey.Add(attrkey);
        //    entities.SaveChanges();
        //}

        //public int Delete(int id)
        //{
        //    throw new NotImplementedException();
        //}

        //public List<ProductAttrKey> GetAll()
        //{
        //    throw new NotImplementedException();
        //}

      

        //public ProductCategory Getone(int id)
        //{
        //    throw new NotImplementedException();
        //}

        //public int Update(ProductAttrKey mode)
        //{
        //    throw new NotImplementedException();
        //}

        public List<ProductAttrKey> GetByCategoryID(int categoryId)
        {
            return entities.ProductAttrKey.Where(x => x.ProductCategoryID == categoryId).ToList();
        }
    }
}

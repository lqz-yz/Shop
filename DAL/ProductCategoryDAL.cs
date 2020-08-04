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
    //同时继承基类和接口时,基类写在接口前面.基类只能继承一个.接口可以继承多个
    public class ProductCategoryDAL:BaseDAL<ProductCategory>,IProductCategoryDAL
    {
        //ShopEntities entities = new ShopEntities();
      

        //public int Delete(int id)
        //{
        //    ProductCategory category = new ProductCategory() { ID = id};
        //    DbEntityEntry entityEntry = entities.Entry(category);
        //    entityEntry.State = EntityState.Deleted;
        //    return entities.SaveChanges();
        //}

        //public List<ProductCategory> GetAll()
        //{

        //    return entities.ProductCategory.ToList();//获取全部分类
        //}

        //public ProductCategory Getone(int id)
        //{
        //    return entities.ProductCategory.First(x => x.ID == id);
        //}


        //获取某一节点下所有子节点
        public List<ProductCategory> GetSub(int id)//父节点ID
        {
            return entities.ProductCategory.Where(x => x.PID == id).ToList();
        }

        
          public override void Update(ProductCategory model)
        {

            DbEntityEntry entityEntry = entities.Entry(model);
            entityEntry.State = EntityState.Modified;
            entityEntry.Property("CreateTime").IsModified = false;
           
        }


        //public int Update(ProductCategory category)
        //{
        //    DbEntityEntry entityEntry = entities.Entry(category);
        //    entityEntry.State = EntityState.Modified;
        //   entityEntry.Property("CreateTime").IsModified = false;
        //    return entities.SaveChanges();
        //}
    }
}

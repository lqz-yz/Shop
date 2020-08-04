using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL;
using System.Data.Entity;
using System.Linq.Expressions;

namespace BLL
{

    /// <summary>
    /// T:Model类型 D:DAL类型
    /// </summary>
    /// <typeparam name="D"></typeparam>
    /// <typeparam name="T"></typeparam>
    public class BaseBLL<T,D> where T:BaseModel,new() where D:IBaseDAL<T>,new()
    {
       public D dal = new D();
       //IProductCategoryDAL dal = new ProductCategoryDAL();
        public virtual int Add(T model)
        {
            dal.Add(model);
            return SaveChanges();
        }

        public virtual int Delete(int id)
        {
            dal.Delete(id);
            return SaveChanges(); 
        }
        public virtual int Delete(T model)
        {
            dal.Delete(model);
            return SaveChanges();
        }

        public virtual List<T> GetAll()
        {

            return dal.GetAll();
        }

        public virtual List<T> Search(Expression<Func<T, bool>> where)
        {
            return dal.Search(where);
        }
        //public virtual List<T> Search(int pageSize, int pageIndex, bool isDesc, Expression<Func<T, bool>> where)
        //{
        //    return dal.Search(pageSize, pageIndex, isDesc, where);
        //}
       
         public virtual List<T> Search<TKey>(int pageSize, int pageIndex, bool isDesc, Func<T, TKey> orderkey, Expression<Func<T, bool>> where, out int count)
        {
            return dal.Search(pageSize, pageIndex,  isDesc, orderkey, where,out count);
        }
        public virtual int GetCount(Expression<Func<T, bool>> where) {
            return dal.GetCount(where);
        }
        public virtual T Getone(int id)
        {
            return dal.Getone(id);
        }
        public virtual int Update(T model)
        {
            dal.Update(model);
            return SaveChanges();
               
        }
        public int SaveChanges()
        {
            return dal.SaveChanges();//事务的提交
        }
        public DbContextTransaction BeginTrane()
        {
            return dal.BeginTrane();
        }
    }
}

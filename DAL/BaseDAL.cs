using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using IDAL;
using Model;

namespace DAL
{
    //泛型约束;where T:class是对泛型T做了一个约束,必须是class,new()必须是无参的构造函数
    //T:BaseModel 指定T必须继承BaseModel
    public class BaseDAL<T> : IBaseDAL<T> where T : BaseModel, new()
    {
        public ShopEntities entities = DbContextFactory.GetEntities();
        public virtual void Add(T model)
        {

            DbEntityEntry entityEntry = entities.Entry(model);
            entityEntry.State = EntityState.Added;

        }
        public virtual void Delete(int id)
        {
            T model = new T() { ID = id };
            DbEntityEntry entityEntry = entities.Entry<T>(model);
            entityEntry.State = EntityState.Deleted;

        }
        //引用类型class . string 不能是int double bool
        public virtual List<T> GetAll()
        {
            return entities.Set<T>().ToList();
        }
        public virtual T Getone(int id)
        {
            return entities.Set<T>().First(x => x.ID == id);
        }
        public virtual void Update(T model)
        {

            DbEntityEntry entityEntry = entities.Entry(model);
            entityEntry.State = EntityState.Modified;

        }
        public virtual void Delete(T model)
        {

            DbEntityEntry entityEntry = entities.Entry(model);
            entityEntry.State = EntityState.Deleted;

        }
        public int SaveChanges()
        {
            return entities.SaveChanges();//事务的提交
        }

        public virtual List<T> Search(Expression<Func<T, bool>> where)
        {

            // .AsNoTracking() EF查询完之后不会缓存，不会跟踪
            /**
             在EF经常在更新模型的时候可能会同时操作一个实体几次。
                其实除了SaveChanges外，其它的几次基本都是要查询出一个结果，
              例如更新的时候，我们要查一下这 个表中有没有相同的纪录之类的。
        查询完之后，我们再SaveChanges就会出错。
                 怎么办呢                  ?
                    查询的时候我们用这个方法查询: AsNoTracking( )
                   db. Set<实体模型>()]
                    . AsNoTracking( )
                   .FirstOrDefault(p => p.x == x)
                     这个方法返回一个新查询结果，但返回的实体不会在缓存中。也就是EF查完了就不再跟踪了
                   好了。
                   */
            return entities.Set<T>().Where(where).ToList();
        }

        //public virtual List<T> Search(int pageSize, int pageIndex, bool isDesc, Expression<Func<T, bool>> where)
        //{

        //    if (isDesc)
        //    {
        //        return entities.Set<T>().Where(where).OrderByDescending(x => x.ID).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
        //    }
        //    else
        //    {
        //        return entities.Set<T>().Where(where).OrderBy(x => x.ID).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
        //    }

        //}
        public virtual List<T> Search<TKey>(int pageSize, int pageIndex, bool isDesc, Func<T, TKey > orderkey, Expression<Func<T, bool>> where,out int count)
        {
            //离开当前方法必须给out参数赋值
            count = entities.Set<T>().Where(where).Count();
            if (isDesc)
            {
                return entities.Set<T>().Where(where).OrderByDescending(orderkey).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            }
            else
            {
                return entities.Set<T>().Where(where).OrderBy(orderkey).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            }

        }
        public virtual int GetCount(Expression<Func<T, bool>> where)
        {//商品分页总数
            return entities.Set<T>().Where(where).Count();
        }
        public DbContextTransaction BeginTrane()
        {
            var tran = entities.Database.BeginTransaction();//开启事务
            return tran;
        }
    }
}

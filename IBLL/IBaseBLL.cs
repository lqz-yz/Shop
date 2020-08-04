using Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IBLL
{
    public interface IBaseBLL<T>
    {
        int Add(T model);
        int Delete(int id);
        int Delete(T model);
        int Update(T model);
        T Getone(int id);
        List<T> GetAll();
        //商品添加 使用

        List<T> Search(Expression<Func<T, bool>> where);//根据条件查询
        //List<T> Search(int pageSize, int pageIndex, bool isDesc, Expression<Func<T, bool>> where);//商品展示分页及展示数量
        List<T> Search<TKey>(int pageSize, int pageIndex, bool isDesc, Func<T, TKey> orderkey, Expression<Func<T, bool>> where, out int count);
        int GetCount(Expression<Func<T, bool>> where);
        int SaveChanges();
        DbContextTransaction BeginTrane();
    }
}

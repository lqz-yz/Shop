using ImTools;
using Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IDAL
{
    /// <summary>
    /// T代表Model类型
    /// </summary>
    /// <param name="id"></param>
   
    public interface IBaseDAL<T>
    {
        /// <summary>
       /// 添加
       /// </summary>
       /// <param name="id">主键</param>
       /// <returns></returns>
        void Add(T model);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>受影响的行数</returns>
        void Delete(int id);

        void Delete(T model);
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        void Update(T model);

        /// <summary>
        /// 获取单条记录
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        T Getone(int id);

        /// <summary>
        /// 获取全部记录
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        List<T> GetAll();
        //商品添加 使用
        List<T> Search(Expression<Func<T, bool>> where);

        //List<T> Search( int pageSize,int pageIndex,bool isDesc, Expression<Func<T, bool>> where);
        List<T> Search<TKey>(int pageSize, int pageIndex, bool isDesc, Func<T, TKey> orderkey, Expression<Func<T, bool>> where, out int count);
        int GetCount(Expression<Func<T, bool>>  where);
        int SaveChanges();
        DbContextTransaction BeginTrane();
    }
}

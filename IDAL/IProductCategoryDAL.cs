using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL
{

    public interface IProductCategoryDAL:IBaseDAL<ProductCategory>
    {
        //void Add(ProductCategory category);
        //List<ProductCategory> GetAll();

 

        ///// <summary>
        ///// 分类的删除
        ///// </summary>
        ///// <param name="id">主键</param>
        ///// <returns>受影响的行数</returns>
        //int Delete(int id);


        //int Update(ProductCategory category);

        //ProductCategory Getone(int id);


        //获取某一节点下所有子节点
        List<ProductCategory> GetSub(int id);//id为父节点ID
    }
}

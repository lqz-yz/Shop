using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBLL
{
    public interface IProductCategoryBLL:IBaseBLL<ProductCategory>
    {
        //void Add(ProductCategory category);
        //int Delete(int id);
        //int Update(ProductCategory category);
        //ProductCategory Getone(int id);
        //List<ProductCategory> GetAll();

        //获取某一节点下所有子节点
        List<ProductCategory> GetSub(int id);//id为父节点ID
    }
}

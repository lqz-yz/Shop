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
    public class ProductAttrKeyBLL : BaseBLL<ProductAttrKey, ProductAttrKeyDAL>,IProductAttrKeyBLL
    {
        public override int Delete(int id)
        {
            dal.Delete(id);//对ProductAttrKey表的Delete操作
            
                IProductAttrValueDAL attrValuedal = new ProductAttrValueDAL();
                var attrValueList = attrValuedal.GetAllByAttrKeyID(id);
            if (attrValueList.Count > 0)  //下拉选择时选择属性值
            {
                foreach (var item in attrValueList)
                {
                    attrValuedal.Delete(item);//对ProductAttrValue表的Delete操作
                }

            }
            return SaveChanges();
        }

        //IProductAttrKeyDAL dal=new ProductAttrKeyDAL();
        //public void Add(ProductAttrKey attrkey)
        //{
        //    dal.Add(attrkey);
        //}

        public List<ProductAttrKey> GetByCategoryID(int categoryId)
        {
            return dal.GetByCategoryID(categoryId);
        }
        public int Update(ProductAttrKey attrKey, List<ProductAttrValue> attrValues)
        {
  
           dal.Update(attrKey);//对ProductAttrKey表的update操作
            if (attrKey.EnterType == 2) {
                IProductAttrValueDAL attrValuedal = new ProductAttrValueDAL();
                var attrValueList = attrValuedal.GetAllByAttrKeyID(attrKey.ID);
                foreach (var item in attrValueList)
                {
                    attrValuedal.Delete(item);//对ProductAttrValue表的Delete操作
                }
                foreach (var item in attrValues)
                {
                    attrValuedal.Add(item);//对ProductAttrValue表的insert操作
                }
            }

            //事务执行的过程:两种情况,1..当sql语句全部执行成功后,让后提交事务(将数据刷新到磁盘,将数据做永久修改)
            //2.如果其中一条sql语句出现异常,对事务回滚操作,(对之前执行的sql语句进行反操作)
            int result = SaveChanges(); 
            return result;
        }

       
    }
}

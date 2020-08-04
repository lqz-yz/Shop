using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using IDAL;
using IBLL;

namespace BLL
{
    public class ProductCategoryBLL : BaseBLL<ProductCategory, ProductCategoryDAL>, IProductCategoryBLL
    {

        public List<ProductCategory> GetSub(int id)
        {

            return dal.GetSub(id);
        }
        public override int Add(ProductCategory Model)
        {
            var tran = this.BeginTrane();
            int result = 0;
            string Path = "";
            try
            {
                Model.CreateTime = DateTime.Now;
                dal.Add(Model);
                result += this.SaveChanges();
                if (Model.PID == 0)
                {
                    Path = Model.ID.ToString();
                }
                else
                {
                    var parent = this.Search(x => x.ID == Model.PID).First();
                    Path = parent.Path + "," + Model.ID;
                }
                Model.Path = Path;
                this.Update(Model);
                result += this.SaveChanges();
                tran.Commit();
            }
            catch (Exception ex)
            {
                tran.Rollback();
            }
            return SaveChanges();
        }


        public override int Update(ProductCategory Model)
        {
            string Path = "";
            try
            {
                if (Model.PID == 0)
                {
                    Path = Model.ID.ToString();
                }
                else
                {
                    var list = this.Search(x => x.ID == Model.PID);
                    var parent = list.First();
                    Path = parent.Path + "," + Model.ID;
                }
                Model.Path = Path;
                Model.UpdateTime = DateTime.Now;
                dal.Update(Model);
            }
            //ef上下文对象,默认情况下会对实体进行缓存(跟踪实体对象的状态)
            //当进行查询时会将数据库返回的树缓存起来,当再次请求相同的实体(主键相同,ID相同)时会直接从缓存中读取
            //缓存中只能存放唯一的实体(ID相同,只能存一个)
            catch (Exception ex)
            {

                throw;
            }
            return SaveChanges();
        }



        //IProductCategoryDAL dal = new ProductCategoryDAL();
        //public void Add(ProductCategory category)
        //{


        //    dal.Add(category);
        //}

        //public int Delete(int id)
        //{
        //    return dal.Delete(id);
        //}

        //public List<ProductCategory> GetAll()
        //{

        //    return dal.GetAll();
        //}

        //public ProductCategory Getone(int id)
        //{
        //    return dal.Getone(id);
        //}

        //public int Update(ProductCategory category)
        //{
        //    return dal.Update(category);
        //}
    }
}

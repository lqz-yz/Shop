using API.Models;
using BLL;
using IBLL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace API.Controllers
{
    public class ProductApiController : BaseApiController<Product, IProductBLL>
    {
        public override IProductBLL Bll
        {
            get
            {
                return new ProductBLL();
            }
        }
        [Route("api/product")]
        public ResponsMessage<ProductApiVModel> GetOne(int id)
        {
            try
            {
                List<ProductAttr> attrs;
                List<ProductSku> skus;
                //List<ProductSkuImg> SkuImg;
                var product = Bll.Getone(id, out attrs, out skus);
                ProductApiVModel Model = new ProductApiVModel()
                {
                    Product = product,
                    Attrs = attrs,
                    Skus = skus,
                    //SkuImg= SkuImg
                };
                return new ResponsMessage<ProductApiVModel>()
                {
                    Code = 200,
                    Message = "请求成功",
                    Data = Model
                    //total = Data.Count//行数
                };
            }
            catch (Exception)
            {
                return new ResponsMessage<ProductApiVModel>()
                {
                    Code = 200,
                    Message = "请求失败," + id,
                };
            }
        }




        // public override IProductBLL Bll { get => new ProductBLL(); }
        //public override IProductBLL Bll
        //{
        //    get
        //    {
        //        return new ProductBLL();
        //    }
        //}
        ////根据前面传的id获取这个id下的数据
        //public ResponsMessage<List<Product>> Get(int id)
        //{
        //    try
        //    {
        //        var Data = Bll.Search(x => x.ProductCategoryID == id);
        //        return new ResponsMessage<List<Product>>()
        //        {
        //            Code = 200,
        //            Message = "请求成功",
        //            Data = Data,
        //            total = Data.Count//行数
        //        };
        //    }
        //    catch (Exception)
        //    {
        //        return new ResponsMessage<List<Product>>()
        //        {
        //            Code = 200,
        //            Message = "请求失败," + id,
        //        };
        //    }
        //}

        //////查询全部数据
        //public virtual ResponsMessage<List<Product>> Get()//泛型方法
        //{
        //    try
        //    {
        //        return new ResponsMessage<List<Product>>()
        //        {
        //            Code = 200,
        //            Message = "请求成功",
        //            Data = Bll.GetAll(),
        //        };
        //        //<List<T>>可以去掉,根据后面传的Bll.GetAll()获取
        //    }
        //    catch (Exception)
        //    {
        //        return new ResponsMessage<List<Product>>()
        //        {
        //            Code = 200,
        //            Message = "请求失败," ,
        //        };
        //    }
        //}
    }
}
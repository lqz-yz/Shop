using API.Models;
using BLL;
using IBLL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Controllers
{
    public class SearchController : BaseApiController<Product, IProductBLL>
    {
        public override IProductBLL Bll
        {
            get
            {
                return new ProductBLL();
            }
        }
        //查询商品数据 
        public override ResponsMessage<PageModel<Product>> PostPager(SearchVModel search)
        {

            try
            {
                int count;
                var list = Bll.Search(
                    search.pageSize,
                    search.pageIndex,
                    false,
                    x => x.ID,
                    x => x.ProductTitle.Contains(search.keyWord),
                    out count
                    );
                PageModel<Product> page = new PageModel<Product>()
                {

                    count = count,
                    Data = list
                };
                return Success(page);

            }
            catch (Exception ex)
            {

                return Error<PageModel<Product>>("在根据关键字查询数据过程中出现异常");
            }
        }
        //public  ResponsMessage<List<Product>> Get(string query)//泛型方法
        //{
        //    try
        //    {
        //        var Data = Bll.Search(x => x.ProductTitle.Contains(query));
        //        return new ResponsMessage<List<Product>>()
        //        {
        //            Code = 200,
        //            Message = "请求成功",
        //            Data = Data,
        //            //total = Data.Count//行数
        //        };
        //    }
        //    catch (Exception)
        //    {
        //        return Error<List<Product>>("在搜索商品名称时出现异常");
        //    }
        //}

    }
}
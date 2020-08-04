using API.Models;
using IBLL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace API.Controllers
{
    public class BaseApiController<T, B> : ApiController
        where T : BaseModel, new()
        where B : IBaseBLL<T>
    {
        public virtual B Bll { get; set; }


        //查询全部数据
        public virtual ResponsMessage<List<T>>  Get()//泛型方法
        {
            try
            {
                return Success<List<T>>(Bll.GetAll());
                //<List<T>>可以去掉,根据后面传的Bll.GetAll()获取
            }
            catch (Exception)
            {
                return Error<List<T>>("查询全部数据过程中出现异常");
            }      
        }

        //查询单条数据
        public virtual ResponsMessage<T> Get(int id)
        {
            try
            {
                return Success<T>(Bll.Getone(id));
                //<T>可以去掉,根据后面传的Bll.Getone(id)获取
            }
            catch (Exception)
            {
                return Error<T>("查询单条数据过程中出现异常");
            }

        }

        public virtual ResponsMessage<PageModel<T>> PostPager(SearchVModel search)
        {

            try
            {
                int count;
                var list = Bll.Search(
                    search.pageSize,
                    search.pageIndex,
                    false,
                    x => x.ID,
                    x => 1 == 1,
                    out count
                    );
                PageModel<T> page = new PageModel<T>()
                {

                    count = count,
                    Data = list
                };
                return Success(page);

            }
            catch (Exception ex)
            {

                return Error<PageModel<T>>("在根据关键字查询数据过程中出现异常");
            }
        }
        //如果方法满足一下两个条件,只能处理post请求
        //1.方法必须是public
        //2.方法不是以Http动词开头的

        //成功封装
        [NonAction]//.该特性表示方法不能处理任何请求
        public ResponsMessage<D> Success<D>(D data) {
            return new ResponsMessage<D>()
            {
                Code = 200,
                Message = "请求成功",
                Data = data,
            };
        }
        //失败封装
        [NonAction]
        public ResponsMessage<D> Error<D>(string message)
        {
            return new ResponsMessage<D>()
            {
                Code = 200,
                Message = "请求失败,"+ message,
            };
        }
    }
}

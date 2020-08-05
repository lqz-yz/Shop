using API.Models;
using BLL;
using COMMON;
using IBLL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace API.Controllers
{
    public class OrderApiController : ApiController
    {
        IOrderMasterBLL Orderbll = new OrderMasterBLL();
        OrderMaster Order = new OrderMaster();
        IMemberBLL Bll = new MemberBLL();
        Member mem = new Member();

        [Route("api/order/data")]
        [HttpPost]
        public ResponsMessage<string> Orderdata(OrderVModel orderVModel)
        {
            var MemberID = RedisHelper.Get(orderVModel.token).ToString();
            //var MemberID = Bll.Getone(Int32.Parse(openid)).ID;
            //生成时间Unix时间戳
            DateTime time = DateTime.Parse(DateTime.Now.ToString());
            string timenew = time.Ticks.ToString();
            timenew = timenew.Substring(0, timenew.Length - 7).ToString();
            string guid = Guid.NewGuid().ToString("N");
            string random = new Random().Next(10000, 99999).ToString();
            string OrderNumber = timenew + random;
            //if (openId=)
            Order.TotalOrderPrice = orderVModel.Order_price;//总价
            Order.ShippingAddress = orderVModel.Consignee_addr;//收货地址
            Order.OrderStatus = "未支付";//订单状态
            Order.OrderNumber = OrderNumber.Trim();//订单编号
            Order.CreateTime = DateTime.Now;//时间
            Order.MemberID = Int32.Parse(MemberID);//用户ID
            var OrderNum=Orderbll.Add(Order, orderVModel.Goods);


            //快递单号
            return new ResponsMessage<string>
            {
                Code = 200,
                Data = OrderNum
            };
        }
    }
}
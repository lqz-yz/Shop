using DAL;
using IBLL;
using IDAL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
   public class OrderMasterBLL : BaseBLL<OrderMaster, OrderMasterDAL>, IOrderMasterBLL
    {
        IOrderScheduleDAL OrderScheduleDAL = new OrderScheduleDAL();
        IProductSkuDAL ProductSku = new ProductSkuDAL();
        public string Add(OrderMaster OrderMaster,  List<OrderSchedule> OrderSchedule)
        {
            var result = 0;
            var tran = dal.BeginTrane();
            try
            {
                //插入OrderMaster表
                dal.Add(OrderMaster);
                //提交获取OrderMasterID
                result += SaveChanges();
                //OrderSchedule
                foreach (var Order in OrderSchedule)
                {
                    Order.OrderMasterID = OrderMaster.ID;
                    OrderScheduleDAL.Add(Order);
                    var skus = ProductSku.Search(x => x.ID == Order.SkuID).FirstOrDefault();
                    skus.Stock = skus.Stock - Order.ProductNum;
                    ProductSku.Update(skus);
                }
               // result += SaveChanges();
                //foreach (var sku in OrderSchedule)
                //{
                    
                //}
                result += SaveChanges();
                tran.Commit();//总提交
            }
            catch (Exception)
            {
                tran.Rollback();
            }

            return result > 0 ? OrderMaster.OrderNumber : null;
        }
    }
}

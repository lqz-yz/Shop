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

        public int Add(OrderMaster OrderMaster,  List<OrderSchedule> OrderSchedule)
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
                }
                result += SaveChanges();
                tran.Commit();//总提交
            }
            catch (Exception)
            {
                tran.Rollback();
            }

            return result;
        }
    }
}

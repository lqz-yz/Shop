using BLL;
using IBLL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMMON
{
   public class NumberHelper
    {
        public static string Createnumber(int type)
        {
            //单号自动生成
            ShopEntities shopEntities = new ShopEntities();
            //1.根据类型查询当前流水号
            //Type:1-销售订单

            int currentNumber = 1;
            Random random = new Random();//随机数
            var number = shopEntities.Number.Where(x => x.Type == type).FirstOrDefault();
            if (number != null)
            {
                currentNumber = number.CurrentNumber.Value + 1;
                //将currentNumber更新到数据库
                number.CurrentNumber = currentNumber;
                shopEntities.SaveChanges();//执行Updata操作
            }
            else
            {
                Number number1 = new Number();
                number1.Type = type;
                number1.CurrentNumber = currentNumber;
                shopEntities.Number.Add(number1);
                shopEntities.SaveChanges();
            }
            string dh = DateTime.Now.ToString("yyyyMMddHHmmssfffff") + random.Next(10000, 99999) + currentNumber.ToString().PadLeft(5, '0');//PadLeft(5,'0')左部充五位数,补充树0
            return dh;
        }
    }
}

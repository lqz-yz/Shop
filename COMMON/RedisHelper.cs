using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMMON
{
   public class RedisHelper
    {
        //1.声明一个链接
        static ConnectionMultiplexer conn = ConnectionMultiplexer.Connect("192.168.230.128:6379,password=123456");
        //2.指定操作的数据库
        static IDatabase db = conn.GetDatabase(0);
        //写入
        public static void Set(string key, string value,TimeSpan timeSpan) {
            //3.写入数据
            //设置name的有效期为15天
            db.StringSet(key, value, timeSpan);
        }
        //读取
        public static string Get(string key)
        {
            //3.写入数据
            //设置name的有效期为15天
            return db.StringGet(key);
        }

    }
}

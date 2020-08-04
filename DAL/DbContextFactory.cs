using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
   public class DbContextFactory
    {
        public static ShopEntities GetEntities()
        {
            //包含直接返回  不包含转换一下
            //首先判断容器中是否包含上下文对象
            var obj = CallContext.GetData("dbContext");//从容器中获取数据
            //创建一个上下文对象并将其放到线程容器中
            if (obj == null)
            {
                ShopEntities entities = new ShopEntities();
                CallContext.SetData("dbContext", entities);//往容器中添加数据
                return entities;
            }
            return (ShopEntities)obj;
           
        }
    }
}

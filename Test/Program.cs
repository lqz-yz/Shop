using Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            int pageSize = 1;
            int pageIndex = 1;
            //OrderBy:升序,,OrderByDescending:降序
            //Skip:条国平多少个元素
            //Take:取多少个元素
            ShopEntities shopEntities = new ShopEntities();
            var list=shopEntities.Product.OrderBy(x=>x.ID).Skip((pageIndex-1)* pageSize).Take(pageSize).ToList();
            shopEntities.Product.Count();
            //ShopEntities entities = new ShopEntities();
            // //第一种删除方式:创建新的实体
            // ProductAttrValue model = new ProductAttrValue() {ID=11};
            // //将实体放到上下文中
            // DbEntityEntry entityEntry = entities.Entry(model);
            // entityEntry.State = EntityState.Deleted;
            //entities.SaveChanges();
            //第二种删除方式:先查后删
            //var model = entities.ProductAttrValue.First(x => x.ID == 10);
            //DbEntityEntry entityEntry = entities.Entry(model);
            //entityEntry.State = EntityState.Deleted;
            //entities.SaveChanges();

            //总结:entites.Entry方法有两个作用,如果实体不在EF中,该方法将实体直接添加到EF中并返回,如果存在不再进行添加直接将EF中的实体返回
            //总结:EF中不允许有两个主键相同的实体
            //Employer employer = new Employer();
            //employer.Dowork1();

            //里氏替换原则
            //Duck duck = new Duck();
            //duck.Fly();
            //duck.Swim();

            //Plane plane = new Plane();
            //plane.Fly();



            //c#操作redis
            //redis:一般存储kv类型的键值对数据(字典数据库)
            //1.声明一个链接
            var conn=ConnectionMultiplexer.Connect("192.168.230.130:6379,password=123456");
            //1.指定操作的数据库
            var db = conn.GetDatabase(0);
            //3.写入数据
            //设置name的有效期为15天
            db.StringSet("name", "lqz",DateTime.Now.AddDays(15)-DateTime.Now);
            db.StringSet("age", "20");
            db.StringSet("age", "30");
            //4.读取数据
            string name = db.StringGet("name");
            string age = db.StringGet("age");
            //redis如果不设置有效期,默认是永远有效(内村足够用)


            Console.ReadLine();

        }
    }
}

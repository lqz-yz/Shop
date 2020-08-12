using Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackExchange.Redis;
using System.IO;
using System.Threading;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            //int pageSize = 1;
            //int pageIndex = 1;
            ////OrderBy:升序,,OrderByDescending:降序
            ////Skip:条国平多少个元素
            ////Take:取多少个元素
            //ShopEntities shopEntities = new ShopEntities();
            //var list = shopEntities.Product.OrderBy(x => x.ID).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            //shopEntities.Product.Count();
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



            ////c#操作redis
            ////redis:一般存储kv类型的键值对数据(字典数据库)
            ////1.声明一个链接
            //var conn=ConnectionMultiplexer.Connect("192.168.230.130:6379,password=123456");
            ////1.指定操作的数据库
            //var db = conn.GetDatabase(0);
            ////3.写入数据
            ////设置name的有效期为15天
            //db.StringSet("name", "lqz",DateTime.Now.AddDays(15)-DateTime.Now);
            //db.StringSet("age", "20");
            //db.StringSet("age", "30");
            ////4.读取数据
            //string name = db.StringGet("name");
            //string age = db.StringGet("age");
            ////redis如果不设置有效期,默认是永远有效(内村足够用)

            //string strTmp = "abcdefg刘刘刘";
            //int i = System.Text.Encoding.Default.GetBytes(strTmp).Length;//刘等于两个长度
            ////将指定字符串中的所有字符编码为一个字节序列
            //int j = strTmp.Length;
            //Console.WriteLine(i);
            //Console.WriteLine(j);


            //string xsdh = Createnumber(1);

            //Console.WriteLine(xsdh);

            //string jhdh = Createnumber(2);

            //Console.WriteLine(jhdh);
            //Console.WriteLine(DateTime.Now.ToString("HHmmssfffff"));


            ////C#实现线程编程

            ////声明一个线程
            //Thread thread = new Thread(() =>
            //{

            //    for (int i = 0; i < 100; i++)
            //    {
            //        Console.WriteLine("x");
            //    }
            //    return "heelo word";
            //});
            //Thread thread = new Thread(F1);
            //thread.Start();//启动一个线程thread线程执行完毕才会继续往下执行

            ////线程阻塞
            //thread.Join();//主线程会等待

            //for (int i = 0; i < 100; i++)
            //{
            //    Console.WriteLine("y");
            //}


            //2.task

            //声明一个task,没有返回值并开始异步操作
            //Task task = Task.Run(() =>
            //{

            //    for (int i = 0; i < 100; i++)
            //    {
            //        Console.WriteLine("x");
            //    }
            //    return "heelo word";
            //});

            ////声明一个task,有返回值(task类型为泛型类型)并开始异步操作
            //Task<string> task = Task.Run(() =>
            //{

            //    for (int i = 0; i < 10; i++)
            //    {
            //        Console.WriteLine("aaa");
            //    }
            //    return "heelo word";
            //});

            //for (int i = 0; i < 10; i++)
            //{
            //    Console.WriteLine("111");
            //}
            //Console.WriteLine(task.Result);//遇到task.Result会产生一个阻塞,等待异步任务执行完才会继续往下执行


            Task<string> ts= F1();

            Console.WriteLine(ts.Result);
            for (int i = 0; i < 20; i++)
            {
                Console.WriteLine("111");
              
            }
            for (int i = 0; i < 20; i++)
            {
                Console.WriteLine("222");
            }
            Console.ReadLine();

        }

        //async:标识该方法内部会有至少一个异步任务
        //async修饰的方法内必须包含一个await运算符,await后一般跟一个异步任务
        //async修饰方法返回值:void  task  task<>
        //void,task不用返回值
        //task<T>:直接返回T类型值
        public async static Task<string> F1()
        {
            //主线程在执行第一个for循环
            for (int i = 0; i < 20; i++)
            {
                Console.WriteLine("aaa");
                //Thread.Sleep(200);
            }
            //遇到await开辟之后会从线程池中取一个线程执行await之后的所有代码
            await Task.Run(() =>
            {
                for (int i = 0; i < 20; i++)
                {
                    Console.WriteLine("bbb");
                   
                }
            });
            await Task.Run(() =>
            {
                for (int i = 0; i < 20; i++)
                {
                    Console.WriteLine("ccc");

                }
            });
            //await也会产生一个阻塞,等待await之后的异步任务执行结束之后再往下继续执行
            for (int i = 0; i < 20; i++)
            {
                Console.WriteLine("ddd");
            }
            for (int i = 0; i < 20; i++)
            {
                Console.WriteLine("fff");
            }
            return "helllll";
        }
            
        }
        //private static string Createnumber(int type)
        //{
        //    //单号自动生成
        //    ShopEntities shopEntities = new ShopEntities();
        //    //1.根据类型查询当前流水号
        //    //Type:1-销售订单
        //    int currentNumber = 1;
           
                
        //    Random random = new Random();//随机数
        //    var number = shopEntities.Number.Where(x => x.Type == type).FirstOrDefault();
        //    if (number != null)
        //    {
        //        //十二点重置
        //        if (DateTime.Now.ToString("HHmmssfffff") == DateTime.Now.ToString("00000000000"))
        //        {
        //            currentNumber = 1;
        //        }
        //        else {
        //            currentNumber = number.CurrentNumber.Value + 1;
        //        }
                
        //        //将currentNumber更新到数据库
        //        number.CurrentNumber = currentNumber;
        //        shopEntities.SaveChanges();//执行Updata操作
        //    }
        //    else
        //    {
        //        Number number1 = new Number();
        //        number1.Type = type;
        //        number1.CurrentNumber = currentNumber;
        //        shopEntities.Number.Add(number1);
        //        shopEntities.SaveChanges();
        //    }
        //    string dh = DateTime.Now.ToString("yyyyMMddHHmmssfffff") + random.Next(10000, 99999) + currentNumber.ToString().PadLeft(5, '0');//PadLeft(5,'0')左部充五位数,补充树0
        //    return dh;
        //}

        // 小张类
        //public class MrZhang
        //{
        //    // 其实买车票的悲情人物是小张
        //    public static void BuyTicket()
        //    {
        //        Console.WriteLine("NND,每次都让我去买票，鸡人呀！");
        //    }

        //    public static void BuyMovieTicket()
        //    {
        //        Console.WriteLine("我去，自己泡妞，还要让我带电影票！");
        //    }
        //}

        ////小明类
        //class MrMing
        //{
        //    // 声明一个委托，其实就是个“命令”
        //    public delegate void BugTicketEventHandler();

        //    public static void Main(string[] args)
        //    {
        //        // 这里就是具体阐述这个命令是干什么的，本例是MrZhang.BuyTicket“小张买车票”
        //        BugTicketEventHandler myDelegate = new BugTicketEventHandler(MrZhang.BuyTicket);

        //        myDelegate += MrZhang.BuyMovieTicket;
        //        // 这时候委托被附上了具体的方法
        //        myDelegate();
        //        Console.ReadKey();
        //    }
        //}

    }


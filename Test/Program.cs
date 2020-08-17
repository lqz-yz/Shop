using Model;
using System;
using System.Collections.Generic;
using System.Collections;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text; 
using System.Threading.Tasks;
using StackExchange.Redis;
using System.IO;
using System.Threading;
using System.Reflection;//反射
namespace Test
{
    class MyIndex {

        //private int[] array = new int[5];

        //声明索引器必须使用this关键字

        //public int this[int index] {
        //    set {
        //        array[index] = value;
        //    }
        //    get {
        //        return array[index];
        //     }
        //}
        //声明一个字典
        private IDictionary<string, string> keyValuePairs = new Dictionary<string, string>();
        //声明所引起
        public string this[string key] {
            set {
                keyValuePairs[key] = value;
            }
            get {
                return keyValuePairs[key];
            }
        }
    }
    class Program
    {
        //实现一个类型转化器
        private static T2 TypeConvert<T1, T2>(T1 p) where T2:new()
        {
            
            
            //创建T2类型实例
            T2 obj = new T2();
            //为相同的属性进行赋值
            //1.获取t1所有属性
            Type t1 = typeof(T1);
            Type t2 = typeof(T2);
            var props = t1.GetProperties();
            foreach (var prop in props)
            {
                //判断T2是否包含该元素
                var _prop = t2.GetProperty(prop.Name);
                if (_prop != null) {
                    //为obj对象的prop属性赋值
                    _prop.SetValue(obj, prop.GetValue(p));
                }
            }
            return obj;

        }

        static void Main(string[] args)
        {
            //通过new的方式创建对象 以及 访问类的成员
            //Person p = new Person();

            //p.age = 20;
            //p.name = "lqz";

            //反射可以访问类型的元数据(描述类的数据,元数据中包含类的名称以及类的成员信息)
            //通过反射创建对象以及访问类的成员
            //通过反射创建类的一个实例
            //Person person = (Person)Assembly.Load("Test").CreateInstance("Test.Person");

            //通过反射访问类的成员
            Person p = new Person();
            p.age = 20;
            //p.name = "lqz";

            //创建type类型变量
            //Type type = p.GetType();
            Type type = typeof(Person);
            Console.WriteLine("Person的完全限定名称" + type.FullName);
            //反射获取所有属性
            var props = type.GetProperties();
            foreach (var item in props)
            {
                if (item.Name == "name")
                {
                    item.SetValue(p, "liuqingzhao");//通过反射对属性进行赋值
                }
                Console.WriteLine(item.Name+"="+item.GetValue(p));//通过反射对属性进行取值
            }
            //反射获取方法
            var eaMethod=type.GetMethod("Eat");
            eaMethod.Invoke(p, null);

            //实现一个类型转化器的方法,将一个类型中相同的属性赋值给另一个对象
            student s = TypeConvert<Person, student>(p);



            //数据类型：值类型数据（int，bool，double，float），引用数据类型（字符串,数组，类，列表，委托）
            //装箱：将值类型转化为引用类型

            //int a = 20;
            //object obj = a;//装箱（尽量避免装箱操作，因为在会在堆开辟空间,占用系统资源）

            //////声明一个非泛型的字典
            //Hashtable hashtable = new Hashtable();
            ////hashtable.Add(1, "lqz");//将1转化为object类型发生装箱操作
            ////hashtable.Add("age", 20);//将20转化为object类型发生装箱操作

            ////声明泛型字典(避免装箱,拆箱)
            ////IDictionary<string, string> keyValuePairs = new Dictionary<string, string>();
            ////keyValuePairs.Add("name", "lqz");
            ////keyValuePairs.Add("age", "20");
            ////string age = keyValuePairs["name"];

            ////非泛型列表()会发生装箱操作
            //// ArrayList list = new ArrayList();
            ////泛型列表List<>

            ////拆箱：将引用类型转化为值类型

            //int b = (int)obj;//拆箱  强制转化



            //MyIndex myIndex = new MyIndex();
            ////myIndex[0]= 0;
            ////myIndex[1] = 1;
            //myIndex["name"] = "lqz";
            //Console.WriteLine(myIndex["name"]);

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


            //Task<string> ts = F1();

            //Console.WriteLine(ts.Result);
            //for (int i = 0; i < 20; i++)
            //{
            //    Console.WriteLine("111");

            //}
            //for (int i = 0; i < 20; i++)
            //{
            //    Console.WriteLine("222");
            //}
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


        private static string Createnumber(int type)
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
                //十二点重置
                if (DateTime.Now.ToString("HHmmssfffff") == DateTime.Now.ToString("00000000000"))
                {
                    currentNumber = 1;
                }
                else
                {
                    currentNumber = number.CurrentNumber.Value + 1;
                }

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

        //小张类
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
}



using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    /// <summary>
    /// 1.抽象类用abstract关键字声明
    /// 2.抽象类中可以包含抽象方法和非抽象方法
    /// 3.抽象类不能直接实例化
    /// 4.子类继承抽象类时必须实现抽象成员(使用override关键字)
    /// </summary>
    public abstract class AbstractClass
    {
        public abstract void Test();
        public  void Test2() {
            Console.WriteLine("Test2非抽象方法");
        }
    }

    public  class AbstractClassIpm: AbstractClass
    {
        public override void Test()
        {
            Console.WriteLine("实现方法");
        }

    }
}

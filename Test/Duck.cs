using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    class Duck : ISwim, IFly
    {
        public void Fly()
        {
            Console.WriteLine("鸭子用翅膀来飞行");
        }

        public void Swim()
        {
            Console.WriteLine("鸭子也可以游泳");
        }
    }
}

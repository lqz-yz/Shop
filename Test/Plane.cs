using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    class Plane : IFly
    {
        public void Fly()
        {
            Console.WriteLine("飞机用机翼来飞行");
        }

       
    }
}

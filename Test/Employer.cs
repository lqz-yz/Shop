using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    class Employer : ITest
    {
        public void Dowork1()
        {
            Console.WriteLine("以完成work1");
        }

        public string Dowork2(string food)
        {
            Console.WriteLine("以完成work2");
            return "";
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Service;

namespace WebApplication1.ServiceImpl
{
    public class CarServiceImpl : ICarService
    {
        private string guid = Guid.NewGuid().ToString();

        public void PrintGuid() {
            Console.WriteLine(guid);
        }
        public void Start()
        {
           Console.WriteLine("正在启动。。。。");
        }

        public void Stop()
        {
            Console.WriteLine("正在停止。。。。");
        }
    }
}

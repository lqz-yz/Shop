using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Service
{
   public  interface ICarService
    {
        public void PrintGuid();
        void Start();

        void Stop();
    }
}

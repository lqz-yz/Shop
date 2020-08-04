using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    interface ITest
    {
        //接口只能定义,不能实现
        //接口成员默认就是public,不需要显示指定
        void Dowork1();

        string Dowork2(string food);
    }
}

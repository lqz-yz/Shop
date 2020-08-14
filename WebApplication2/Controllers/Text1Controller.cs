using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Service;
using WebApplication1.ServiceImpl;

namespace WebApplication1.Controllers
{
    public class Text1Controller : Controller
    {
        //注入的方式:构造函数.属性
        //注入的地方:Controller的构造函数,Starup里的Configure方法  可以注入只有这两个
        //构造函数
        private ICarService _carService;
        public Text1Controller(ICarService carService)//HomeCotroler可以注入还有Starup里的Configure可以注入只有这两个
        {
            _carService = carService;
        }

        ////属性   core不支持
        //public ICarService carService { get; set; }
        public IActionResult Index()
        {
            _carService.PrintGuid();
            //carService.Start();
            //carService.Stop();
            return View();
        }
    }
}

using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model;
using IBLL;
using System.IO;
using System.Text;
using COMMON;
using Shop.Models;
using System.Dynamic;
using Newtonsoft.Json;

namespace Shop.Controllers
{
    //public class Result {
    //    public bool State { get; set; }
    //}
    [LoginCheck]
    public class ProductCategoryController : Controller
    {        IProductCategoryBLL bll = new ProductCategoryBLL();
        public static List<ProductCategory> pclist = new List<ProductCategory>();
        // GET: ProductCategory
        
        public ActionResult Index()
        {
            return View();
        }
        
        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Add(ProductCategory category)
        {
            //对模型的验证是否通过
            if (!ModelState.IsValid) {
                foreach (ModelState modelState in ModelState.Values)
                {
                    foreach (ModelError error in modelState.Errors)
                    {
                        return Json(new { State = false, msg =error.ErrorMessage });
                    }
                }
               
            }

            ////HttpPostedFileBase
            //var Img = Request.Files["Img"];
            //if (Img == null) {
            //    return Json(new { state = false, msg = "图片不存在" });
            //}
            //byte[] bytes = Common.StreamToBytes(Img.InputStream);
            //FileExtension fileExtension = FileHelper.CheckTextFile(bytes);

            //if (fileExtension == FileExtension.VALIDFILE) {
            //    return Json(new { state = false, msg = "上传文件已损坏" });
            //}
            //if (!(fileExtension == FileExtension.GIF || fileExtension == FileExtension.JPG || fileExtension == FileExtension.PNG))
            //{
            //    return Json(new { state = false, msg = "上传类型有误" });
            //}
            ////Server.MapPath 根据相对路径(项目中的路径)来获取绝对路径
            //string path = Server.MapPath("/StudentPhotos/");
            ////file.SaveAs(path):保存图片必须使用绝对路径+图片名称
            //var extension = Path.GetExtension(Img.FileName);

            ////string[] allowExtensions = { ".jpg", ".jpeg", ".png", ".gif" };

            ////if (!allowExtensions.Contains(extension))
            ////{
            ////    return Content("必须上传图片格式文件");
            ////}
            //Random random = new Random();
            //int randomNum= random.Next(10000, 99999);
            //var dataStr = DateTime.Now.ToString("yyyyMMddHHmmssfffff")+ randomNum.ToString();
            //Img.SaveAs(path + dataStr + Img.FileName);
            //category.Img = dataStr + Img.FileName;
            category.CreateTime = DateTime.Now;
            //var Name = Request.Form["Name"];
            //var OrderNum = Request.Form["OrderNum"];
            //var Img = Request.Files["Img"];
                   
            bll.Add(category);
            //匿名类型 var 变量名=new {属性名1=值1,属性2=值2...}
            //成功返回:State = true
            //失败返回:State = false
            //Josn方法将一个对象序列化成json串
            return Json(new { State = true, msg = "添加成功" });
        }
        [HttpGet]
        public ActionResult AjaxText()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AjaxTextPost()
        {
            var name = Request.Form["name"];
            var age = Request.Form["age"];
            var file = Request.Files["file"];
            //1  判断上传文件的类型(.jpg .jpeg .png .gif),类型检测不通过,直接返回
            //2  生成一个新的文件名:(yyyyMMddHHmmssfffff)+五位随机数+后缀名
            //3. 将图片保存到服务器
            //4  将数据保存到数据库

            //Result result = new Result();
            //result.State = true;
            //匿名类型 var 变量名=new {属性名1=值1,属性2=值2...}
            //成功返回:State = true
            //失败返回:State = false
            var result = new { State = true ,data=name};

            //Josn方法将一个对象序列化成json串
            return Json(result);
        }

        public ActionResult List()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetAll(int draw)
        {
            //获取全部分类列表
           // var list = bll.GetAll();

            //递归生成json数据
            //1.获取一级节点(分类)
            var rootList=bll.GetSub(0);
            List<ProductCategoryVModel> list = new List<ProductCategoryVModel>();
            foreach (var categary in rootList)
            {
                ProductCategoryVModel vModel = new ProductCategoryVModel();
                vModel.ID = categary.ID;
                vModel.Name = categary.Name;
                vModel.OrderNum = categary.OrderNum;
                vModel.Img = categary.Img;
                vModel.PID = categary.PID;
                vModel.keyWords = categary.keyWords;
                vModel.Path = categary.Path;
                vModel.children = new List<ProductCategoryVModel>();//初始化子节点集合
                GetSub(vModel);
                list.Add(vModel);
            }

            //构造返回josn数据{"draw","data":}
            var result = new { draw = draw,data=list};
            return Json(result);
        }
        //获取父节点下的所有子节点
        private void GetSub(ProductCategoryVModel parentNode)
        {
            var sublist = bll.GetSub(parentNode.ID);

            if (sublist.Count() == 0)//递归的终止条件
            {
                return;
            }
            //判断子节点下是否还包含子节点
            foreach (var categary in sublist)
            {
                ProductCategoryVModel vModel = new ProductCategoryVModel();
                vModel.ID = categary.ID;
                vModel.Name = categary.Name;
                vModel.OrderNum = categary.OrderNum;
                vModel.Img = categary.Img;
                vModel.PID = categary.PID;
                vModel.keyWords = categary.keyWords;
                vModel.Path = categary.Path;
                vModel.children = new List<ProductCategoryVModel>();//初始化子节点集合
                GetSub(vModel);//开始递归
                parentNode.children.Add(vModel);
            }
        }
        [HttpGet]
        public ActionResult GetByPID(int pid) {
            var list = bll.GetSub(pid);
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            int result= bll.Delete(id);
            return Json(new { state = result > 0 ? true : false });
        }


        //static int ID;
        //static string img;
        //static Nullable<System.DateTime> CreateTime;
        [HttpGet]
        public ActionResult Update(int id)
        {
            ProductCategory category = bll.Getone(id);
            List<ProductCategory> categories = bll.GetAll();
            //img = category.Img;
            //CreateTime = category.CreateTime;
            ProductCategoryUpdateVModel vModel = new ProductCategoryUpdateVModel() {
                Category = category, Categories = categories };
            return View(vModel);  
        }

        [HttpPost]
        public ActionResult Update(ProductCategory category)
        {
            if (!ModelState.IsValid)
            {
                foreach (ModelState modelState in ModelState.Values)
                {
                    foreach (ModelError error in modelState.Errors)
                    {
                        return Json(new { State = false, msg = error.ErrorMessage });
                    }
                }

            }
            //var Img = Request.Files["file"];
            //if (Img!=null) {
            //    byte[] bytes = Common.StreamToBytes(Img.InputStream);
            //    FileExtension fileExtension = FileHelper.CheckTextFile(bytes);

            //    if (fileExtension == FileExtension.VALIDFILE)
            //    {
            //        return Json(new { state = false, msg = "上传文件已损坏" });
            //    }
            //    if (!(fileExtension == FileExtension.GIF || fileExtension == FileExtension.JPG || fileExtension == FileExtension.PNG))
            //    {
            //        return Json(new { state = false, msg = "上传类型有误" });
            //    }
            //    //Server.MapPath 根据相对路径(项目中的路径)来获取绝对路径
            //    string path = Server.MapPath("/StudentPhotos/");
            //    //file.SaveAs(path):保存图片必须使用绝对路径+图片名称
            //    var extension = Path.GetExtension(Img.FileName);

            //    //string[] allowExtensions = { ".jpg", ".jpeg", ".png", ".gif" };

            //    //if (!allowExtensions.Contains(extension))
            //    //{
            //    //    return Content("必须上传图片格式文件");
            //    //}
            //    Random random = new Random();
            //    int randomNum = random.Next(10000, 99999);
            //    var dataStr = DateTime.Now.ToString("yyyyMMddHHmmssfffff") + randomNum.ToString();
            //    Img.SaveAs(path + dataStr + Img.FileName);
            //    category.Img = dataStr + Img.FileName;

            //}
            //category.UpdateTime = DateTime.Now;
            int result = bll.Update(category);
            return Json(new { State = result > 0 ? true : false });
        }
        //属性设置
        public ActionResult KeyList()
        {
            return View();
        }
        [HttpPost]
        public ActionResult GetAll1()
        {
            //递归生成json数据
            //1、获取所有一级节点（分类）
            var rootList = bll.GetSub(0);//(男装、女装)
            //dynamic动态类型
            List<dynamic> list = new List<dynamic>();

            //单独出PID为零
            dynamic firstNode = new ExpandoObject();//ExpandoObject:动态类型, 可以动态为其添加属性
            firstNode.text = "无上级分类";
            firstNode.tags = new List<int>();
            firstNode.tags.Add(0);//tags存放分类ID
            list.Add(firstNode);

            foreach (var category in rootList)
            {
                dynamic treeObj = new ExpandoObject();//ExpandoObject:动态类型, 可以动态为其添加属性

                treeObj.text = category.Name;
                treeObj.nodeId = category.ID;//可传可不传
                treeObj.tags = new List<int>();
                treeObj.tags.Add(category.ID);//tags存放分类ID

                GetSub1(treeObj);
                list.Add(treeObj);
            }
            //构造返回json对象 {"draw":,"data":}
            //var result = new {data = list };
            //JsonConvert.SerializeObject(list)将list转化成json串
            return Json(JsonConvert.SerializeObject(list));
        }

        //递归获取父节点的所有子节点（层层钻取获取子节点）
        private void GetSub1(dynamic treeObj)//( 1 男装 2 男士上衣 3 男士T恤) 
        {
            //var subList = bll.GetSub(treeObj.tags[0]);//(1 男士上衣 2 男士T恤、男士衬衫)
            //只展示一级分类和二级分类
            //Expression  表达式树中不能包含动态类型 
            int pid = (int)treeObj.tags[0];
            var subList = bll.Search(x => x.PID == pid );//在数据库阶段过滤
            subList = subList.Where(x => x.Path.Split(',').Length != 3).ToList();//内存中进行过滤
            //判断子节点下是否还包含子节点
            if (subList.Count == 0)//递归终止条件
            {
                return;
            }
            treeObj.nodes = new List<dynamic>();//初始化子节点集合
            foreach (var category in subList)
            {
                dynamic treeObj1 = new ExpandoObject();
                treeObj.nodeId = category.ID;
                treeObj1.text = category.Name;
                treeObj1.tags = new List<int>();
                treeObj1.tags.Add(category.ID);
                GetSub1(treeObj1);//开始进行递归
                treeObj.nodes.Add(treeObj1);
            }
        }
    }
}

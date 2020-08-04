using COMMON;
using Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IBLL;
using BLL;

namespace Shop.Controllers
{
    public class ProductBrandController : Controller
    {
        IProductBrandBLL bll = new ProductBrandBLL();
        // GET: ProductBrand
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(ProductBrand productBrand)
        {
            ////对模型的验证是否通过
            //if (!ModelState.IsValid)
            //{
            //    foreach (ModelState modelState in ModelState.Values)
            //    {
            //        foreach (ModelError error in modelState.Errors)
            //        {
            //            return Json(new { State = false, msg = error.ErrorMessage });
            //        }
            //    }

            //}

            ////HttpPostedFileBase
            //var Img = Request.Files["BrandLogo"];
            //var BrandImg = Request.Files["BrandImg"];

            //if (Img == null)
            //{
            //    return Json(new { state = false, msg = "图片不存在" });
            //}

            ////byte[] bytes = Common.StreamToBytes(BrandImg.InputStream);
            //byte[] bytes = Common.StreamToBytes(Img.InputStream);
            //FileExtension fileExtension = FileHelper.CheckTextFile(bytes);

            //if (fileExtension == FileExtension.VALIDFILE)
            //{
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
            //int randomNum = random.Next(10000, 99999);
            //var dataStr = DateTime.Now.ToString("yyyyMMddHHmmssfffff") + randomNum.ToString();
            //Img.SaveAs(path + dataStr + Img.FileName);
            //productBrand.BrandLogo = dataStr + Img.FileName;
            //productBrand.CreateTime = DateTime.Now;
            ////var Name = Request.Form["Name"];
            ////var OrderNum = Request.Form["OrderNum"];
            ////var Img = Request.Files["Img"];
            productBrand.CreateTime = DateTime.Now;
            bll.Add(productBrand);
            //匿名类型 var 变量名=new {属性名1=值1,属性2=值2...}
            //成功返回:State = true
            //失败返回:State = false
            //Josn方法将一个对象序列化成json串
            return Json(new { State = true, msg = "添加成功" });
            //return View();
        }

        public ActionResult List()
        {
            return View();
        }
        [HttpPost]
        public ActionResult GetAll(int draw, int pageSize, int pageIndex)
        {
            //获取全部分类列表
            //var list = bll.GetAll();
            int count;
            var list = bll.Search(pageSize, pageIndex, false,x=>x.ID, x => 1 == 1, out count);
            //var count = bll.GetCount(x => 1 == 1);
            
            //构造返回josn数据{"draw","data":}
            var result = new {
                draw = draw,
                data = list,
                recordsTotal = count,
                recordsFiltered = count
            };
            return Json(result);
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            int result = bll.Delete(id);
            return Json(new { state = result > 0 ? true : false });
        }
        public ActionResult Update()
        {
            return View();
        }
        [HttpGet]
        public virtual ActionResult Getone(int id)
        {
            
            var Brand = bll.Getone(id );
            var data = new
            {
                Brand = Brand,
            };
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Update(ProductBrand productBrand)
        {
            productBrand.UpdateTime = DateTime.Now;
            int result= bll.Update(productBrand);
            return Json(new { State = true, msg = "修改成功" });
        }
        public ActionResult GetAllByAddProduct()
        {
            var result = bll.GetAll();
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}
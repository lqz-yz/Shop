using COMMON;
using Constant;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shop.Controllers
{
    public class ImgController : Controller
    {
        public ActionResult Upload()
        {
            List<string> filesNames = new List<string>();
            //上传多张图片   
            var Imgs = Request.Files;
            for (int i = 0; i < Imgs.Count; i++)//Imgs.Count获取长度
            {
                var Img = Imgs[i];
                if (Img == null)
                {
                    return Json(new { state = false, msg = "图片不存在" });
                }
                byte[] bytes = Common.StreamToBytes(Img.InputStream);
                FileExtension fileExtension = FileHelper.CheckTextFile(bytes);

                if (fileExtension == FileExtension.VALIDFILE)
                {
                    return Json(new { state = false, msg = "上传文件已损坏" });
                }
                if (!(fileExtension == FileExtension.GIF || fileExtension == FileExtension.JPG || fileExtension == FileExtension.PNG))
                {
                    return Json(new { state = false, msg = "上传类型有误" });
                }
                //Server.MapPath 根据相对路径(项目中的路径)来获取绝对路径
               
                //file.SaveAs(path):保存图片必须使用绝对路径+图片名称
                var extension = Path.GetExtension(Img.FileName);
                Random random = new Random();
                int randomNum = random.Next(10000, 99999);
                var dataStr = DateTime.Now.ToString("yyyyMMddHHmmssfffff") + randomNum.ToString();
                //图片保存到服务器
                var imgType = Request["imgType"];//获取图片类型
                string upLoadPath = null;
                if (WebConstants.ImgPathDicts.ContainsKey(imgType))
                {
                    upLoadPath = WebConstants.ImgPathDicts[imgType];
                }
                else {
                    return Json(new { state = false });
                }
                string fullpath = upLoadPath + dataStr + Img.FileName;
                Img.SaveAs(Server.MapPath(fullpath));
                filesNames.Add(fullpath);
            }
            return Json(filesNames);
        }
    }
}